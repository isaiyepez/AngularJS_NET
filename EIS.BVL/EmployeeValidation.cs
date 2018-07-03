using EIS.BOL;
using EIS.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;

namespace EIS.BVL
{
    //public class UniqueEmployeeIdAttribute : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        EISDBContext db = new EISDBContext();
    //        string EmployeeIdValue = value.ToString();
    //        int count = db.Employees.Where(x => x.EmployeeId == EmployeeIdValue).ToList().Count();
    //        if (count != 0)
    //            return new ValidationResult("EmployeeId Already Exist");
    //        return ValidationResult.Success;
    //    }
    //}
    public class EmployeeValidation
    {
        public static List<string> Errors = new List<string>();
        public static bool IsValid(Employee emp)
        {
            EmployeeBs employeeObjBs = new EmployeeBs();

            string EmployeeIdValue = emp.EmployeeId.ToString();
            int count = employeeObjBs.GetALL().Where(x => x.EmployeeId == EmployeeIdValue).ToList().Count();
            if (count != 0)
            {
                Errors.Add("EmployeeId Already Exist");                
            }

            if (Errors.Count() == 0)
                return true;
            else
                return false;
        }
    }
}
