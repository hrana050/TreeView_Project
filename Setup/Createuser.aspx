<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Createuser.aspx.cs" Inherits="Setup_Createuser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <script src="http://code.jquery.com/jquery-1.11.3.js" type="text/javascript"></script>
    <script type="text/javascript">

        function CheckuserAvailability() {
            //This function call on text change.             
            $.ajax({
                type: "POST",
                url: 'Createuser.aspx/CheckuserAvailability', // this for calling the web method function in cs code.  
                data: '{usercontact: "' + $("#<%=txt_contact.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response);
                }
            });
        }
        function OnSuccess(response) {
            var txtvalue = $("#<%=txt_name.ClientID%>")[0].value;
            var msg = $("#spnMsg")[0];
            switch (response.d) {
                case "true":
                    msg.style.display = "block";
                    msg.style.color = "red";
                    msg.innerHTML = "(" + txtvalue + ")" + " username already taken";
                    document.getElementById('<%=btn_save.ClientID %>').disabled = true;
                     break;
                 case "false":
                     msg.style.display = "block";
                     msg.style.color = "green";
                     msg.innerHTML = "(" + txtvalue + ")" + " username Available";
                     document.getElementById('<%=btn_save.ClientID %>').disabled = false;
                     break;
             }
         }


    </script>
  <section class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1>User Add</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item"><a href="#">Manage User</a></li>
            <li class="breadcrumb-item active">Add User</li>
          </ol>
        </div>
      </div>
    </div>
  </section>
     <asp:UpdateProgress ID="MyProcess" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="5">
        <ProgressTemplate>
            <div style="left: 0; position: fixed; width: 100%; height: 100%; z-index: 9999999; top: 0; background: rgba(0,0,0,0.5);">
                <div style="text-align: center; z-index: 10; margin: 300px auto;">
                    <img alt="img" src="../Img/loding.gif" style="height: 100px; width: 100px;" /><br />
                    <br />
                    <span>
                        <h4>
                            <asp:Label runat="server" Text="Please Wait..." ID="lblPleaseWait"></asp:Label>
                    </span>
                    </h4>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <!-- Main content -->
    <section class="content" style="padding-left: 15px;padding-right: 15px;">
        <div class="row">
          <div class="col-md-5">
            <div class="card card-primary">
            
              <div class="alert alert-success alert-dismissible fade show" role="alert" runat="server" id="successdiv" visible="false">
                <strong>Success!</strong> Record Saved Sucessfully.
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="alert alert-danger alert-dismissible fade show" role="alert" runat="server" id="errordiv" visible="false">
                <strong>
                    <asp:Label ID="lblerrormsg" runat="server"></asp:Label>!</strong> Already Exist.
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>

              <div class="card-header">
                <h3 class="card-title">User</h3>
                <div class="card-tools">
                
                </div>
              </div>
                 
              <div class="update" id="validateform">
                
              <div class="card-body">
                <div class="form-group">
                    <div class="row">
                          <div class="col-md-5">
                             <label for="inputName">User Name</label>
                             <asp:TextBox ID="txt_name" runat="server" class="form-control" AutoComplete="off"></asp:TextBox>
                              <span id="spnMsg"></span>
                        </div>
                 <div class="col-md-1" style="margin-top: 30px;">
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_name" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                    <div class="col-md-5">
                  <label for="inputName">Password</label>
                   <asp:TextBox ID="txt_pwd" runat="server" class="form-control" AutoComplete="off"></asp:TextBox>
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_pwd" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                        </div><br />
                          <div class="row">
                          <div class="col-md-5">
                             <label for="inputName">Email Id</label>
                             <asp:TextBox ID="txt_emaild" runat="server" class="form-control" AutoComplete="off"></asp:TextBox>
                        </div>
                 <div class="col-md-1" style="margin-top: 30px;">
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_emaild" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                    <div class="col-md-5">
                  <label for="inputName">Contact No</label>
                   <asp:TextBox ID="txt_contact" runat="server" class="form-control" AutoComplete="off" onchange="CheckuserAvailability()"></asp:TextBox>
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_contact" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                        </div><br />
                          <div class="row">
                               <div class="col-md-5">
                  <label for="inputName">Select Type</label>
                         <asp:DropDownList ID="ddl_usertype" runat="server"  class="form-control">
                             <asp:ListItem Value="0" Text="Select Type">Select Type</asp:ListItem>
                             <asp:ListItem Value="Admin" Text="Admin">Admin</asp:ListItem>
                             <asp:ListItem Value="Staff" Text="Staff">Staff</asp:ListItem>
                         </asp:DropDownList>
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddl_usertype" ErrorMessage="*" InitialValue="0" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>       
                     <div class="col-md-5">
                  <label for="inputName">Select Status</label>
                         <asp:DropDownList ID="ddl_status" runat="server"  class="form-control">
                             <asp:ListItem Value="3" Text="Select Status">Select Status</asp:ListItem>
                             <asp:ListItem Value="1" Text="Active">Active</asp:ListItem>
                             <asp:ListItem Value="0" Text="Inactive">Inactive</asp:ListItem>
                         </asp:DropDownList>
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_status" ErrorMessage="*" InitialValue="3" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>       
                         
                  </div>
                  
                </div>
              
                <div class="row">
                     <div class="col-md-11" id="Divhide">
                          <asp:Button ID="btnCancel" Text="Cancel" runat="server" class="btn btn-secondary float-right" OnClick="btnCancel_Click" />

                        <asp:Button ID="btn_save" runat="server" Text="Save" ValidationGroup="Save" class="btn btn-success float-right" style="margin-right: 15px;" OnClick="btn_Save_Click"/>
                    </div>
                    <div class="col-md-1"></div>
                  </div>
              </div>
              </div>
           
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
          </div>
          
          <div class="col-md-7">
            <div class="card">
              <div class="card-header" style="background-color: #ffffff;">
                <div class="card-body">
         <asp:GridView ID="grdrecord" runat="server" AutoGenerateColumns="false" OnRowCommand="grdrecord_RowCommand" AllowPaging="true"
                                     OnPageIndexChanging="OnPageIndexChanging" PageSize="10" Width="100%" class="table table-bordered table-striped">
                          
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#ffffff" Font-Bold="True" ForeColor="Black" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="#FFFFFF"/>
                            <RowStyle HorizontalAlign="Center" BackColor="#f2f2f2"/>
                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                            <Columns>
                                <%-- <asp:BoundField HeaderText="LevelID" DataField="LevelID" />--%>
                                  <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="User name" DataField="username" />
                          <%--      <asp:BoundField HeaderText="Email Id" DataField="emailid" />--%>
                                <asp:BoundField HeaderText="Contact No" DataField="contactno" />
                                <asp:BoundField HeaderText="Password" DataField="Password" />
                                 <asp:BoundField HeaderText="Type" DataField="u_role" />
                               <asp:BoundField HeaderText="Status" DataField="isactive" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="lnkEdit" CommandArgument='<%#Eval("loginid") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Record Found
                            </EmptyDataTemplate>
                        </asp:GridView>
                  </div>
              </div>
              
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
          </div>
        </div>
    
      </section>

  </ContentTemplate>
                      </asp:UpdatePanel>
</asp:Content>

