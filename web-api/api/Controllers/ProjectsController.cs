using System.Collections.Generic;
using System.Web.Http;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace api.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ApiController
    {
        private static string connString;

        [HttpGet]
        public IEnumerable<Project> Get()
        {
            using (var conn = new SqlConnection(Db.ConnectionString))
            {
                return conn.Query<Project>("SELECT id,name FROM Projects");
            }
        }

        [HttpPost]
        public int Post([FromBody] CreateProjectRequest req)
        {
            const string sql = 
                @"INSERT INTO Projects(name) VALUES(@name);
                  SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var conn = new SqlConnection(Db.ConnectionString))
            {
                return conn.Query<int>(sql, new {req.name}).Single();
            }
        }
    }

    public class CreateProjectRequest
    {
        public string name { get; set; }
    }

    public class Project
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
