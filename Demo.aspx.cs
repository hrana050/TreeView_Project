﻿using System;
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

public partial class Demo : System.Web.UI.Page
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
           // TreeViewProducts.Attributes.Add("onclick", "alertVal()");

            gettreeviewdata();
        }
    }

    private void gettreeviewdata()
    {
        con = new SqlConnection(constr);
        da = new SqlDataAdapter("getmasterlevel", con);
        ds = new DataSet();
        da.Fill(ds);
        ds.Relations.Add("ChildRows", ds.Tables[0].Columns["LevelID"], ds.Tables[0].Columns["Levelvalue"],false);
        foreach(DataRow leveldata in ds.Tables[0].Rows)
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
    private void GetChildRows(DataRow dr,TreeNode tn)
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
         if(childRow.GetChildRows("ChildRows").Length>0)
         {
             if(childRow["fileupload"].ToString() !="" && childRow["fileupload"].ToString() !=null)
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
        if (TreeViewProducts.SelectedNode.Parent == null)
        {

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
    public static string GetCurrentTime(string name)
    {
        return "Hello " + name + Environment.NewLine + "The Current Time is: "
            + DateTime.Now.ToString();
    }
}