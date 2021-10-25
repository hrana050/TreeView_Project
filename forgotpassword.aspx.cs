using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.IO;
using System.Drawing;
using System.Web.Services;

public partial class forgotpassword : System.Web.UI.Page
{
    string constr = "";
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    Hashtable hash;
    protected void page_Init()
    {
        constr = ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        da = new SqlDataAdapter();
        cmd = new SqlCommand();
        hash = new Hashtable();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
     [WebMethod]
    public static string CheckemailAvailability(string usercontact)
    {
        string retval = "";
        string conString = ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString;
        SqlConnection conn = new SqlConnection(conString);
        SqlCommand cmd = new SqlCommand("CheckemailidAvailability", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@emailid", usercontact);
        conn.Open();
        int HasRow = (int)cmd.ExecuteScalar();
        if (HasRow > 0)
        {
            retval = "true";
        }
        else
        {
            retval = "false";
        }
        return retval;
    }
     protected void btn_forgot_Click(object sender, EventArgs e)
     {
           SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("getemailiddetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@emailid", txt_emailid.Text);
            con.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                Session["emailidsno"] = dt.Rows[0]["LoginID"].ToString();
                Response.Redirect("recoverypassword.aspx");
            }
    }
   
}