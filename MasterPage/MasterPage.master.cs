using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;

public partial class MasterPage_MasterPage : System.Web.UI.MasterPage
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
            if (Session["User"] != null || Session["LastLogin"] != null || Session["SchoolName"] != null)
            {
                if (!IsPostBack)
                {

                    hash = (Hashtable)Session["User"];
                    lblUser.Text = "Welcome - " + Convert.ToString(hash["Name"].ToString());
                    if (Session["LastLogin"].ToString().Length > 0)
                    {
                        lblLastLogin.Text = "Your Last Login - " + Convert.ToDateTime(Session["LastLogin"].ToString()).ToString("dd-MMM-yyyy HH:mm:ss tt");
                    }
                    else
                    {
                        lblLastLogin.Text = string.Empty;
                    }

                    ToggleDisplayPages();
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
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
    protected void lnklogout_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Abandon();
            Response.Redirect("../Login.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }
    public void ToggleDisplayPages()
    {
        try
        {
            int adduser = 0;
            int usermapwithfile = 0;
            int makeuser = 0;
            int addlevel = 0;
            int levelmapwithfile = 0;
            int fileview = 0;

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
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    if (row["PageName"].ToString() == "Createuser.aspx")
                    {
                        adduser = adduser + 1;
                    }
                    else if (row["PageName"].ToString() == "Usermap.aspx")
                    {
                        usermapwithfile = usermapwithfile + 1;
                    }
                    else if (row["PageName"].ToString() == "Makeuser.aspx")
                    {
                        makeuser = makeuser + 1;
                    }
                    else if (row["PageName"].ToString() == "level.aspx")
                    {
                        addlevel = addlevel + 1;
                    }
                    else if (row["PageName"].ToString() == "levelmap.aspx")
                    {
                        levelmapwithfile = levelmapwithfile + 1;
                    }
                    else if (row["PageName"].ToString() == "UploadedFile.aspx")
                    {
                        fileview = fileview + 1;
                    }
                   
                }

                if (adduser > 0)
                {
                    adduserli.Visible = true;
                    addyear.Visible = true;
                }
                else
                {
                    adduserli.Visible = false;
                    addyear.Visible = false;
                }
                if (usermapwithfile > 0)
                {
                    addusermapli.Visible = true;
                }
                else
                {
                    addusermapli.Visible = false;
                }
                if (makeuser > 0)
                {
                    addmakeuserli.Visible = true;
                }
                else
                {
                    addmakeuserli.Visible = false;
                }
                if (addlevel > 0)
                {
                    addlevelli.Visible = true;
                }
                else
                {
                    addlevelli.Visible = false;
                }
                if (levelmapwithfile > 0)
                {
                    addlevelmapli.Visible = true;
                }
                else
                {
                    addlevelmapli.Visible = false;
                }
                if (fileview > 0)
                {
                    addlevelfile.Visible = true;
                }
                else
                {
                    addlevelfile.Visible = false;
                }

                if (adduser > 0 || usermapwithfile > 0 || makeuser > 0 )
                {
                    userli.Visible = true;
                }
                else
                {
                    userli.Visible = false;
                }

                if (addlevel > 0 || levelmapwithfile > 0 || fileview > 0 )
                {
                    levelli.Visible = true;
                }
                else
                {
                    levelli.Visible = false;
                }
            }
            else
            {
               // userli.Visible = false;
                //levelli.Visible = false;
               // addyear.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }
}
