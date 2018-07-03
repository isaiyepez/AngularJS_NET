using EIS.BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIS.DAL
{
    public class EmployeeDb
    {
        private EISDBContext db;

        public EmployeeDb()
        {
            db = new EISDBContext();
        }
        public IEnumerable<Employee> GetALL()
        {
            return db.Employees.ToList();
        }
        public Employee GetByID(string Id)
        {
            return db.Employees.Find(Id);
        }
        public void Insert(Employee emp)
        {
            db.Employees.Add(emp);
            Save();
        }
        public void Delete(string Id)
        {
            Employee emp = db.Employees.Find(Id);
            db.Employees.Remove(emp);
            Save();
        }
        public void Update(Employee emp)
        {
            db.Entry(emp).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            Save();
            db.Configuration.ValidateOnSaveEnabled = true;
        }

        public Employee GetByEmail(string email)
        {
            return db.Employees.Where(x=>x.Email==email).FirstOrDefault();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
