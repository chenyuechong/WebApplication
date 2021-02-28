using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DB;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private DBOperate db;
        public UsersController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            db = new DBOperate(_configuration);
        }


        [Route("register")]
        [HttpPost]
        public JsonResult register(RegisterInfo user)
        {
            string query = @"insert into User (name, email, password, city, country)
                            values
                            (
                            '" + user.name + @"'
                            ,'" + user.email + @"'
                            ,'" + user.password + @"'
                            ,'" + user.city + @"'
                            ,'" + user.country + @"'
                            )";
            return new JsonResult(db.excuteSQL(query));
        }



    }
}
