using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;

namespace LunWen.Repository.baseDAO
{
    /// <summary>
    /// 适用
    ///     表主键是列名id，且自增
    ///     表示是否删除是列名status，且等于0时代表记录删除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MySqlBaseRepository<T> : IBaseRepository<T> where T : class
    {
        public int Insert(T model)
        {
            using (var conn = GetConn())
            {
                StringBuilder builder = new StringBuilder();
                builder.Append($"insert into {typeof(T).Name} (");
                builder.Append(GetFieldsStr());
                builder.Append(") values (");
                builder.Append(GetFieldsWithParaStr());
                builder.Append(" );");
                builder.Append($"select max(id) from {typeof(T).Name};");

                string sql = builder.ToString();
                var para = GetParas(model);

                return conn.ExecuteScalar<int>(sql, para);
            }
        }

        //这个删除的缺点，没有记录是谁操作的。
        public void Delete(int id)
        {
            using (var conn = GetConn())
            {
                string sql = string.Format("update {0} set status = 0 where id = @id", typeof(T).Name);
                conn.Execute(sql, new { id = id });
            }
        }

        public void DeleteAll(IEnumerable<int> ids)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var id in ids)
            {
                builder.AppendFormat("update {0} set status = 0 where id = @id", typeof(T).Name);
            }
            throw new NotImplementedException();
        }

        public void Update(T model)
        {
            using (var conn = GetConn())
            {
                StringBuilder builder = new StringBuilder();
                builder.Append($"update {typeof(T).Name} set ");
                builder.Append(GetUpdateFields(model));
                builder.Append(" where ");
                builder.Append(" id = @id ");

                string sql = builder.ToString();
                var para = GetParas(model);

                conn.Execute(sql, para);
            }
        }

        public void UpdateBy(Dictionary<string, string> destFields, Dictionary<string, string> whereFields)
        {
            using (var conn = GetConn())
            {
                StringBuilder builder = new StringBuilder();
                builder.Append($"update {typeof(T).Name} set ");
                builder.Append(DicHelper.ToSql(destFields, DicSeprator.Comma));
                builder.Append(" where ");
                builder.Append(DicHelper.ToSqlWithPara(whereFields));

                string sql = builder.ToString();
                var para = GetParas(whereFields);

                conn.Execute(sql, para);
            }
        }

        public T SelectBy(int Id)
        {
            using (var conn = GetConn())
            {
                string sql = $"select * from {typeof(T).Name} where id = @id";
                var model = conn.QueryFirstOrDefault<T>(sql, new { id = Id });
                return model;
            }
        }

        public IEnumerable<T> SelectBy(Dictionary<string, string> fields)
        {
            using (var conn = GetConn())
            {
                StringBuilder builder = new StringBuilder();
                builder.Append($"select * from {typeof(T).Name} ");
                builder.Append(" where ");
                builder.Append(DicHelper.ToSqlWithPara(fields));

                string sql = builder.ToString();
                var para = GetParas(fields);

                IEnumerable<T> models = conn.Query<T>(sql, para);
                return models;
            }
        }

        public bool ExeTransaction(string sql)
        {
            var conn = GetConn();

            var tran = conn.BeginTransaction();
            try
            {
                conn.Execute(sql);
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                LunWen.Infrastructure.Logger.Log(ex.Message);
                tran.Rollback();
                return false;
            }
            finally
            {
                conn.Dispose();
            }
        }

        #region 帮助方法

        //Name,Sex,Mail
        private string GetFieldsStr()
        {
            List<string> list = new List<string>();
            foreach (var proper in typeof(T).GetProperties())
            {
                if (proper.Name.ToLower() != "id")
                    list.Add(proper.Name);
            }
            return string.Join(",", list.ToArray());
        }

        //@Name,@Sex,@Mail
        private string GetFieldsWithParaStr()
        {
            List<string> list = new List<string>();
            foreach (var proper in typeof(T).GetProperties())
            {
                if (proper.Name.ToLower() != "id")
                    list.Add("@" + proper.Name);
            }
            return string.Join(",", list.ToArray());
        }

        //Name=@Name,Sex=@Sex,Mail=@Mail
        private string GetUpdateFields(T model)
        {
            List<string> list = new List<string>();
            foreach (var proper in typeof(T).GetProperties())
            {
                var val = proper.GetValue(model);
                list.Add($"{proper.Name}=@{proper.Name}");
            }
            return string.Join(",", list.ToArray());
        }

        private DynamicParameters GetParas(T model)
        {
            DynamicParameters para = new DynamicParameters();
            foreach (var proper in typeof(T).GetProperties())
            {
                var val = proper.GetValue(model);
                para.Add("@" + proper.Name, val);
            }
            return para;
        }

        private DynamicParameters GetParas(Dictionary<string, string> whereFields)
        {
            DynamicParameters para = new DynamicParameters();
            foreach (var item in whereFields)
            {
                para.Add($"@{item.Key}", item.Value);
            }
            return para;
        }

        private MySqlConnection _conn;

        protected MySqlConnection GetConn()
        {
            //不要每次都是新建一个，这样连接过多就会崩溃
            if (_conn == null)
            {
                string connStr = "server=192.168.103.90;database=thesismgmt;Uid=thesismgmt;Pwd=123456;";
                connStr = "server=127.0.0.1;database=thesisdb;Uid=root;Pwd=123456;";
                connStr = ConfigurationManager.ConnectionStrings["connStr"].ToString();
                MySqlConnection conn = new MySqlConnection(connStr);
                _conn = conn;
            }

            if (_conn.State == System.Data.ConnectionState.Closed)
                _conn.Open();

            return _conn;
        }

        public IEnumerable<Column> GetTableColumns(string tableName)
        {
            using (var conn = GetConn())
            {
                string sql = $"select column_name as ColumnName,column_comment as ColumnComment,data_type as DataType from information_schema.columns where table_schema = '{conn.Database}' and table_name = '{tableName}'";
                IEnumerable<Column> list = conn.Query<Column>(sql);
                return list;
            }
        }

        public IEnumerable<Table> GetTables()
        {
            using (var conn = GetConn())
            {
                string sql = $"select table_name as TableName,table_comment as TableComment from information_schema.tables where table_schema='{conn.Database}' and table_type='base table';";
                IEnumerable<Table> list = conn.Query<Table>(sql);
                return list;
            }
        }

        #endregion

    }
}
