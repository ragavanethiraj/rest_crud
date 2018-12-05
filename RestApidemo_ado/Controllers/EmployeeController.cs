using System;
using System.Collections.Generic;
using System.Web.Http;
using RestApidemo_ado.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace RestApidemo_ado.Controllers
{
    public class EmployeeController : ApiController
    {
        #region Defining Sqlconnection
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        #endregion

        #region HttpGet all Employee details
        [HttpGet]
        [ActionName("GetEmployee")]
        public List<Employee> Get()
        {       
            #region GetConnection          
            SqlConnection myConnection = new SqlConnection(constr);
            #endregion
                        
            List<Employee> EmpList = new List<Employee>();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Rest_demo";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.SingleResult);                     
            while (reader.Read())
            {
               Employee emp = new Employee();
                emp.Id =Guid.Parse(Convert.ToString( reader["Id"]));
                emp.ApplicationId =Convert.ToInt32( reader["ApplicationId"]); 

                emp.Type = Convert.ToString(reader["Type"]);
                emp.Summary= Convert.ToString(reader["Summary"]);
                emp.Amount = Convert.ToInt32(reader["Amount"]);
                emp.PostingDate = Convert.ToDateTime(reader["PostingDate"]);
                emp.IsCleared = Convert.ToString(reader["IsCleared"]);
                emp.ClearedDate= Convert.ToDateTime(reader["ClearedDate"]);
                EmpList.Add(emp);
            }
            return EmpList;
            myConnection.Close();
        }
        #endregion

        #region HttpGet Spcific Employee details
        [HttpGet]
        [ActionName("GetEmployeeById")]
        public List<Employee> Getbyid(Guid ?Id)
        {
            #region GetConnection          
            SqlConnection myConnection = new SqlConnection(constr);
            #endregion

            List<Employee> EmpList = new List<Employee>();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Rest_demo where Id='" + Id + "'";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.SingleResult);
            while (reader.Read())
            {
                Employee emp = new Employee();
                emp.Id = Guid.Parse(Convert.ToString(reader["Id"]));
                emp.ApplicationId = Convert.ToInt32(reader["ApplicationId"]);

                emp.Type = Convert.ToString(reader["Type"]);
                emp.Summary = Convert.ToString(reader["Summary"]);
                emp.Amount = Convert.ToInt32(reader["Amount"]);
                emp.PostingDate = Convert.ToDateTime(reader["PostingDate"]);
                emp.IsCleared = Convert.ToString(reader["IsCleared"]);
                emp.ClearedDate = Convert.ToDateTime(reader["ClearedDate"]);
                EmpList.Add(emp);
            }
            return EmpList;
            myConnection.Close();
        }
        #endregion

        #region HttpPost Create new employ details
        [HttpPost]
        public void AddEmployee(Employee employee)
        {
            #region Get SQL Connection          
            SqlConnection myConnection = new SqlConnection(constr);
            #endregion

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText =
                "insert into Rest_demo(Id, ApplicationId, Type, Summary, Amount,PostingDate, IsCleared, ClearedDate) Values (@Id, @ApplicationId, @Type, @Summary, @Amount, @PostingDate, @IsCleared, @ClearedDate)";
            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@Id", employee.Id);
            sqlCmd.Parameters.AddWithValue("@ApplicationId",employee.ApplicationId );                           
            sqlCmd.Parameters.AddWithValue("@Type", employee.Type);
            sqlCmd.Parameters.AddWithValue("@Summary", employee.Summary);
             sqlCmd.Parameters.AddWithValue("@Amount", employee.Amount);
            sqlCmd.Parameters.AddWithValue("@PostingDate", employee.PostingDate);
            sqlCmd.Parameters.AddWithValue("@IsCleared", employee.IsCleared);
            sqlCmd.Parameters.AddWithValue("@ClearedDate", employee.ClearedDate);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
        #endregion

        #region HttpDelete Delete specific Employ Details by Id
        [ActionName("DeleteEmployee")]        
        public void DeleteEmployeeByID(Guid? Id)
        {
            SqlConnection myConnection = new SqlConnection(constr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from  rest_demo where Id='" + Id+"'";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
             sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
        #endregion

        #region HttpPut Update specific Employ details by Id
        [ActionName("Update")]
        [HttpPut]
        public void UpdateEmployee(Guid? Id)
        {
            SqlConnection myConnection = new SqlConnection(constr);

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "update  rest_demo set Type='credit' where Id='" + Id + "'";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            int rowDeleted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
        #endregion

    }
}
