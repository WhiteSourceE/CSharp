using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WsBenchmark.Controllers
{
    public class InterController : Controller
    {
        private MyContext _context = new MyContext(new DbContextOptions<MyContext>());
        private string _sConnect = @"SERVER = .; DATABASE = MYDB; INTEGRATED SECURITY = TRUE";
        private Random _rand = new Random();

        // GET
        public string Index()
        {
            return "InterProceduralController";
        }

        private SqlCommand GetCommandSafe(string id, SqlConnection connection)
        {
            string query = "SELECT * FROM Users WHERE Id = @id";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.Add("@id", SqlDbType.Text);
            sqlCommand.Parameters["@id"].Value = id;
            return sqlCommand;
        }
        
        private SqlCommand GetCommandUnsafe(string id, SqlConnection connection)
        {
            string query = "SELECT * FROM Users WHERE Id = '" + id + "'";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            return sqlCommand;
        }
        
        [HttpGet]
        [Route("inter/1_safe/{id}")]
        public string Inter1Safe(string id)
        {
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                sqlConnection.Open();
                SqlCommand sqlCommand = GetCommandSafe(id, sqlConnection);
                query = sqlCommand.CommandText;
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
            }
            return query;
        }
        
        [HttpGet]
        [Route("inter/1_unsafe/{id}")]
        public string Inter1Unsafe(string id)
        {
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                sqlConnection.Open();
                SqlCommand sqlCommand = GetCommandUnsafe(id, sqlConnection);
                query = sqlCommand.CommandText;
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
            }
            return query;
        }
        
        [HttpGet]
        [Route("inter/2_unsafe/{id}")]
        public string Inter2Unsafe(string id)
        {
            string Pad(string s) => "_" + s;
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                query = "SELECT * FROM Users WHERE Id = '" + Pad(id) + "'";
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
        [Route("inter/2_safe/{id}")]
        public string Inter2Safe(string id)
        {
            string Clear(string s) => "guest";
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                query = "SELECT * FROM Users WHERE Id = '" + Clear(id) + "'";
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
        [Route("inter/3_unsafe/{id}")]
        public string Inter3Unsafe(string id)
        {
            Base myBase = new ChildUnsafe(id);
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                sqlConnection.Open();
                SqlCommand sqlCommand = myBase.GetCommand(id, sqlConnection);
                query = sqlCommand.CommandText;
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
            }
            return query;
        }
        
        [HttpGet]
        [Route("inter/3_safe/{id}")]
        public string Inter3Safe(string id)
        {
            Base myBase = new ChildSafe(id);
            string query = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_sConnect);
                sqlConnection.Open();
                SqlCommand sqlCommand = myBase.GetCommand(id, sqlConnection);
                query = sqlCommand.CommandText;
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ignore)
            {
            }
            return query;
        }
        
    }

    public abstract class Base
    {
        public string Id { get; set; }

        public abstract SqlCommand GetCommand(string id, SqlConnection connection);
    }

    public class ChildSafe : Base
    {
        public ChildSafe(string id)
        {
            Id = id;
        }

        public override SqlCommand GetCommand(string id, SqlConnection connection)
        {
            string query = "SELECT * FROM Users WHERE Id = @id";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.Add("@id", SqlDbType.Text);
            sqlCommand.Parameters["@id"].Value = id;
            return sqlCommand;
        }
    }
    
    public class ChildUnsafe : Base
    {
        public ChildUnsafe(string id)
        {
            Id = id;
        }

        public override SqlCommand GetCommand(string id, SqlConnection connection)
        {
            string query = "SELECT * FROM Users WHERE Id = '" + id + "'";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            return sqlCommand;
        }
    }
}