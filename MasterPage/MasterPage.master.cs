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
}
