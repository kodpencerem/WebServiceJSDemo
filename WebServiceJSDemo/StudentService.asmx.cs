using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

namespace WebServiceJSDemo
{
    /// <summary>
    /// Summary description for StudentService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class StudentService : System.Web.Services.WebService
    {

        [WebMethod]
        public Student GetStudentById(int ID)
        {
            string conString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using(SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand sqlCommand= new SqlCommand("upGetStudentByID", con);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter sqlParameter = new SqlParameter("@ID",ID);

                sqlCommand.Parameters.Add(sqlParameter);

                Student student = new Student();

                con.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while(sqlDataReader.Read())
                {
                    student.ID = Convert.ToInt32(sqlDataReader["ID"]);
                    student.Name = sqlDataReader["Name"].ToString();
                    student.Gender = sqlDataReader["Gender"].ToString();
                    student.TotalMarks = Convert.ToInt32(sqlDataReader["TotalMarks"]);
                    
                }
                return student;
            }
        }
    }
}
