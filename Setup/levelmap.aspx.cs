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
using System.Collections.Generic;


public partial class Setup_levelmap : System.Web.UI.Page
{
    string constr = "";
    string parentid;
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    Hashtable hash;
    string sno;
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
    public void levelname()
    {
        try
        {
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("ManageLevel", con);
            cmd.Parameters.AddWithValue("@LevelName", null);
            cmd.Parameters.AddWithValue("@User", null);
            cmd.Parameters.AddWithValue("@Type", "GetRecords");
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            ddl_levelname.DataSource = dt;
            ddl_levelname.DataTextField = "Levelname";
            ddl_levelname.DataValueField = "Levelid";
            ddl_levelname.DataBind();
            ddl_levelname.Items.Insert(0, new ListItem("New Base Level", "0"));
            con.Close();
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


    public void Bindgrid()
    {
        try
        {
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("ManageLevelMap", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (Session["ParentID"] != null && Session["ParentID"].ToString() == "0")
            {
                cmd.Parameters.AddWithValue("@Type", "GetRecords_admin");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Type", "GetRecords");
            }
            if (Session["LoginID"].ToString() != null)
            {
                sno = Session["LoginID"].ToString();
            }
            cmd.Parameters.AddWithValue("@fileaccess", sno);
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
            ddl_levelname.ClearSelection();
            txt_filename.Text = "";
            ViewState["fileid"] = null;
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
            if(Session["LoginID"] !=null)
            {
                 sno = Session["LoginID"].ToString();
            }
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("ManageLevelMap", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@levelId", ddl_levelname.SelectedValue);
            cmd.Parameters.AddWithValue("@User", Convert.ToString(hash["Name"].ToString()));
            cmd.Parameters.AddWithValue("@fileaccess", sno);
            if (ViewState["fileid"] == null)
            {
                cmd.Parameters.AddWithValue("@fileid", 0);
                if (FileUpload1.HasFile)
                {
                    cmd.Parameters.AddWithValue("@Type", "Save");
                  
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('Please Choose File');", true);
                    successdiv.Visible = false;
                }
            }
            else
            {
             
                    cmd.Parameters.AddWithValue("@filename", txt_filename.Text);
                    cmd.Parameters.AddWithValue("@filepath", ViewState["filepath"]);
                    cmd.Parameters.AddWithValue("@fileid", ViewState["fileid"]);
                    cmd.Parameters.AddWithValue("@Type", "Update");
            }
            con.Open();

            int HasRow = (int)cmd.ExecuteScalar();
            updateimage(HasRow);
            con.Close();
            if (HasRow > 0)
            {
                Bindgrid();
                Clear();
                successdiv.Visible = true;
                errordiv.Visible = false;
            }
            else
            {
                errordiv.Visible = true;
                successdiv.Visible = false;
               
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }

    public void updateimage(int id)
    {
        string strFileName;
        if (FileUpload1.HasFile)
        {
            string OriginalfileName = Convert.ToString(FileUpload1.FileName);
            string strFileType = System.IO.Path.GetExtension(OriginalfileName).ToString().ToLower();
            strFileName = (id + strFileType);
            FileUpload1.SaveAs(Server.MapPath(("~/UpLoad/" + (strFileName))));
            if (Session["LoginID"] != null)
            {
                sno = Session["LoginID"].ToString();
            }
            hash = new Hashtable();
            hash = (Hashtable)Session["User"];
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("ManageLevelMap", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@levelId", ddl_levelname.SelectedValue);
            cmd.Parameters.AddWithValue("@fileaccess", sno);
            cmd.Parameters.AddWithValue("@User", Convert.ToString(hash["Name"].ToString()));
            cmd.Parameters.AddWithValue("@filename", txt_filename.Text);
            cmd.Parameters.AddWithValue("@filepath", strFileName);
            cmd.Parameters.AddWithValue("@FileId", id);
            cmd.Parameters.AddWithValue("@Type", "Update");
            con.Open();
            cmd.ExecuteNonQuery();
            Clear();
        }
        else
        {
            if(btn_save.Text=="Save")
            { 
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('PLease select file...');", true);
            }
            else
            {

            }
        }
    }
    protected void grdrecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "lnkEdit")
            {
                SqlConnection con = new SqlConnection(constr);
                cmd = new SqlCommand("ManageLevelMap", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileId", e.CommandArgument);
                cmd.Parameters.AddWithValue("@User", Convert.ToString(hash["Name"].ToString()));
                cmd.Parameters.AddWithValue("@Type", "GetRecords");
                con.Open();
                DataTable dt = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                ddl_levelname.SelectedValue = dt.Rows[0]["uflevelid"].ToString();
                txt_filename.Text = dt.Rows[0]["file_names"].ToString();
                ViewState["filename"] = dt.Rows[0]["file_names"].ToString();
                ViewState["filepath"] = dt.Rows[0]["filepath"].ToString();
                ViewState["fileid"] = e.CommandArgument;
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
   
}