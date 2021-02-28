using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

/// <summary>
/// Summary description for CRUD
/// </summary>
public class CRUD
{
    public static string myVar = "KSA";
    SqlCommand cmd;
    DataTable dt;
    SqlDataAdapter adp;
    DataSet ds;
    DataView dv;
    // istesd of pasting connection string in each page this refrence the connection on the webconfig  
    public static string  conStr = WebConfigurationManager.ConnectionStrings["conStrIntern"].ConnectionString;
    SqlConnection con = new SqlConnection(conStr);

    public CRUD()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public SqlDataReader getDrPassSql(string mySql, Dictionary<string, object> myPara)
    {
        SqlDataReader dr;
        using (SqlCommand cmd = new SqlCommand(mySql, con))
        {
            foreach (KeyValuePair<string, object> p in myPara)
            {
                // can put validation here to see if the value is empty or not 
                cmd.Parameters.AddWithValue(p.Key, p.Value);
            }
            con.Open();
            dr = cmd.ExecuteReader();
            return dr;
        }
    }
    public SqlDataReader getDrPassSql(string mySql)
    {
        SqlDataReader dr;
        using (SqlCommand cmd = new SqlCommand(mySql, con))
        {
            //foreach (KeyValuePair<string, object> p in myPara)
            //{
            //    // can put validation here to see if the value is empty or not 
            //    cmd.Parameters.AddWithValue(p.Key, p.Value);
            //}
            con.Open();
            dr = cmd.ExecuteReader();
            return dr;
        }
    }
    public int InsertUpdateDelete(string sqlCmd)
    {
        int rtn = 0;
        using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
        {
            cmd.CommandType = CommandType.Text;
            using (con)
            {
                con.Open();
                rtn = cmd.ExecuteNonQuery();  // -1    > = 1 
                con.Close();
            }
        }
        return rtn;
    }
    public int InsertUpdateDelete(string sqlCmd, Dictionary<string, object> myPara)
    {
        int rtn = 0; 
              using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
            {
                cmd.CommandType = CommandType.Text;
                foreach (KeyValuePair<string, object> p in myPara)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
                using (con)
                {
                    con.Open();
                rtn= cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        return rtn;
    }
}



