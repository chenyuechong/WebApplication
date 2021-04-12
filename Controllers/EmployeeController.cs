using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DB;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly DBOperate db;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            db = new DBOperate(_configuration);
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select EmployeeId, EmployeeName, Department,
                            convert(varchar(10),DateOfJoining,120) as DateOfJoining,PhotoFileName from dbo.Employee";
            
            return new JsonResult(db.excuteSQL(query));
        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {           
            string query = @"insert into dbo.Employee 
                        (EmployeeName,Department,DateOfJoining,PhotoFileName)
                        values
                        (
                        '" + emp.EmployeeName + @"'
                        ,'" + emp.Department + @"'
                        ,'" + emp.DateOfJoining + @"'
                        ,'" + emp.PhotoFileName + @"'
                        )
                        ";
            db.excuteSQL(query);

            return new JsonResult("Create Successfully");

        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"update dbo.Employee set 
                             EmployeeName ='" + emp.EmployeeName + @"'
                             ,Department ='" + emp.Department + @"'
                             ,DateOfJoining ='" + emp.DateOfJoining + @"'
                             where EmployeeId = '" + emp.EmployeeId + @"'
                             ";
            db.excuteSQL(query);

            return new JsonResult("Update Successfully");

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int Id)
        {
            string query = @"delete from dbo.Employee 
                             where EmployeeId = '" + Id + @"'
                             ";
            db.excuteSQL(query);

            return new JsonResult("Delete Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch(Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            string query = @"select DepartmentName from dbo.Department";
            

            return new JsonResult(db.excuteSQL(query));
        }


    }
}
