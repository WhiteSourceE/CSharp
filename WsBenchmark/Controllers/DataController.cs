using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WsBenchmark.Controllers
{
    public class DataController : Controller
    {
        
        private MyContext _context = new MyContext(new DbContextOptions<MyContext>());
        private string _sConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        private Random _rand = new Random();
        private string FieldId { get; set; }
        private static string StaticFieldId { get; set; }

        // GET
        public string Index()
        {
            return "DataController";
        }

        [HttpGet]
        [Route("data/setStatic/{id}")]
        public void SetStaticId(string id)
        {
            StaticFieldId = id;
        }
        
        [HttpGet]
        [Route("data/1_safe/{id}")]
        public string Data1Safe(string id)
        {
            List<string> ids = new List<string> {"user_1", "user_2", "user_3", "user_4"};
            ids[3] = id;
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                query = "SELECT * FROM Users WHERE Id = '" + ids[0] + "'";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
                
            }
            return query;
        }
        
        [HttpGet]
        [Route("data/1_unsafe/{id}")]
        public string Data1Unsafe(string id)
        {
            List<string> ids = new List<string> {"user_1", "user_2", "user_3", "user_4"};
            ids[3] = id;
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                query = "SELECT * FROM Users WHERE Id = '" + ids[3] + "'";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
                
            }
            return query;
        }
        
        [HttpGet]
        [Route("data/2_safe/{id}")]
        public string Data2Safe(string id)
        {
            string[] ids = {"user_1", "user_2", "user_3", "user_4"};
            ids[3] = id;
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                query = "SELECT * FROM Users WHERE Id = '" + ids[0] + "'";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
                
            }
            return query;
        }
        
        [HttpGet]
        [Route("data/2_unsafe/{id}")]
        public string Data2Unsafe(string id)
        {
            string[] ids = {"user_1", "user_2", "user_3", "user_4"};
            ids[3] = id;
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                query = "SELECT * FROM Users WHERE Id = '" + ids[3] + "'";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
                
            }
            return query;
        }
        
        [HttpGet]
        [Route("data/3_unsafe/{id}")]
        public string Data3Unsafe(string id)
        {
            Holder holder = new Holder(id);
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                query = "SELECT * FROM Users WHERE Id = '" + holder.HolderId + "'";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
                
            }
            return query;
        }
        
        [HttpGet]
        [Route("data/3_safe/{id}")]
        public string Data3Safe(string id)
        {
            Holder holder = new Holder(id);
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                query = "SELECT * FROM Users WHERE Id = '" + holder.SafeId + "'";
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
                
            }
            return query;
        }
        
        [HttpGet]
        [Route("data/4_unsafe/{id}")]
        public string Data4Unsafe(string id)
        {
            FieldId = id;
            string query = "SELECT * FROM Users WHERE Id = '" + FieldId + "'";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
            }
            return query;
        }
        
        [HttpGet]
        [Route("data/4_safe/{id}")]
        public string Data4Safe(string id)
        {
            FieldId = id;
            string query = "SELECT * FROM Users WHERE Id = @id";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@id", SqlDbType.Text);
                sqlCommand.Parameters["@id"].Value = FieldId;
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
                
            }
            return query;
        }
        
        [HttpGet]
        [Route("data/5_unsafe")]
        public string Data5Unsafe()
        {
            string query = "SELECT * FROM Users WHERE Id = '" + StaticFieldId + "'";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
            }
            return query;
        }
        
        [HttpGet]
        [Route("data/5_safe")]
        public string Data5Safe()
        {
            string query = "SELECT * FROM Users WHERE Id = @id";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@id", SqlDbType.Text);
                sqlCommand.Parameters["@id"].Value = StaticFieldId;
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
                
            }
            return query;
        }
    }

    public class Holder
    {
        public string HolderId { get; set; }
        public string SafeId { get; set; }

        public Holder(string holderId)
        {
            HolderId = holderId;
            SafeId = "guest";
        }
    }
}