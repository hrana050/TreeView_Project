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


public partial class Setup_Createuser : System.Web.UI.Page
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
            cmd = new SqlCommand("Manageusers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", null);
            cmd.Parameters.AddWithValue("@User", null);
            cmd.Parameters.AddWithValue("@Type", "GetRecords");
            con.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            grdrecord.DataSource = dt;
            grdrecord.DataBind();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }

    }

    public void Clear()
    {
        try
        {
            ddl_status.ClearSelection();
            ddl_usertype.ClearSelection();
            txt_name.Text = string.Empty;
            txt_pwd.Text = string.Empty;
            txt_emaild.Text = string.Empty;
            txt_contact.Text = string.Empty;
            ViewState["userid"] = null;
            btn_save.Text = "Save";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            hash = new Hashtable();
            hash = (Hashtable)Session["User"];
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("Manageusers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", txt_name.Text);
            cmd.Parameters.AddWithValue("@password", txt_pwd.Text);
            cmd.Parameters.AddWithValue("@emailid", txt_emaild.Text);
            cmd.Parameters.AddWithValue("@contactno", txt_contact.Text);
            cmd.Parameters.AddWithValue("@u_role", ddl_usertype.SelectedValue);
            cmd.Parameters.AddWithValue("@status", ddl_status.SelectedValue);
            cmd.Parameters.AddWithValue("@User", Convert.ToString(hash["Name"].ToString()));

            if (ViewState["userid"] == null)
            {
                cmd.Parameters.AddWithValue("@userid", 0);
                cmd.Parameters.AddWithValue("@Type", "Save");
            }
            else
            {
                cmd.Parameters.AddWithValue("@userid", ViewState["userid"]);
                cmd.Parameters.AddWithValue("@Type", "Update");
            }
            con.Open();

            int HasRow = (int)cmd.ExecuteScalar();
            con.Close();
            if (HasRow == 1)
            {
                Bindgrid();
                Clear();
                successdiv.Visible = true;
                errordiv.Visible = false;
                // ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('Record Saved Sucessfully.');", true);
            }
            else
            {
                errordiv.Visible = true;
                successdiv.Visible = false;
                lblerrormsg.Text = txt_name.Text;
                //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + txt_year.Text + " Already Exist.');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }
 
    protected void grdrecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "lnkEdit")
            {
                SqlConnection con = new SqlConnection(constr);
                cmd = new SqlCommand("Manageusers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", e.CommandArgument);
                cmd.Parameters.AddWithValue("@Type", "GetRecords");
                con.Open();
                DataTable dt = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                txt_name.Text = dt.Rows[0]["UserName"].ToString();
                txt_pwd.Text = dt.Rows[0]["Password"].ToString();
                txt_emaild.Text = dt.Rows[0]["emailid"].ToString();
                txt_contact.Text = dt.Rows[0]["contactno"].ToString();
                ddl_usertype.SelectedValue = dt.Rows[0]["u_role"].ToString();
                if( Convert.ToString(dt.Rows[0]["IsActive"])=="Active")
                {
                    ddl_status.SelectedValue = "1";
                }
                else
                {
                    ddl_status.SelectedValue = "0";
                }
               // ddl_status.SelectedValue = dt.Rows[0]["IsActive"].ToString();
                ViewState["userid"] = e.CommandArgument;
                btn_save.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
            Bindgrid();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdrecord.PageIndex = e.NewPageIndex;
        this.Bindgrid();
    }
    [WebMethod]
    public static string CheckuserAvailability(string usercontact)
    {
        string retval = "";
        string conString = ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString;
        SqlConnection conn = new SqlConnection(conString);
        SqlCommand cmd = new SqlCommand("CheckuserAvailability", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@contactno", usercontact);
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
}