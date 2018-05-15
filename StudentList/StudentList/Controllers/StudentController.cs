using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentList.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            SampleEntities1 entities = new SampleEntities1();
            return View(entities.Students);
        }

        [HttpPost]
        public JsonResult Create(Student student)
        {
            using (SampleEntities1 entities = new SampleEntities1())
            {
                entities.Students.Add(student);
                entities.SaveChanges();
            }

            return Json(student);
        }
    }
}