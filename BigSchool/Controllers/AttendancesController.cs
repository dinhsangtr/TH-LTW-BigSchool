using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers
{
    public class AttendancesController : ApiController
    {

        [HttpPost]
        public IHttpActionResult Attend(Course attendenceDto)
        {
            var userID = User.Identity.GetUserId();
            BigSchoolContext context = new BigSchoolContext();
            if (context.Attendences.Any(p => p.Attendee == userID && p.CourseId == attendenceDto.Id))
            {
                return BadRequest("The attenddance already exists!");
            }

            var attendence = new Attendence() { CourseId = attendenceDto.Id, Attendee = User.Identity.GetUserId() };
            context.Attendences.Add(attendence);
            context.SaveChanges();
            return Ok();
        }





    }
}
