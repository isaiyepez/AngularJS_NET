using EIS.BLL;
using EIS.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace EIS.API.Controllers
{
    [EnableCors("*","*","*")]
    public class EmployeeController : ApiController
    {
        EmployeeBs employeeObjBs;

        public EmployeeController()
        {
            employeeObjBs = new EmployeeBs();
        }

        [ResponseType(typeof(IEnumerable<Employee>))]
        public IHttpActionResult Get()
        {
            var emps = employeeObjBs.GetALL().OrderByDescending(x=>x.CreatedDate);
            return Ok(emps);
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Get(string id)
        {
            Employee employee = employeeObjBs.GetByID(id);
            if (employee != null)
                return Ok(employee);
            else
                return NotFound();
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Post(Employee employee)
        {
            if (!(ModelState.IsValid && employeeObjBs.Insert(employee)))
                return SendBadRequest();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeId }, employee);
        }

        private IHttpActionResult SendBadRequest()
        {
            foreach (var error in employeeObjBs.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return BadRequest(ModelState);
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Put(Employee employee)
        {
            if (!(ModelState.IsValid && employeeObjBs.Update(employee)))
                return SendBadRequest();

            return Ok(employee);
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Delete(string id)
        {
            Employee employee = employeeObjBs.GetByID(id);
            if (employee != null)
            {
                employeeObjBs.Delete(id);
                return Ok(employee);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
