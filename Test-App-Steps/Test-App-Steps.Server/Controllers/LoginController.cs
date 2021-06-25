using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test_App_Steps.Server.Models;

namespace Test_App_Steps.Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;





        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;

        }




        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel userL)
        {
            if (userL == null)

                return BadRequest("Invalid Client request");
            string query = @"
            select * from userlibrary where userName=@userName and userPassword=@password
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))

                {
                    myCommand.Parameters.AddWithValue("@userName", userL.userName);
                    myCommand.Parameters.AddWithValue("@password", userL.password);
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();

                    mycon.Close();
                }
            }


            if (table != null)
            {





                if (userL.userName == "mahdi" && userL.password == "123456")

                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userL.userName),
                    new Claim(ClaimTypes.Role,"admin")
                };





                    var tokenOptions = new JwtSecurityToken(
                          issuer: "https://localhost:44366",
                        audience: "https://localhost:44366",

                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signingCredentials

                        );




                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new { Token = tokenString });

                }




                if (userL.userName != "mahdi" && userL.password != "123456")

                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userL.userName),
                    new Claim(ClaimTypes.Role,"employee")
                };





                    var tokenOptions = new JwtSecurityToken(
                          issuer: "https://localhost:44366",
                        audience: "https://localhost:44366",

                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signingCredentials

                        );




                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new { Token = tokenString });

                }





            }
            return Unauthorized();






        }



    }
}
