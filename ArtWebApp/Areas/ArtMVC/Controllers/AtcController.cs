using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVC.Controllers
{
    public class AtcController : ApiController
    {
        ArtEntitiesnew db = new ArtEntitiesnew();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetAtc")]
        public IHttpActionResult GetAtc()
        {
            List<AtcMaster> atcMaster = new List<AtcMaster>();

            atcMaster = db.AtcMasters.Where(u => u.IsCompleted == "N" && u.IsClosed == "N").OrderBy(a => a.AtcNum).ToList();

            return Json(atcMaster);
        }
        // GET api/<controller>/5
        public string Get(int id)
        {
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