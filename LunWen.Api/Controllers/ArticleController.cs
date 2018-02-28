using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LunWen.Api.Controllers
{
    public class ArticleController : ApiController
    {
        // GET: api/Article
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Article/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Article
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Article/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Article/5
        public void Delete(int id)
        {
        }
    }
}
