﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Usermap.aspx.cs" Inherits="Setup_Usermap" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
          <!-- Content Header (Page header) -->
  <section class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1>File Link </h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item"><a href="#">Manage User</a></li>
            <li class="breadcrumb-item active">Add File Link</li>
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
                <h3 class="card-title">File Link</h3>
                <div class="card-tools">
                
                </div>
              </div>
                 
              <div class="update" id="validateform">
                
              <div class="card-body">
                <div class="form-group">
                    <div class="row">
                          <div class="col-md-5">
                  <label for="inputName">Select User</label>
                              <asp:DropDownList ID="ddl_user" runat="server" class="form-control">

                              </asp:DropDownList>
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_user" ErrorMessage="*" InitialValue="0" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                </div>
                    <div class="col-md-5">
                  <label for="inputName">Select File</label>
                  <asp:DropDownList ID="ddl_file" runat="server" class="form-control">

                  </asp:DropDownList>
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddl_file" ErrorMessage="*" InitialValue="0" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                        </div><br />
                          <div class="row">
                     <div class="col-md-5">
                  <label for="inputName">Assign</label>
                           <asp:DropDownList ID="ddl_assign" runat="server" class="form-control">
                               <asp:ListItem Value="0" Text="Select Value">Select Value</asp:ListItem>
                               <asp:ListItem Value="1" Text="Yes">Yes</asp:ListItem>
                               <asp:ListItem Value="2" Text="No">No</asp:ListItem>
                  </asp:DropDownList>
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_assign" ErrorMessage="*" InitialValue="0" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>       
                        <div class="col-md-6" id="Divhide" style="margin-top: 30px;">
                          <asp:Button ID="btnCancel" Text="Cancel" runat="server" class="btn btn-secondary float-right" OnClick="btnCancel_Click" />

                        <asp:Button ID="btn_save" runat="server" Text="Save" ValidationGroup="Save" class="btn btn-success float-right" style="margin-right: 15px;" OnClick="btn_Save_Click"/>
                    </div>  
                  </div>
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
                      <asp:GridView ID="grdrecord" runat="server" AutoGenerateColumns="false" OnRowCommand="grdrecord_RowCommand" Width="100%" class="table table-bordered table-striped">
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#ffffff" Font-Bold="True" ForeColor="Black" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="#FFFFFF"/>
                            <RowStyle HorizontalAlign="Center" BackColor="#f2f2f2"/>
                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:BoundField HeaderText="Name" DataField="UserName" />
                                  <asp:BoundField HeaderText="Level Name" DataField="LevelName" />
                                <asp:BoundField HeaderText="File Name" DataField="file_names" />
                                  <asp:TemplateField HeaderText="Uploaded File">
                                    <ItemTemplate>
                                        <a href="/upload/<%# Eval("filepath") %>" target="_blank">View</a> 
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Assign" DataField="visiblevalue" />
                                <%--<asp:BoundField HeaderText="Created On" DataField="CreatedOn" />--%>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="lnkEdit" CommandArgument='<%#Eval("linkid") %>' />
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

