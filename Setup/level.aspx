<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="level.aspx.cs" Inherits="Setup_level" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <style>
        .success {
            background-color: #5cb85c;
            font-size: 12px;
            color: #ffffff;
            padding: 3px 6px 3px 6px;
        }

        .failure {
            background-color: #ed4e2a;
            font-size: 12px;
            color: #ffffff;
            padding: 3px 6px 3px 6px;
        }
    </style>
     <script src="http://code.jquery.com/jquery-1.11.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        function handleSpace(event) {
            //handling ie and other browser keycode 
            var keyPressed = event.which || event.keyCode;

            //Handling whitespace
            //keycode of space is 32
            if (keyPressed == 32) {
                event.preventDefault();
                event.stopPropagation();
            }
        }
        function ChecklevelAvailability() {
         
            $.ajax({
                type: "POST",
                url: 'level.aspx/ChecklevelAvailability', // this for calling the web method function in cs code.  
                data: '{levelname: "' + $("#<%=txt_Lname.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response);
                }
            });
        }
        function OnSuccess(response) {
            var txtvalue = $("#<%=txt_Lname.ClientID%>")[0].value;
            var msg = $("#spnMsg")[0];
             switch (response.d) {
                 case "true":
                     msg.style.display = "block";
                     msg.style.color = "red";
                     msg.innerHTML = "(" + txtvalue + ")" + " Level already taken";
                     document.getElementById('<%=btn_save.ClientID %>').disabled = true;
                     break;
                 case "false":
                     msg.style.display = "block";
                     msg.style.color = "green";
                     msg.innerHTML = "(" + txtvalue + ")" + " Level Available";
                     document.getElementById('<%=btn_save.ClientID %>').disabled = false;
                     break;
             }
         }

    
    </script>

      <!-- Content Header (Page header) -->
  <section class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1>Level Add</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item"><a href="#">Manage Level</a></li>
            <li class="breadcrumb-item active">Add Level</li>
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
                <h3 class="card-title">Level</h3>
                <div class="card-tools">
                
                </div>
              </div>
                 
              <div class="update" id="validateform">
                
              <div class="card-body">
                <div class="form-group">
                    <div class="row">
                          <div class="col-md-5">
                  <label for="inputName">Level Value</label>
                              <asp:DropDownList ID="ddl_levelvalue" runat="server" class="form-control">

                              </asp:DropDownList>
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                </div>
                    <div class="col-md-5">
                  <label for="inputName">Level Name</label>
                   <asp:TextBox ID="txt_Lname" runat="server" class="form-control" AutoComplete="off" onchange="ChecklevelAvailability()" onkeypress="handleSpace(event)"></asp:TextBox>
                        <span id="spnMsg"></span>
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_Lname" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                        </div><br />
                          <div class="row">
                     <div class="col-md-11">
                  <label for="inputName">Level Title</label>
                            <asp:TextBox ID="txt_title" runat="server" class="form-control" AutoComplete="off"></asp:TextBox>
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_title" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
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
                                  <asp:BoundField HeaderText="Level Title" DataField="LevelTitle" />
                              
                                  <asp:BoundField HeaderText="Level Value" DataField="Levelvalue" />
                                <asp:BoundField HeaderText="Level" DataField="Levelname" />
                              
                              <%--  <asp:BoundField HeaderText="Created By" DataField="CreatedBy" />--%>
                                <asp:BoundField HeaderText="Created On" DataField="CreatedOn" />
                              <%--  <asp:BoundField HeaderText="Updated By" DataField="UpdatedBy" />
                                <asp:BoundField HeaderText="Updated On" DataField="UpdatedOn" />--%>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="lnkEdit" CommandArgument='<%#Eval("LevelID") %>' />
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

