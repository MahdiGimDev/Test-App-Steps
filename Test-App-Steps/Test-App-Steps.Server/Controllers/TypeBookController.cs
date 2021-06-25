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

namespace Test_App_Steps.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeBookController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public TypeBookController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
              select typeId,typeName from typebook
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))

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
        public JsonResult Post(TypeBook type)
        {
            string query = @"
              insert into typebook (typeName) values(@typeName);
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {

                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@typeName", type.typeName);
                 
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Type book Added succesfuly");
        }


        [HttpPut]
        public JsonResult Put(TypeBook type)
        {
            string query = @"
              update typebook set typeName = @typeName  where typeId=@typeId;
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@typeId", type.typeId);
                    myCommand.Parameters.AddWithValue("@typeName", type.typeName);
                   
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated type book succesfuly");

        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
              delete from typebook  where typeId=@typeId;
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {

                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@typeId", id);

                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }

            }

            return new JsonResult("Deleted type book succesfuly");

        }












    }
}

 