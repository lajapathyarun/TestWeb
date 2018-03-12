using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    public class EmployeeV1Controller : ApiController
    {
        public IEnumerable<TeacherViewModel> GetTeachersV1()
        {
            var teachers = new List<TeacherViewModel>()    {
                new TeacherViewModel() { Id = 1, Name = "Rob" },
                new TeacherViewModel() { Id = 2, Name = "Mike" },
                new TeacherViewModel() { Id = 3, Name = "Mary" }
            };

            return teachers;
        }
    }
}