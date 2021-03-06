using API_DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace API_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
            private readonly IConfiguration _configration;

        public EmployeeController(IConfiguration configuration)
            {
                _configration = configuration;
            }

            [HttpGet]
            public JsonResult Get()
            {
                string query = @" select EmployeeId, EmployeeName, Department, convert(varchar(10), DateOfJoining,120) as DateOfJoining, PhoteFileName from dbo.Employee";

                DataTable table = new DataTable();
                string sqlDataSource = _configration.GetConnectionString("EmployeeAppCon");
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

            [HttpPost]
            public JsonResult Post(Employee emp)
            {
                string query = @" insert into dbo.Employee(EmployeeName, Department, DateOfJoining, PhoteFileName) values (@EmployeeName, @Department, @DateOfJoining, @PhoteFileName)";

                DataTable table = new DataTable();
                string sqlDataSource = _configration.GetConnectionString("EmployeeAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();

                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                        myCommand.Parameters.AddWithValue("@Department", emp.Department);
                        myCommand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                        myCommand.Parameters.AddWithValue("@PhoteFileName", emp.PhoteFileName);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Added Successfully");
            }

            [HttpPut]
            public JsonResult Put(Employee emp)
            {
                string query = @" update dbo.Employee set EmployeeName = @EmployeeName, Department = @Department, DateOfJoining = @DateOfJoining, PhoteFileName = @PhoteFileName where EmployeeId = @EmployeeId";

                DataTable table = new DataTable();
                string sqlDataSource = _configration.GetConnectionString("EmployeeAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();

                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                    myCommand.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhoteFileName", emp.PhoteFileName);

                    myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Updated Successfully");
            }

            [HttpDelete("{id}")]
            public JsonResult Delete(int id)
            {
                string query = @" delete from dbo.Employee where EmployeeId = @EmployeeId";

                DataTable table = new DataTable();
                string sqlDataSource = _configration.GetConnectionString("EmployeeAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();

                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@EmployeeId", id);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Deleted Successfully");
            }
        
    }
}
