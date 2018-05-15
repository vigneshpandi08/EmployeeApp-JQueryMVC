using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeApplication.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            EmployeeListEntities entities = new EmployeeListEntities();
            return View(entities.Employees);
        }

        [HttpPost]
        public JsonResult Create(Employee emp)
        {
            using (EmployeeListEntities entities = new EmployeeListEntities())
            {
                entities.Employees.Add(emp);
                entities.SaveChanges();
            }

            return Json(emp);
        }

        [HttpPost]
        public ActionResult UpdateEmployee(Employee emp)
        {
            using (EmployeeListEntities entities = new EmployeeListEntities())
            {
                Employee updatedList = (from c in entities.Employees
                                        where c.EmployeeId == emp.EmployeeId
                                       select c).FirstOrDefault();
                updatedList.EmployeeName = emp.EmployeeName;
                updatedList.DesignationId = emp.DesignationId;
                updatedList.Gender = emp.Gender;
                updatedList.DOB = emp.DOB;
                updatedList.EmailId = emp.EmailId;
                updatedList.MobileNo = emp.MobileNo;
                updatedList.Address = emp.Address;
                updatedList.Description = emp.Description;
                updatedList.Salary = emp.Salary;
                entities.SaveChanges();
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteEmployee(int employeeId)
        {
            using (EmployeeListEntities entities = new EmployeeListEntities())
            {
                Employee employeeList = (from c in entities.Employees
                                       where c.EmployeeId == employeeId
                                       select c).FirstOrDefault();
                entities.Employees.Remove(employeeList);
                entities.SaveChanges();
            }
            return new EmptyResult();
        }

        public JsonResult GetDesignation()
        {
            List<Designation> details = new List<Designation>();
            SqlConnection con = new SqlConnection("Data Source=(Localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeApp;integrated Security=True");
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("select DesignationId,DesignationName from Designation", con);
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Designation pc = new Designation();
                pc.DesignationName = ds.Tables[0].Rows[i]["DesignationName"].ToString();
                pc.DesignationId =Convert.ToInt32(ds.Tables[0].Rows[i]["DesignationId"].ToString());
                details.Add(pc);
            }
            return Json(details, JsonRequestBehavior.AllowGet);
        }
    }
}