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


public partial class Home_Home : System.Web.UI.Page
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
        try
        {
            if (Session["User"] != null)
            {
                hash = (Hashtable)Session["User"];
                if (!IsPostBack)
                {
                    Bindgrid();
                    Bindgrid_file();
                    if (Session["ParentID"] != null && Session["ParentID"].ToString() == "0")
                    {
                        admindashboard.Visible = true;
                    }
                    else
                    {
                        admindashboard.Visible = false;
                    }
                
                }

            }
            else
            {
                Response.Redirect("../Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }
    public void Bindgrid()
    {
        try
        {
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("getdashboard", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            if(dt.Rows.Count>0)
            {
                lblactivestaff.Text = dt.Rows[0]["activestaff"].ToString();
                lbldeactivestaff.Text = dt.Rows[0]["deactivestaff"].ToString();
                lbl_staff.Text = Convert.ToString(Convert.ToInt32(lblactivestaff.Text) + Convert.ToInt32(lbldeactivestaff.Text));

                lblactivelevel.Text = dt.Rows[0]["activelevel"].ToString();
                lbldeactivelevel.Text = dt.Rows[0]["deactivelevel"].ToString();
                lbllevel.Text = Convert.ToString(Convert.ToInt32(lblactivelevel.Text) + Convert.ToInt32(lbldeactivelevel.Text));

                lblactivefile.Text = dt.Rows[0]["activefile"].ToString();
                lbldeactivefile.Text = dt.Rows[0]["deactivefile"].ToString();
                lblfile.Text = Convert.ToString(Convert.ToInt32(lblactivefile.Text) + Convert.ToInt32(lbldeactivefile.Text));

                grdrecord.DataSource = dt;
                grdrecord.DataBind();

            }
                                                                                                                                                                                                                         

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }

    }
    public void Bindgrid_file()
    {
        try
        {
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("getdashboardfilelist", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {

                grdfile.DataSource = dt;
                grdfile.DataBind();

            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }

    }
  
}