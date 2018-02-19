using ArtWebApp.Areas.MVCTNA.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ArtWebApp.Api
{
    public class ArtApiController : ApiController
    {
        ArtEntitiesnew db = new ArtEntitiesnew();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

      
       
        public IEnumerable<AtcMaster> GetAtc()
        {
            List<AtcMaster> atcMaster = new List<AtcMaster>();

            atcMaster = db.AtcMasters.Where(u => u.IsCompleted == "N" && u.IsClosed == "N").OrderBy(a => a.AtcNum).ToList();

           
         

            return atcMaster;
        }

        public IHttpActionResult GetAtcs()
        {
            var atcs = db.AtcMasters.Where(u => u.IsCompleted == "N" && u.IsClosed == "N").OrderBy(a => a.AtcNum).ToList();
            return Ok(new { results = atcs });
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


    public class TaskController : ApiController
    {

        // GET: api/GetAllPendingTask  
        [System.Web.Http.HttpGet]
        public IEnumerable<ArtTaskModel> GetAllPendingTask(int id)
        {
            List<TaskModel> students = new List<TaskModel>();

            List<ArtTaskModel> list = new List<ArtTaskModel>();
            list = WebartApiRepo.GetPendingTask(id);


            TaskModel mt = new TaskModel();
            mt.Rollnum = "2017-0001";
            mt.TaskTittle = "Nishan";
            mt.Location = "Kathmandu";
            mt.Pending = "3";
            mt.Message = "9849845061";
           
            students.Add(mt);


            int k = list.Count();


            return list;



         
        }



    }
}
