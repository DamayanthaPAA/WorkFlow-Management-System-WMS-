using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowMgtSystem.Models;

namespace WorkFlowMgtSystem.Service
{
    class ServiceAutoNumber
    {


        public string getAutoNumber(string strResion)
        {
            Int32 AutoNumberx = 0;
            string AutoNumber = "";
            var db = new SmartCRM();
            var conn = db.Database.Connection;
            var ConnnectionState = conn.State;
            String SqlString = "";
            DataTable dt = new DataTable();
            try
            {



                if (ConnnectionState != ConnectionState.Open) { conn.Open(); }
                ConnnectionState = conn.State;
                using (System.Data.SqlClient.SqlCommand cmd = (System.Data.SqlClient.SqlCommand)conn.CreateCommand())
                {


                    {

                        SqlString = "SELECT  Autonumber  FROM AutoNumber WHERE  (Reason = '" + strResion + "')";

                    }

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlString;

                    if (!String.IsNullOrWhiteSpace(SqlString))
                        cmd.CommandText = SqlString;
                    cmd.CommandType = CommandType.Text;

                    using (var red = cmd.ExecuteReader())
                    {
                        //dt.Load(red);
                        while (red.Read())
                        {
                            String xNo = "";
                            xNo = red[0].ToString();
                            AutoNumberx = Convert.ToInt32(xNo);
                            AutoNumberx = AutoNumberx + 1;
                            AutoNumber = AutoNumberx.ToString();
                        }
                    }

                    if (AutoNumberx == 0)
                    {
                        AutoNumber = "1";
                        SqlString = "INSERT INTO AutoNumber   (Reason, Autonumber, CompanyID, Year) VALUES (@Reason, @Autonumber, @CompanyID, @Year)";

                        cmd.CommandText = SqlString;
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@Reason", strResion);
                        cmd.Parameters.AddWithValue("@Autonumber", 1);
                        cmd.Parameters.AddWithValue("@CompanyID", "00001");
                        cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);

                    }
                    else
                    {
                        SqlString = "UPDATE AutoNumber  SET Autonumber =Autonumber+1 WHERE(Reason = @Reason)";
                        cmd.CommandText = SqlString;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Reason", strResion);
                    }



                    int saved = 0;
                    saved = cmd.ExecuteNonQuery();

                    if (saved != 0) return AutoNumber.ToString(); else return "";
                }
            }
            catch (Exception e)
            {
                return "";
                throw;
            }
            finally
            {
                if (ConnnectionState == ConnectionState.Open) { conn.Close(); }
            }
            


        }
    }
   
}
