﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace todoappnew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private IConfiguration _configuration;

        public TodoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetNotes")]

        public JsonResult GetNotes()
        {
            string query = "select * from dbo.Notes";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query,myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        [Route("AddNotes")]

        public JsonResult AddNotes([FromForm] string newNotes)
        {
            string query = "insert into dbo.Notes values(@newNotes)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@newNotes", newNotes);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpDelete]
        [Route("DeleteNotes")]

        public JsonResult DeleteNotes(int id)
        {
            string query = "delete from dbo.Notes where id=@id";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Delete Successfully");
        }
    }
}
