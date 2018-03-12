using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TestWebApi.Models;
using TestWebApi.VIewModels;
using System.Net.Http;
using System.Net;
using System.Web.Http.Cors;
using TestWebApi.Custom;

namespace TestWebApi.Controllers
{
    [RoutePrefix("api/employees")]
    [EnableCors("*", "*", "*")]
    public class EmployeeController : ApiController
    {
        static List<EmployeeViewModel> empModels
            = new List<EmployeeViewModel>(){
                    new EmployeeViewModel() { Id = 1, Name = "Tom", Courses= new List<string> { "C#", "ASP.NET", "SQL Server" } },
                    new EmployeeViewModel() { Id = 2, Name = "Sam" },
                    new EmployeeViewModel() { Id = 3, Name = "John" } };

        public EmployeeController()
        {

        }

        [Route("")]
        //[DisableCors]        
        public List<EmployeeViewModel> GetEmployees()
        {
            return empModels;
        }

        // ATTRIBUTE ROUTE CONSTRAINT
        // USING HTTPRESPONSEMESSAGE
        //[EnableCors("*", "*", "*")]
        [Route("{id:int:range(1,5)}", Name = "GetEmployeeById")]
        public HttpResponseMessage GetEmployeeById(int? id)
        {
            var model = empModels.FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee id not found!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        // IHttpActionResult vs HTTPRESPONSEMESSAGE
        // Simplified Syntax of HTTPRESPONSEMESSAGE
        // Cleaner to Read and Unit Testing is easier
        [Route("{name:alpha}")]
        public IHttpActionResult GetEmployeeByName(string name)
        {
            var model = empModels.FirstOrDefault(x => x.Name == name);

            if (model == null)
            {
                return Content(HttpStatusCode.NotFound, "Employee name not found!");
                //return NotFound();
            }

            return Ok(model); // Simplified HTTPRESPONSEMESSAGE syntax.
        }

        //VERSION 1
        // Override Route Prefix using tilde "~" symbol in Route attribute
        [Route("~/api/v1/teachers")]
        public IEnumerable<TeacherViewModel> GetTeachersV1()
        {
            var teachers = new List<TeacherViewModel>()    {
                new TeacherViewModel() { Id = 1, Name = "Rob" },
                new TeacherViewModel() { Id = 2, Name = "Mike" },
                new TeacherViewModel() { Id = 3, Name = "Mary" }
            };

            return teachers;
        }

        //VERSION 2
        [Route("~/api/v2/teachers")]
        public IEnumerable<TeacherViewModelV2> GetTeachersV2()
        {
            var teachers = new List<TeacherViewModelV2>()    {
                new TeacherViewModelV2() { Id = 1, FirstName = "Rob" },
                new TeacherViewModelV2() { Id = 2, FirstName = "Mike" },
                new TeacherViewModelV2() { Id = 3, FirstName = "Mary" }
            };

            return teachers;
        }

        // Route Parameter using Curly brace ({id})
        [Route("{id}/courses")]
        public List<string> GetEmployeeCourses(int? id)
        {
            if (id == 1)
                return new List<string>() { "C#", "ASP.NET", "SQL Server" };
            else if (id == 2)
                return new List<string>() { "ASP.NET Web API", "C#", "SQL Server" };
            else
                return new List<string>() { "Bootstrap", "jQuery", "AngularJs" };
        }

        [Route("Add")]
        public HttpResponseMessage PostEmployee(EmployeeViewModel viewodel)
        {
            empModels.Add(viewodel);

            var response = Request.CreateResponse(HttpStatusCode.Created);

            // Problem with the Below : Slash will be missed if you mssing to provide at the end
            // response.Headers.Location = new Uri(Request.RequestUri + viewodel.Id.ToString());

            // GENERATE LINKS USING ROUTE NAME
            response.Headers.Location = new
                Uri(Url.Link("GetEmployeeById", new { id = viewodel.Id }));

            return response;
        }
    }
}