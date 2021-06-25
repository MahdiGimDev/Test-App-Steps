using Microsoft.AspNetCore.Authorization;
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
    public class BookController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public BookController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
              select bookId,bookName,bookAuthor,bookType from book
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



       /* [HttpGet("{id}")]
        public JsonResult GetByAuthor(int id)
        {
            string query = @"
              select bookId,bookName,bookAuthor from book where authorId=@AuthorId;
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))

                {
                    myCommand.Parameters.AddWithValue("@AuthorId", id);

                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();

                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }*/





        [HttpPost]
        public JsonResult Post(Book book)
        {
            string query = @"
              insert into book (bookName,bookAuthor,bookType) values(@bookName,@bookAuthor,@bookType);
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {

                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@bookName", book.bookName);
                    myCommand.Parameters.AddWithValue("@bookAuthor", book.bookAuthor);
                    myCommand.Parameters.AddWithValue("@bookType", book.bookType);
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("book Added succesfuly");
        }


        [HttpPut]
        public JsonResult Put(Book book)
        {
            string query = @"
              update book set bookName = @bookName,bookType = @bookType, bookAuthor = @bookAuthor  where bookId=@bookId;
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@bookId", book.bookId);
                    myCommand.Parameters.AddWithValue("@bookName", book.bookName);
                    myCommand.Parameters.AddWithValue("@bookAuthor", book.bookAuthor);
                   myCommand.Parameters.AddWithValue("@bookType", book.bookType);

                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated book succesfuly");

        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
              delete from Book  where bookId=@bookId;
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {

                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@bookId", id);

                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }

            }

            return new JsonResult("Deleted book succesfuly");

        }



        [Route("GetAllAuthorNames")]
        public JsonResult GetAllAuthorNames()
        {

            string query = @"
              select authorName from author
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







        [Route("GetAllTypes")]
        public JsonResult GetAllTypesNames()
        {

            string query = @"
              select typeName from typebook
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





    }
}
