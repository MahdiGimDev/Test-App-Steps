using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Test_App_Steps.Server.Models;
using Microsoft.AspNetCore.Authorization;

namespace Test_App_Steps.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpGet]
       //[Authorize(Roles ="admin" )]
        public JsonResult Get()
        {
            string query = @"
              select userId,userName,userRole,userEmail,userPassword from UserLibrary
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using(MySqlConnection mycon=new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using(MySqlCommand myCommand = new MySqlCommand(query, mycon)) 
                {
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table); 
        }
            

        [HttpPost]
       // [Authorize(Roles = "admin")]
        public JsonResult Post(User user)
        {
            string query = @"
              insert into UserLibrary (userName,userRole,userEmail,userPassword) values(@userName,@userRole,@userEmail,@userPassword);
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@userName", user.userName);
                    myCommand.Parameters.AddWithValue("@userEmail", user.userEmail);
                    myCommand.Parameters.AddWithValue("@userPassword", user.userPassword);
                    myCommand.Parameters.AddWithValue("@userRole", user.userRole);
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added succesfuly");
        }


        [HttpPut]
        //[Authorize(Roles ="admin" )]
        public JsonResult Put(User user)
        {
            string query = @"
              update userlibrary set userName = @userName, userEmail = @userEmail, userRole = @userRole, userPassword = @userPassword where UserId=@UserId;
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@userId", user.userId);
                    myCommand.Parameters.AddWithValue("@userName", user.userName);
                    myCommand.Parameters.AddWithValue("@userEmail", user.userEmail);
                    myCommand.Parameters.AddWithValue("@userRole", user.userRole);
                    myCommand.Parameters.AddWithValue("@userPassword", user.userPassword);
                    
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Updated succesfuly");
        }


        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        public JsonResult Delete(int id)
        {
            string query = @"
              delete from userlibrary  where userId=@userId;
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@userId", id);
                    
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Deleted succesfuly");
        }

    }
}
