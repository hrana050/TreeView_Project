<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Makeuser.aspx.cs" Inherits="Setup_Makeuser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
  <section class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1>Make User</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item"><a href="#">Manage User</a></li>
            <li class="breadcrumb-item active">Make User</li>
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
          <div class="col-md-4">
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
                          <div class="col-md-11">
                  <label for="inputName">Select User</label>
                              <asp:DropDownList ID="ddl_user" runat="server" AutoPostBack="true" class="form-control"  OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged">

                              </asp:DropDownList>
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_user" ErrorMessage="*" InitialValue="0" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                </div>
                </div>
              </div>
              </div>
           
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
          </div>
          </div>
          <div class="col-md-8">
            <div class="card">
              <div class="card-header" style="background-color: #ffffff;">
                <div class="card-body">
              <center>
                    <table style="margin-left: -40px;">
                        <tr>
                            <td>
                                <h4>Pages in Level</h4>
                            </td>
                            <td>
                                <h4>Pages in User</h4>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnllevelChkbx" runat="server" Height="300px" Width="250px" Style="overflow: auto;" CssClass="checkbox">
                                    <asp:CheckBox ID="chklevelAll" Text="Select All" runat="server" AutoPostBack="true" OnCheckedChanged="chklevelAll_CheckedChanged" />
                                    <asp:CheckBoxList ID="chkbxlevelPageList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkbxlevelPageList_SelectedIndexChanged"></asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:Panel ID="pnluserChkbx" runat="server" Height="300px" Width="250px" Style="overflow: auto;">
                                    <asp:CheckBox ID="chkuserAll" Text="Select All" runat="server" AutoPostBack="true" OnCheckedChanged="chkuserAll_CheckedChanged" />
                                    <asp:CheckBoxList ID="chkbxuserPageList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkbxuserPageList_SelectedIndexChanged"></asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <td>
                            <br />
                        </td>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save User" CssClass="btn btn-default" ValidationGroup="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-primary" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </center>
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

