using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        private PetitionDBOperate db;
        private readonly commonfunctions funHelper;
        public UsersController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            db = new PetitionDBOperate(_configuration);
            funHelper = new commonfunctions();
        }


        [Route("register")]
        [HttpPost]
        public IActionResult register(RegisterInfo user)
        {
            if (funHelper.IsValidEmail(user.email) == false)
                return BadRequest("email format is wrong");

            if (funHelper.IsEmailExist(user.email,db))
                return BadRequest("email is already existed");

            string query = @"insert into Users (name, email, password, city, country)
                            values
                            (
                            '" + user.name + @"'
                            ,'" + user.email + @"'
                            ,'" + user.password + @"'
                            ,'" + user.city + @"'
                            ,'" + user.country + @"'
                            )";
             db.excuteSQL(query);
            return Ok();
        }


        [Route("login")]
        [HttpPost]
        public IActionResult login(login user)
        {
            string token = funHelper.generateToken();
            

            string  query = @"select users.auth_token as token, users.user_id as userId from users
                            where 
                            users.email='" + user.email + @"'
                            and users.password='" + user.password + @"'
                            ";
            DataTable b = db.excuteSQL(query);

            if (b.Rows.Count == 0)
            {
                return BadRequest("user is not existed");
            }
             query = @"update users 
                            set users.auth_token ='" + token + @"'
                            where 
                            users.email='" + user.email + @"'
                            and users.password='" + user.password + @"'
                            ";
            db.excuteSQL(query);
            query = @"select users.auth_token as token, users.user_id as userId from users
                            where 
                            users.email='" + user.email + @"'
                            and users.password='" + user.password + @"'
                            ";
            b = db.excuteSQL(query);
            return new JsonResult(b);
        }


        [Route("logout")]
        [HttpPost]
        public IActionResult logout([FromHeader] string token)
        {
            if (token == "")
            {
                return Unauthorized("the token is null");
            }
            string query = @"select * from users  
                            where 
                            users.auth_token='" + token + @"'
                            ";
            DataTable b = db.excuteSQL(query);
            if (b.Rows.Count == 0)
                return Unauthorized("token is wrong or do not logout again");

            query = @"update users 
                            set users.auth_token = NULL
                            where 
                            users.auth_token='" + token + @"'
                            ";
            b = db.excuteSQL(query);

            return new JsonResult(b);
        }


        [HttpGet("{id}")]
        public IActionResult getID(int id,[FromHeader] string token)
        {
            string query = @"select user_id from users where auth_token='" + token + @"'";
            DataTable b = db.excuteSQL(query);
            string userId = b.Rows[0][0].ToString();
            if (userId != id.ToString())
                return BadRequest("you can not get other's infomation");
            query = @"select name, city, country, email from users  
                            where 
                            users.user_id= '" + id + @"'
                            ";
             b = db.excuteSQL(query);
            if (b.Rows.Count == 0)
            {
                return NotFound();
            }
            return new JsonResult(b);
        }

        [HttpPatch("{id}")]
        public IActionResult patchID(int id,[FromHeader]string token, [FromBody]updateUserInfo info)
        {
            if (info.password == null)
                return BadRequest("user password cannot be null");
            else if (!funHelper.IsValidEmail(info.email))
                return BadRequest("It is not a valid emailaddress");
            else if (info.password != null && info.currentPassword != null && info.currentPassword != info.password)
                return Forbid("passwords do not match");
            else if (token == "")
                return Unauthorized();
            else if (!funHelper.IsUser(token, id, db))
                return Forbid("you can only change your information");
            else if (!funHelper.IsEmailExist(info.email, db))
                return Forbid("you email is currently used in this sytem");
            else
            {
               
                string query = @"update users set 
                             name ='" + info.name + @"'
                             ,city ='" + info.city + @"'
                             ,country ='" + info.country + @"'
                             ,email ='" + info.email + @"'
                             ,password ='" + info.password + @"'
                             where auth_token = '" + token + @"'
                             ";
                DataTable b = db.excuteSQL(query);
                return Ok();
            }
        }


        [Route("{id}/photo")]
        [HttpPost]
        public JsonResult SaveFile(int id)
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                string query = @"update users set 
                             photo ='" + fileName + @"'
                            
                             where user_id = '" + id + @"'
                             ";
                DataTable b = db.excuteSQL(query);

                return new JsonResult(fileName);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

    }
}
