﻿using LunWen.Model;
using LunWen.Model.Request;
using LunWen.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Service
{
    public class MenuService
    {
        private MenuRepository _menuRepository;

        public MenuService(MenuRepository MenuRepository)
        {
            _menuRepository = MenuRepository;
        }

        public int Add(Menu model)
        {
            return _menuRepository.Insert(model);
        }

        public void Remove(int id)
        {
            _menuRepository.Delete(id);
        }

        //public void Save(MenuSaveModel saveModel)
        //{
        //    if (!saveModel.Id.HasValue)
        //        throw new Exception("id不能为空");

        //    Menu model = _MenuRepository.SelectBy(saveModel.Id.Value);
        //    if (model == null)
        //        throw new Exception("id不存在");

        //    saveModel.SetValTo(model);

        //    _MenuRepository.Update(model);
        //}

        //public QueryResult<MenuItem> Get(MenuQuery query)
        //{
        //    return _MenuRepository.Get(query);
        //}

        public IEnumerable<Menu> GetMenuByRole(int roleId)
        {
            return _menuRepository.GetByRole(roleId);
        }

        public Menu GetFirstMenu(int roleId)
        {
            var menus = _menuRepository.GetByRole(roleId);
            if (menus != null)
                return menus.OrderBy(m => m.Sort).FirstOrDefault();
            else
                return null;
        }
    }
}
