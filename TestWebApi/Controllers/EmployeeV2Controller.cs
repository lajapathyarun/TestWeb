using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    public class EmployeeV2Controller : ApiController
    {
        public IEnumerable<TeacherViewModelV2> GetTeachersV2()
        {
            var teachers = new List<TeacherViewModelV2>()    {
                new TeacherViewModelV2() { Id = 1, FirstName = "Rob" },
                new TeacherViewModelV2() { Id = 2, FirstName = "Mike" },
                new TeacherViewModelV2() { Id = 3, FirstName = "Mary" }
            };

            return teachers;
        }
    }
}