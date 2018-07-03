using EIS.BOL;
using EIS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIS.BLL
{
    public class RoleBs
    {
        private RoleDb objDb;

        public RoleBs()
        {
            objDb = new RoleDb();
        }
        public IEnumerable<Role> GetALL()
        {
            return objDb.GetALL().ToList();
        }
        public Role GetByID(int Id)
        {
            return objDb.GetByID(Id);
        }
        public void Insert(Role role)
        {
            objDb.Insert(role);
        }
        public void Delete(int Id)
        {
            objDb.Delete(Id);
        }
        public void Update(Role role)
        {
            objDb.Update(role);
        }
    }
}
