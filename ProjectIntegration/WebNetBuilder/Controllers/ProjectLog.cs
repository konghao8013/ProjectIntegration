using LibExtend.NetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebNetBuilder.Controllers
{
    public class ProjectLogController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(string name)
        {
            ///  getlog
            if (!string.IsNullOrEmpty(name))
            {
                var http = new HttpHelper();
                var value = http.HttpGet(System.Web.Configuration.WebConfigurationManager.AppSettings["ServerURL"] + "/getlog", "name=" + name);
                return value;
            }
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}