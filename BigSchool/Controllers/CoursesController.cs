using BigSchool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {



        // GET: Courses
        public ActionResult Create()
        {
            //get list category
            BigSchoolContext context = new BigSchoolContext();
            Course objCourse = new Course();
            objCourse.ListCategory = context.Categories.ToList();

            return View(objCourse);
        }


        // POST: Courses
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objCourse)
        {
            BigSchoolContext context = new BigSchoolContext();
            //khong xet valid lectureId vi bang user dang nhap
            ModelState.Remove("LecturerId");
            if (!ModelState.IsValid)
            {
                objCourse.ListCategory = context.Categories.ToList();
                return View("Create", objCourse);
            }

            //Lay login user id
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objCourse.LecturerId = user.Id;

            //add vao csdl
            context.Courses.Add(objCourse);
            context.SaveChanges();

            //tro ve home, index
            return RedirectToAction("Index", "Home");
        }



        //Course I am going
        public ActionResult Attending()
        {

            BigSchoolContext context = new BigSchoolContext();
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var listAttendences = context.Attendences.Where(x => x.Attendee == currentUser.Id).ToList();
            var courses = new List<Course>();
            foreach (Attendence temp in listAttendences)
            {
                Course objCourse = temp.Course;
                objCourse.LecturerId = System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(objCourse.LecturerId).Name;
                courses.Add(objCourse);
            }
            return View(courses);
        }


        //My upcoming course
        public ActionResult Mine()
        {
            Session.Remove("Delete");
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            BigSchoolContext context = new BigSchoolContext();
            var courses = context.Courses.Where(x => x.LecturerId == currentUser.Id && x.DateTime > DateTime.Now).ToList();
            foreach (Course i in courses)
            {
                i.LecturerId = currentUser.Name;
            }
            return View(courses);
        }

        //My upcoming course - EDIT
        [HttpGet]
        public ActionResult Edit(int id) // id course
        {
            BigSchoolContext context = new BigSchoolContext();
            Course course = context.Courses.SingleOrDefault(x => x.Id == id);
            course.ListCategory = context.Categories.ToList();
            if (course == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, string Place, DateTime DateTime, int CategoryId)
        {
            BigSchoolContext context = new BigSchoolContext();
            Course course = context.Courses.SingleOrDefault(x => x.Id == Id);
            course.Place = Place;
            course.DateTime = DateTime;
            course.CategoryId = CategoryId;

            context.Courses.AddOrUpdate(course);
            context.SaveChanges();
            return RedirectToAction("Mine");
        }



        //My upcoming course - DELETE
        [HttpGet]
        public ActionResult Delete(int id) // id course
        {
            BigSchoolContext context = new BigSchoolContext();
            Course course = context.Courses.SingleOrDefault(x => x.Id == id);

            var category = context.Categories.SingleOrDefault(c => c.Id == course.CategoryId);
            ViewBag.CategoryName = category.Name;
            if (course == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(course);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourse(int id)
        {
            Session.Remove("Delete");
            BigSchoolContext context = new BigSchoolContext();
            Course course = context.Courses.SingleOrDefault(x => x.Id == id);
            if (course == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //kiem tra da co trong bang attendance chua
            List<Attendence> listAttendence = context.Attendences.Where(a => a.CourseId == course.Id).ToList();
            if (listAttendence.Count > 0)
            {
                Session["Delete"] = "Không thể xóa - (>.<)"; // thong bao
                return RedirectToAction("Delete", new { id = id });
            }
            else
            {
                context.Courses.Remove(course);
                context.SaveChanges();
                return RedirectToAction("Mine");
            }
        }
    }
}