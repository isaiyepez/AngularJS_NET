using EIS.BLL;
using EIS.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace EIS.API.Controllers
{
    public class RoleController : ApiController
    {
        RoleBs roleObjBs;

        public RoleController()
        {
            roleObjBs = new RoleBs();
        }

        [ResponseType(typeof(IEnumerable<Role>))]
        public IHttpActionResult Get()
        {
            return Ok(roleObjBs.GetALL());
        }

        [ResponseType(typeof(Role))]
        public IHttpActionResult Get(int id)
        {
            Role role = roleObjBs.GetByID(id);
            if (role != null)
                return Ok(role);
            else
                return NotFound();
        }

        [ResponseType(typeof(Role))]
        public IHttpActionResult Post(Role role)
        {
            if (ModelState.IsValid)
            {
                roleObjBs.Insert(role);
                return CreatedAtRoute("DefaultApi", new { id = role.RoleId }, role);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [ResponseType(typeof(Role))]
        public IHttpActionResult Put(int id, Role role)
        {
            if (ModelState.IsValid)
            {
                roleObjBs.Update(role);
                return Ok(role);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [ResponseType(typeof(Role))]
        public IHttpActionResult Delete(int id)
        {
            Role role = roleObjBs.GetByID(id);
            if (role != null)
            {
                roleObjBs.Delete(id);
                return Ok(role);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
