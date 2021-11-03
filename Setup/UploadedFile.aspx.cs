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
using System.Linq;

public partial class Setup_UploadedFile : System.Web.UI.Page
{
    string constr = "";
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
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
        if (!Page.IsPostBack)
        {
            if (Session["User"] != null)
            {
                bindyear();
                string yearid;
                if (Session["yearid"].ToString().Length > 0)
                {
                    yearid = Session["yearid"].ToString();
                    gettreeviewdata(yearid);
                }
                else
                {
                    yearid = "0";
                }
            
            }
            else
            {
                Response.Redirect("../Login.aspx", false);
            }
        }
    }
    private void gettreeviewdata(string yearid)
    {
        
        con = new SqlConnection(constr);
        //cmd = new SqlCommand("getmasterlevel", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@yearid", yearid);
        //con.Open();
       // DataTable dt = new DataTable();
        //da = new SqlDataAdapter("getmasterlevel", con);
        cmd = new SqlCommand("getmasterlevel", con);
        cmd.Parameters.Add("@yearid", yearid);
        cmd.CommandType = CommandType.StoredProcedure;
        da.SelectCommand = cmd;
        ds = new DataSet();
        da.Fill(ds);
        ds.Relations.Add("ChildRows", ds.Tables[0].Columns["LevelID"], ds.Tables[0].Columns["Levelvalue"], false);
        TreeViewProducts.Nodes.Clear();
        foreach (DataRow leveldata in ds.Tables[0].Rows)
        {
            if (leveldata["Levelvalue"].ToString() == "0")
            {
                TreeNode parentTreeNodeobj = new TreeNode();
                parentTreeNodeobj.Text = leveldata["Leveltitle"].ToString();
                parentTreeNodeobj.Value = leveldata["LevelID"].ToString();
                //   parentTreeNodeobj.NavigateUrl = "JavaScript:Void(0);";
                //   parentTreeNodeobj.NavigateUrl = leveldata["levelid"].ToString();
                //DataRow[] childrow = leveldata.GetChildRows("ChildRows");
                //foreach(DataRow leveldata_1 in childrow)
                //{
                //       TreeNode childTreeNodeobj = new TreeNode();
                //       childTreeNodeobj.Text = leveldata_1["ProductName"].ToString();
                //       parentTreeNodeobj.ChildNodes.Add(childTreeNodeobj);

                //}
                GetChildRows(leveldata, parentTreeNodeobj);
                TreeViewProducts.Nodes.Add(parentTreeNodeobj);
                TreeViewProducts.CollapseAll();
            }
        }
    }
    public void bindyear()
    {
        try
        {
            SqlConnection con = new SqlConnection(constr);
            cmd = new SqlCommand("ManageYears", con);
            cmd.Parameters.AddWithValue("@User", null);
            cmd.Parameters.AddWithValue("@Type", "GetRecords_file");
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            ddl_year.DataSource = dt;
            ddl_year.DataTextField = "Year";
            ddl_year.DataValueField = "Yearid";
            ddl_year.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
        }
    }
    private void GetChildRows(DataRow dr, TreeNode tn)
    {

        DataRow[] childRows = dr.GetChildRows("ChildRows");
        foreach (DataRow childRow in childRows)
        {
            TreeNode childTreeNode = new TreeNode();
            childTreeNode.Text = childRow["Leveltitle"].ToString();
            childTreeNode.Value = childRow["LevelID"].ToString();
            // childTreeNode.NavigateUrl = "JavaScript:Void(0);";
            // childTreeNode.NavigateUrl = childRow["levelid"].ToString();
            tn.ChildNodes.Add(childTreeNode);
            if (childRow.GetChildRows("ChildRows").Length > 0)
            {
                if (childRow["fileupload"].ToString() != "" && childRow["fileupload"].ToString() != null)
                {
                    // childTreeNode.Text = childRow["fileupload"].ToString();
                }

                GetChildRows(childRow, childTreeNode);
            }
        }
    }

    protected void TreeViewProducts_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeViewProducts.SelectedNode.Collapse();
       // this.labelCurrentPath.Text = ">" + TreeViewProducts.SelectedNode.ValuePath.ToString();
        if (TreeViewProducts.SelectedNode.Parent == null)
        {
            int id = int.Parse(TreeViewProducts.SelectedNode.Value.ToString());
            //Session["treesno"] = id;
            ScriptManager.RegisterStartupScript(this, typeof(string), "Passing", String.Format("somefun('{0}');", id), true);
        }
        else
        {
            int id = int.Parse(TreeViewProducts.SelectedNode.Value.ToString());
            // ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "validate", "javascript: alert('" + ex.Message + "');", true);
            ScriptManager.RegisterStartupScript(this, typeof(string), "Passing", String.Format("somefun('{0}');", id), true);

            string name = TreeViewProducts.SelectedNode.Text.ToString();
            // obj.filename = name;
            //SqlDataReader dr = obj.RetriveImagePath(obj.filename, obj.folderid);
            //while (dr.Read())
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "myFunction('" + dr["FilePath"] + "')", true);
            //}

        }
    }
    [System.Web.Services.WebMethod]
    public static string Getdata(string name,string id)
    {
       
        string constr = ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("getmapingfile", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@levelid", name);
              //  cmd.Parameters.AddWithValue("@userud", id);
                cmd.Connection = con;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds.GetXml();
                }
            }
        }
       
    }

    [System.Web.Services.WebMethod]
    public static string updatedata(string id,string status)
    {

        string constr = ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("updatedata", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fileid", id);
                cmd.Parameters.AddWithValue("@filestatus", status);
                cmd.Connection = con;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds.GetXml();
                }
            }
        }

    }
    protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
    {
        string yearid = ddl_year.SelectedValue;
        gettreeviewdata(yearid);
    }
}