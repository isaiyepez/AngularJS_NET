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
using System.Web.Http.ModelBinding;

namespace EIS.API.Controllers
{
    [EnableCors("*", "*", "*", "*")]
    public class LoginController : ApiController
    {
        EmployeeBs employeeObjBs;

        public LoginController()
        {
            employeeObjBs = new EmployeeBs();
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Post(Employee emp)
        {
            Employee employee = employeeObjBs.GetByEmail(emp.Email);

            if (employee == null)
            {
                ModelState.AddModelError("", "Email id Does not Exist");
                return BadRequest(ModelState);
            }
            else if (employee.Password != emp.Password)
            {
                ModelState.AddModelError("", "Invalid Password");
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(employee);
            }
        }

        [ResponseType(typeof(Employee))]
        [ActionName("RecoverPassword")]
        public IHttpActionResult Get(string empEmail)
        {
            Employee employee = employeeObjBs.RecoverPasswordByEmail(empEmail);

            if (employee == null)
            {
                ModelState.AddModelError("", "Email id Does not Exist");
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(employee);
            }
        }
    }
}
