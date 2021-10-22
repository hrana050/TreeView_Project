<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Demo.aspx.cs" Inherits="Demo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
   
     function somefun(value)
     {
         $.ajax({
             type: "POST",
             url: "Demo.aspx/GetCurrentTime",
             data: '{name: "' + value + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
           alert(response.d);
        }
          });
        // $('#myModal').modal('show');
     }
     function OnSuccess(response) {
         $('#divopen').show();
         
         alert(response.d);
     }
</script>
  
    <br />
    <div style="width: 50%; text-align: left;">
  
       <asp:TreeView ID="TreeViewProducts" runat="server" OnSelectedNodeChanged="TreeViewProducts_SelectedNodeChanged">
           <HoverNodeStyle BackColor="#0000" />
           <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
        NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
    <ParentNodeStyle Font-Bold="False" />
    <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
        VerticalPadding="0px" />
        </asp:TreeView>
        <br />
        <asp:Label ID="labelCurrentPath" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
    </div>
    <div class="container" id="divopen" style="display:none">
    <!-- Modal -->
   <span>Hemant Singh Rana</span>
</div>
</asp:Content>

