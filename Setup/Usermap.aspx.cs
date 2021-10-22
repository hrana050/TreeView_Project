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


public partial class Setup_Usermap : System.Web.UI.Page
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
                   // Bindgrid();
                    userlist();
                    filelist();
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

    public enum MenuType
    {
        All = 0,
        SetUp = 1,
        ImportData = 2,
        Actions = 3,
        Reports = 4
    }


    //public void Bindgrid()
    //{
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(constr);
    //        cmd = new SqlCommand("ManageLevel", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@LevelName", null);
    //        cmd.Parameters.AddWithValue("@User", null);
    //        cmd.Parameters.AddWithValue("@Type", "GetRecords");
    //        con.Open();
    //        DataTable dt = new DataTable();
    //        da = new SqlDataAdapter(cmd);
    //        da.Fill(dt);
    //        con.Close();
    //        grdrecord.DataSource = dt;
    //        grdrecord.DataBind();

    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
    //    }

    //}

    public void Clear()
    {
        try
        {
            ddl_file.ClearSelection();
            ddl_user.ClearSelection();
            ddl_assign.ClearSelection();
            ViewState["mapid"] = null;
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
            cmd = new SqlCommand("ManageLevel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LevelName", ddl_file.SelectedValue);
            cmd.Parameters.AddWithValue("@Levelvalue", ddl_assign.SelectedValue);
            cmd.Parameters.AddWithValue("@Leveltitle", ddl_user.Text);

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
               // Bindgrid();
                Clear();
                successdiv.Visible = true;
                errordiv.Visible = false;
                // ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('Record Saved Sucessfully.');", true);
            }
            else
            {
                errordiv.Visible = true;
                successdiv.Visible = false;
              //  lblerrormsg.Text = select.Text;
                //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + txt_year.Text + " Already Exist.');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }
    public void userlist()
    {
        try
        {
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("Manageuserslinkfile", con);
            cmd.Parameters.AddWithValue("@User", null);
            cmd.Parameters.AddWithValue("@Type", "Getuserrecord");
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            ddl_user.DataSource = dt;
            ddl_user.DataTextField = "username";
            ddl_user.DataValueField = "loginid";
            ddl_user.DataBind();
            ddl_user.Items.Insert(0, new ListItem("Select User", "0"));
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }
    public void filelist()
    {
        try
        {
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("Manageuserslinkfile", con);
            cmd.Parameters.AddWithValue("@User", null);
            cmd.Parameters.AddWithValue("@Type", "Getfilerecord");
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            ddl_file.DataSource = dt;
            ddl_file.DataTextField = "file_names";
            ddl_file.DataValueField = "fileid";
            ddl_file.DataBind();
            ddl_file.Items.Insert(0, new ListItem("Select File", "0"));
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
                cmd.Parameters.AddWithValue("@Type", "GetRecords");
                con.Open();
                DataTable dt = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                ddl_user.SelectedValue = dt.Rows[0]["LevelName"].ToString();
                ddl_user.SelectedValue = dt.Rows[0]["LevelName"].ToString();
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
           // Bindgrid();

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
      //  grdrecord.PageIndex = e.NewPageIndex;
      //  this.Bindgrid();
    }
}