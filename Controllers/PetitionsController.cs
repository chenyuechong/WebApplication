using Microsoft.AspNetCore.Hosting;
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
    public class PetitionsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private DBOperate db;
        public PetitionsController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            db = new DBOperate(_configuration);
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "";
            return new JsonResult(db.excuteSQL(query));
        }
        
        [HttpPost]
        public JsonResult Post(Petition petition)
        {
            string query = @"insert into Petition 
                            (title, description, category_id, created_date,closing_date)
                            values(
                            '" + petition.title + @"'
                            ,'" + petition.description + @"'
                            ,'" + petition.categoryId + @"'
                            ,'" + new System.DateTime() + @"'
                            ,'" + petition.closingDate + @"'
                            
                            )
                            ";
            return new JsonResult(db.excuteSQL(query));
        }

        [Route("categories")]
        [HttpGet]
        public JsonResult categories()
        {
            string query = @"select category_Id as categoryId,name from dbo.category";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int Id)
        {
            string query = @"delete from dbo.Petition 
                             where petition_id = '" + Id + @"'
                             ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Delete Successfully");
        }


        [HttpGet("{id}")]
        public JsonResult Get(int Id)
        {
            string query = @"select Petition.petition_id as 'petitionId', Petition.title, Category.name as 'category', 
                                Users.name as 'authorName', 
                                (SELECT COUNT(*) FROM Signature where Signature.petition_id = Petition.petition_id) as 'signatureCount', 
                                Petition.description, Users.user_id as 'authorId', 
                                Users.city as 'authorCity', Users.country as 'authorCountry',
                                Petition.created_date as 'createdDate',
                                Petition.closing_date as 'closingDate' from Petition  
                                left join Category on Petition.category_id = Category.category_id 
                                left join Users on Petition.author_id = Users.user_id  
                                left join Signature on Petition.petition_id = Signature.petition_id 
                                where Petition.petition_id = '" + Id + @"'
                                ";
            

            return new JsonResult(db.excuteSQL(query));
        }

        [HttpPatch("{id}")]
        public JsonResult Patch(int Id, [FromBody]Petition petition)
        {
            string query = @"update dbo. Petition set
                             title ='" + petition.title + @"'
                             ,description ='" + petition.description + @"'
                             ,categoryId ='" + petition.categoryId + @"'
                             ,closingdate ='" + petition.closingDate + @"'
                             where petition_id = '" + Id + @"'
                             ";
            return new JsonResult(db.excuteSQL(query));
        }
    
    
        [HttpGet("{id}/signatures")]
        public JsonResult GetSignatures(int Id)
        {
            string query = @"select Signature.signatory_id as signatoryId,
                                Users.name as name,Users.city,Users.country,
                                Signature.signed_date as signedDate 
                                from Signature 
                                left join  User on Signature.signatory_id = Users.user_id 
                               where Signature.petition_id ='"
                                + Id + @"' order by Signature.signed_date";
            return new JsonResult(db.excuteSQL(query));
        }

        [HttpPost("{id}/signatures")]
        public JsonResult PostSignatures(int Id)
        {
            string query = @"select Signature.signatory_id as signatoryId,
                                Users.name as name,Users.city,Users.country,
                                Signature.signed_date as signedDate 
                                from Signature 
                                left join  User on Signature.signatory_id = Users.user_id 
                               where Signature.petition_id ='"
                                + Id + @"' order by Signature.signed_date";
            return new JsonResult(db.excuteSQL(query));
        }

        [HttpDelete("{id}/signatures")]
        public JsonResult DeleteSignatures(int Id)
        {
            string query = @"delete 
                                from Signature 
                               where Signature.petition_id ='"
                                + Id + @"' and signatory_id='"
                                + Id + @"'
                                ";
            return new JsonResult(db.excuteSQL(query));
        }
    }
}


    
