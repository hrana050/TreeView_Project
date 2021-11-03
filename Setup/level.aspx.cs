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

public partial class Setup_level : System.Web.UI.Page
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
                    levelname();
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
        string yearid;
        try
        {
            if (Session["yearid"].ToString().Length > 0)
            {
                yearid = Session["yearid"].ToString();
            }
            else
            {
                yearid = "0";
            }
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("ManageLevel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LevelName", null);
            cmd.Parameters.AddWithValue("@User", null);
            cmd.Parameters.AddWithValue("@yearid", yearid);
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
            ddl_levelvalue.ClearSelection();
            txt_title.Text = string.Empty;
            txt_Lname.Text = string.Empty;
            ViewState["LevelID"] = null;
            btn_save.Text = "Save";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string yearid;
        try
        {
            if (Session["yearid"].ToString().Length > 0)
            {
                yearid = Session["yearid"].ToString();
            }
            else
            {
                 yearid = "0";
            }
            hash = new Hashtable();
            hash = (Hashtable)Session["User"];
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("ManageLevel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LevelName", txt_Lname.Text);
            cmd.Parameters.AddWithValue("@Levelvalue", ddl_levelvalue.SelectedValue);
            cmd.Parameters.AddWithValue("@Leveltitle", txt_title.Text);
            cmd.Parameters.AddWithValue("@yearid", yearid);
            cmd.Parameters.AddWithValue("@User", Convert.ToString(hash["Name"].ToString()));

            if (ViewState["LevelID"] == null)
            {
                cmd.Parameters.AddWithValue("@LevelID", 0);
                cmd.Parameters.AddWithValue("@Type", "Save");
            }
            else
            {
                cmd.Parameters.AddWithValue("@LevelID", ViewState["LevelID"]);
                cmd.Parameters.AddWithValue("@Type", "Update");
            }
            con.Open();

            int HasRow = (int)cmd.ExecuteScalar();
            con.Close();
            if (HasRow == 1)
            {
                Bindgrid();
                levelname();
                Clear();
                successdiv.Visible = true;
                errordiv.Visible = false;
                // ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('Record Saved Sucessfully.');", true);
            }
            else
            {
                errordiv.Visible = true;
                successdiv.Visible = false;
                lblerrormsg.Text = txt_Lname.Text;
                //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + txt_year.Text + " Already Exist.');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }
    public void levelname()
    {
        try
        {
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("ManageLevel", con);
            cmd.Parameters.AddWithValue("@LevelName", null);
            cmd.Parameters.AddWithValue("@User", null);
            cmd.Parameters.AddWithValue("@Type", "GetRecords_year");
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            ddl_levelvalue.DataSource = dt;
            ddl_levelvalue.DataTextField = "Levelname";
            ddl_levelvalue.DataValueField = "Levelid";
            ddl_levelvalue.DataBind();
            ddl_levelvalue.Items.Insert(0, new ListItem("New Base Level", "0"));
            con.Close();
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
                cmd = new SqlCommand("ManageLevel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LevelID", e.CommandArgument);
                cmd.Parameters.AddWithValue("@LevelName", txt_Lname.Text);
                cmd.Parameters.AddWithValue("@Levelvalue", ddl_levelvalue.SelectedValue);
                cmd.Parameters.AddWithValue("@Leveltitle", txt_title.Text);
                cmd.Parameters.AddWithValue("@User", Convert.ToString(hash["Name"].ToString()));
                cmd.Parameters.AddWithValue("@Type", "GetRecords");
                con.Open();
                DataTable dt = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                txt_Lname.Text = dt.Rows[0]["LevelName"].ToString();
                ddl_levelvalue.SelectedValue = dt.Rows[0]["Levelvalue"].ToString();
                txt_title.Text = dt.Rows[0]["Leveltitle"].ToString();
                ViewState["LevelID"] = e.CommandArgument;
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
    public static string  ChecklevelAvailability(string levelname,string id)
    {
        string retval = "";
        string conString = ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString;
         SqlConnection conn = new SqlConnection(conString);
         SqlCommand cmd = new SqlCommand("ChecklevelAvailability", conn);
         cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@levelname", levelname);
                cmd.Parameters.AddWithValue("@yearid", id);
                conn.Open();
                int HasRow = (int)cmd.ExecuteScalar();
                if (HasRow>0)
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