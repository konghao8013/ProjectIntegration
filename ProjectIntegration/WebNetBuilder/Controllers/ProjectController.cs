using LibExtend.NetworkServer;
using NetBuilderServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebNetBuilder.Api
{
    public class ProjectController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<ProjectSetting> Get()
        {
            var http = new HttpHelper();
            var value = http.HttpGet(System.Web.Configuration.WebConfigurationManager.AppSettings["ServerURL"] + "/getprojects", "");
            return value.Deserialize<List<ProjectSetting>>();
        }


        // GET api/<controller>/5
        public string Get(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var http = new HttpHelper();
                var value = http.HttpGet(System.Web.Configuration.WebConfigurationManager.AppSettings["ServerURL"] + "/builder", "name=" + name);
                return value;
            }
            return "-1";
        }
        [AcceptVerbs("POST")]
        // POST api/<controller>
        public bool Post(ProjectSetting project)
        {
            var http = new HttpHelper();
            var result = false;

            http.HttpPost(System.Web.Configuration.WebConfigurationManager.AppSettings["ServerURL"] + "/projectsetting", project.Serialize());
            result = true;

            return result;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
        [AcceptVerbs("Delete")]
        // DELETE api/<controller>/5
        public bool Delete(dynamic name)
        {
            var http = new HttpHelper();
            var result = false;
            try
            {
                string str = http.HttpGet(System.Web.Configuration.WebConfigurationManager.AppSettings["ServerURL"] + "/removeproject", "name=" + name);
                if (str.IndexOf("true")>-1)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {

                //  throw;
            }
            return result;
        }
    }
}