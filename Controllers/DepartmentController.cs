using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DB;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DBOperate db;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new DBOperate(_configuration);
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DepartmentId, DepartmentName from dbo.Department";
            
            return new JsonResult(db.excuteSQL(query));
        }

        [HttpPost]
        public JsonResult Post(Department dep)
        {
            string query = @"insert into dbo.Department values('"+ dep.DepartmentName +@"')";
            db.excuteSQL(query);
            return new JsonResult("Create Successfully");

        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"update dbo.Department set 
                             DepartmentName ='" + dep.DepartmentName +@"'
                             where DepartmentId = '" + dep.DepartmentId +@"'
                             ";
            db.excuteSQL(query);

            return new JsonResult("Update Successfully");

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int Id)
        {
            string query = @"delete from dbo.Department 
                             where DepartmentId = '" + Id + @"'
                             ";
            db.excuteSQL(query);

            return new JsonResult("Delete Successfully");
        }

    }
}
