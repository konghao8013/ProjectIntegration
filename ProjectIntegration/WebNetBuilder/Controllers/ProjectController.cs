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
        public string Get(int id)
        {
            return "value";
        }
        [AcceptVerbs("POST")]
        // POST api/<controller>
        public void Post(ProjectSetting project)
        {
            var http = new HttpHelper();
            http.HttpPost(System.Web.Configuration.WebConfigurationManager.AppSettings["ServerURL"] + "/projectsetting", project.Serialize());
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