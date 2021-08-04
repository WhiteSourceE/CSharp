using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WsBenchmark.Controllers
{
    public class HelloController : Controller
    {
        private MyContext _context = new MyContext(new DbContextOptions<MyContext>());
        private string _sConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        private Random _rand = new Random();

        public HelloController()
        {
        }

        // GET
        public string Index()
        {
            return "my index page";
        }

        [HttpGet]
        [Route("basic/1_unsafe/{id}")]
        public string Basic1Unsafe(string id)
        {
            id = "id_" + id;
            string query = "SELECT * FROM Users WHERE Id = '" + id + "'";
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
        [Route("basic/1_safe/{id}")]
        public string Basic1Safe(string id)
        {
            id = "id_" + id;
            string query = "SELECT * FROM Users WHERE Id = @id";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@id", SqlDbType.Text);
                sqlCommand.Parameters["@id"].Value = id;
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
                
            }
            return query;
        }
        
        [HttpGet]
        [Route("basic/2_safe/{id}")]
        public string Basic2Safe(string id)
        {
            bool b = true;
            string searchId;
            if (b)
            {
                searchId = "guest";
            }
            else
            {
                searchId = id;
            }
            string query = "SELECT * FROM Users WHERE Id = '" + searchId + "'";
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
        [Route("basic/2_unsafe/{id}")]
        public string Basic2Unsafe(string id)
        {
            bool b = false;
            string searchId;
            if (b)
            {
                searchId = "guest";
            }
            else
            {
                searchId = id;
            }
            string query = "SELECT * FROM Users WHERE Id = '" + searchId + "'";
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
        [Route("basic/3_unsafe/{id}")]
        public string Basic3Unsafe(string id)
        {
            var searchId = _rand.NextDouble() < 0.5 ? "guest" : id;
            string query = "SELECT * FROM Users WHERE Id = '" + searchId + "'";
            try
            {
                SqlConnection _sqlConnection = new SqlConnection(_sConnect);
                _sqlConnection.Open();
                SqlCommand _sqlCommand = new SqlCommand(query, _sqlConnection);
                _sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();
            }
            catch (Exception ignore)
            {
            }
            return query;
        }
        
        [HttpGet]
        [Route("basic/4_safe/{id}")]
        public string Basic4Safe(string id)
        {
            string searchId = "guest";
            for (int i = 0; i < 0; i++)
            {
                searchId = "id_" + id;
            }
            string query = "SELECT * FROM Users WHERE Id = '" + searchId + "'";
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
        [Route("basic/4_unsafe/{id}")]
        public string Basic4Unsafe(string id)
        {
            string searchId = "guest";
            for (int i = 0; i < 1; i++)
            {
                searchId = "id_" + id;
            }
            string query = "SELECT * FROM Users WHERE Id = '" + searchId + "'";
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
        [Route("basic/5_unsafe/{id}")]
        public string Basic5Unsafe(string id)
        {
            string searchId = "guest";
            while (_rand.NextDouble() < 0.5)
            {
                searchId = id;
            }
            string query = "SELECT * FROM Users WHERE Id = '" + searchId + "'";
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
        [Route("constant/1_unsafe/{id}")]
        public string Constant1Unsafe(string id)
        {
            string searchId = "guest";
            if (true)
            {
                searchId = id;
            }
            string query = "SELECT * FROM Users WHERE Id = '" + searchId + "'";
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
        [Route("constant/1_safe/{id}")]
        public string Constant1Safe(string id)
        {
            string searchId = "guest";
            if (false)
            {
                searchId = id;
            }
            string query = "SELECT * FROM Users WHERE Id = '" + searchId + "'";
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
        [Route("try/1_safe/{id}")]
        public string Try1Safe(string id)
        {
            string searchId = "guest";
            try
            {
                searchId = id;
                throw new Exception();
            }
            catch (Exception exception)
            {
                searchId = "guest";
            }
            string query = "SELECT * FROM Users WHERE Id = '" + searchId + "'";
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
        [Route("try/1_unsafe/{id}")]
        public string Try1Unsafe(string id)
        {
            string searchId = "guest";
            try
            {
                var path = Path.Combine(Path.GetTempPath(), "myTempFile.txt");
                System.IO.File.Delete(path);
                searchId = "guest";
            }
            catch (Exception exception)
            {
                searchId = id;
            }
            string query = "SELECT * FROM Users WHERE Id = '" + searchId + "'";
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
        
    }
}