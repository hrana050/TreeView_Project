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

public partial class Setup_Makeuser : System.Web.UI.Page
{
    string constr = "";
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    Hashtable hash;
    int Count;
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
                    CheckUserRights();
                  
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
    public void CheckUserRights()
    {
        try
        {
            int HasMatch = 0;
            string RequestURL = Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(RequestURL);
            string PageName = oInfo.Name;
            string CheckPageName = "";

            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("GetLoginDetails", con);
            cmd.Parameters.AddWithValue("@UserName", null);
            cmd.Parameters.AddWithValue("@Password", null);
            cmd.Parameters.AddWithValue("@LoginID", Session["LoginID"]);
            cmd.Parameters.AddWithValue("@MenuID", MenuType.All);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            if (ds.Tables[1].Rows.Count > 0)
            {
                int i = 0;

                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    CheckPageName = ds.Tables[1].Rows[i]["PageName"].ToString();
                    if (PageName == CheckPageName)
                    {
                        HasMatch++;
                        break;
                    }

                    i++;
                }

                if (HasMatch > 0)
                {
                    levelPages();
                    userPages();
                    userlist();   
                  
                }
                else
                {
                    Response.Redirect("../NotAuthorized/NotAuthorized.aspx");
                }
            }
            else
            {
                levelPages();
                userPages();
                userlist();   
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }
    public enum Menu
    {
        level = 2,
        user = 1,
      
    }
    public enum MenuType
    {
        level = 1,
        user = 2,
        All = 0,
  
    }
    public void levelPages()
    {
        try
        {
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("ShowPages", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageID", 0);
            cmd.Parameters.AddWithValue("@MenuID", Menu.level);
            con.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            chkbxlevelPageList.DataSource = dt;
            chkbxlevelPageList.DataTextField = "PageTitle";
            chkbxlevelPageList.DataValueField = "PageID";
            chkbxlevelPageList.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }

    public void userPages()
    {
        try
        {
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("ShowPages", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageID", 0);
            cmd.Parameters.AddWithValue("@MenuID", Menu.user);
            con.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            chkbxuserPageList.DataSource = dt;
            chkbxuserPageList.DataTextField = "PageTitle";
            chkbxuserPageList.DataValueField = "PageID";
            chkbxuserPageList.DataBind();
            con.Close();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int CountlevelPageList = 0;
            foreach (ListItem item in chkbxlevelPageList.Items)
            {
                if (item.Selected == true)
                {
                    CountlevelPageList++;
                }
            }

            int CountuserPageList = 0;
            foreach (ListItem item in chkbxuserPageList.Items)
            {
                if (item.Selected == true)
                {
                    CountuserPageList++;
                }
            }


            if (CountlevelPageList > 0 || CountuserPageList > 0)
            {
                hash = new Hashtable();
                hash = (Hashtable)Session["User"];
                SqlConnection con = new SqlConnection(constr);

                cmd = new SqlCommand("insert_pages", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User", Convert.ToString(hash["Name"].ToString()));
                cmd.Parameters.AddWithValue("@LoginID", 0);
                cmd.Parameters.AddWithValue("@id", ddl_user.SelectedValue);
                cmd.Parameters.AddWithValue("@MenuID", MenuType.All);
                cmd.Parameters.AddWithValue("@PageID", 0);
                cmd.Parameters.AddWithValue("@Type", "DeleteOldRecord");
                con.Open();
                Count = (int)cmd.ExecuteScalar();
                con.Close();

                    foreach (ListItem item in chkbxlevelPageList.Items)
                    {
                        if (item.Selected == true)
                        {
                            cmd = new SqlCommand("insert_pages", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@User", Convert.ToString(hash["Name"].ToString()));
                            cmd.Parameters.AddWithValue("@LoginID", 0);
                            cmd.Parameters.AddWithValue("@id", ddl_user.SelectedValue);
                            cmd.Parameters.AddWithValue("@MenuID", MenuType.level);
                            cmd.Parameters.AddWithValue("@PageID", item.Value);
                            cmd.Parameters.AddWithValue("@Type", "UpdatePages");
                            con.Open();
                            Count = (int)cmd.ExecuteScalar();
                            con.Close();
                        }
                    }

                    foreach (ListItem item in chkbxuserPageList.Items)
                    {
                        if (item.Selected == true)
                        {
                            cmd = new SqlCommand("insert_pages", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@User", Convert.ToString(hash["Name"].ToString()));
                            cmd.Parameters.AddWithValue("@LoginID", 0);
                            cmd.Parameters.AddWithValue("@id", ddl_user.SelectedValue);
                            cmd.Parameters.AddWithValue("@MenuID", MenuType.user);
                            cmd.Parameters.AddWithValue("@PageID", item.Value);
                            cmd.Parameters.AddWithValue("@Type", "UpdatePages");
                            con.Open();
                            Count = (int)cmd.ExecuteScalar();
                            con.Close();
                        }
                }

                if (Count == 0)
                {
                    Clear();
                    userlist();
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('Record Saved Sucessfully.');", true);
                }
              
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('Error in Saving Details.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('Please Select at least one page from the below page list.');", true);
            }
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
          
            chklevelAll.Checked = false;
            chkbxuserPageList.ClearSelection();
            chkuserAll.Checked = false;
            chkbxlevelPageList.ClearSelection();
            ddl_user.ClearSelection();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }
    protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddl_user.SelectedValue) > 0)
            {
                chklevelAll.Checked = false;
                chkbxlevelPageList.ClearSelection();
                chkuserAll.Checked = false;
                chkbxuserPageList.ClearSelection();
                SqlConnection con = new SqlConnection(constr);
                cmd = new SqlCommand("GetLoginDetails", con);
                cmd.Parameters.AddWithValue("@LoginID", ddl_user.SelectedValue);
                cmd.Parameters.AddWithValue("@UserName", null);
                cmd.Parameters.AddWithValue("@Password", null);
                cmd.Parameters.AddWithValue("@MenuID", MenuType.All);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                DataSet ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                   
                    btnSave.Text = "Update User";
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (ListItem item in chkbxlevelPageList.Items)
                    {
                        int i = 0;
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            if (item.Value == ds.Tables[1].Rows[i]["PageID"].ToString())
                            {
                                item.Selected = true;
                            }
                            i++;
                        }
                    }

                    foreach (ListItem item in chkbxuserPageList.Items)
                    {
                        int i = 0;
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            if (item.Value == ds.Tables[1].Rows[i]["PageID"].ToString())
                            {
                                item.Selected = true;
                            }
                            i++;
                        }
                    }


                }
            }
            else
            {
                Clear();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }

    protected void chklevelAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chklevelAll.Checked == true)
            {
                foreach (ListItem item in chkbxlevelPageList.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (ListItem item in chkbxlevelPageList.Items)
                {
                    item.Selected = false;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }

    protected void chkbxlevelPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int Count = 0;
            foreach (ListItem item in chkbxlevelPageList.Items)
            {
                if (item.Selected == false)
                {
                    Count++;
                }
            }
            if (Count > 0)
            {
                chklevelAll.Checked = false;
            }
            else
            {
                chklevelAll.Checked = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }

    protected void chkuserAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkuserAll.Checked == true)
            {
                foreach (ListItem item in chkbxuserPageList.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (ListItem item in chkbxuserPageList.Items)
                {
                    item.Selected = false;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }

    protected void chkbxuserPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int Count = 0;
            foreach (ListItem item in chkbxuserPageList.Items)
            {
                if (item.Selected == false)
                {
                    Count++;
                }
            }
            if (Count > 0)
            {
                chkuserAll.Checked = false;
            }
            else
            {
                chkuserAll.Checked = true;
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
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }

}