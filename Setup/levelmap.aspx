<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="levelmap.aspx.cs" Inherits="Setup_levelmap" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function Filevalidation() {
            var lblFile = document.getElementById("<%=lblSelected.ClientID %>");
        lblFile.innerHTML = "";
        var lblError = document.getElementById("<%=lblError.ClientID %>");
        lblError.innerHTML = "";
        var fileUpload = document.getElementById("<%=FileUpload1.ClientID %>");
        var allowedFiles = [".doc", ".docx", ".pdf",".xlsx",".xls"];
        var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
        if (!regex.test(fileUpload.value.toLowerCase())) {
            lblError.innerHTML = "Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.";
            document.getElementById('<%=btn_save.ClientID %>').disabled = true;
            return false;
        } else {
            if (fileUpload.files.length > 0) {
                for (var i = 0; i <= fileUpload.files.length - 1; i++) {
                    var fsize = fileUpload.files.item(i).size;
                    var file = Math.round((fsize / 1024));
                    // The size of the file.
                    if (file >= 10000) {
                      //  alert("File too Big, please select a file less than 11mb");
                        lblFile.innerHTML = "File too Big, please select a file less than 11mb";
                        document.getElementById('<%=btn_save.ClientID %>').disabled = true;
                    }
                    else {
                        document.getElementById('<%=btn_save.ClientID %>').disabled = false;

                    }
                }
            }
           
        }
    }
</script>
      <section class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1>Level Map</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item"><a href="#">Manage Level</a></li>
            <li class="breadcrumb-item active">Add Level Map</li>
          </ol>
        </div>
      </div>
    </div>
  </section>
    <%-- <asp:UpdateProgress ID="MyProcess" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    </asp:UpdateProgress>--%>
<%--     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
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
                  <label for="inputName">Level</label>
                           <asp:DropDownList ID="ddl_levelname" runat="server" class="form-control">
                              </asp:DropDownList>
              
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddl_levelname" ErrorMessage="*" InitialValue="0" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                       <div class="col-md-5">
                  <label for="inputName">File Alias Name</label>
                   <asp:TextBox ID="txt_filename" runat="server" class="form-control" AutoComplete="off"></asp:TextBox>
                        </div>
                         <div class="col-md-1" style="margin-top: 30px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_filename" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                        
                  </div><br />
                     <div class="row">
                     <div class="col-md-11">
                  <label for="inputName">File Upload</label>
                         <asp:FileUpload ID="FileUpload1" runat="server"  class="form-control" accept=".pdf,.docx,.xlsx,xls" onchange="Filevalidation()"/>
                          <asp:Label ID="lblError" runat="server" ForeColor="Red" />
                          <hr />
                           <asp:Label ID="lblSelected" runat="server" ForeColor="red" />
                        </div>
                       <%-- <div class="col-md-1" style="margin-top: 30px;">
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUpload1" ErrorMessage="*" ValidationGroup="Save" ForeColor="Red">
        </asp:RequiredFieldValidator>    
                        </div>--%>
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
                                  <asp:BoundField HeaderText="Level Title" DataField="Leveltitle" />
                                <asp:BoundField HeaderText="File Name" DataField="file_names" />
                                  <asp:TemplateField HeaderText="Uploaded File">
                                    <ItemTemplate>
                                        <a href="/upload/<%# Eval("filepath") %>" target="_blank">View</a> 
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Created By" DataField="CreatedBy" />
                                <%--<asp:BoundField HeaderText="Created On" DataField="CreatedOn" />--%>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="lnkEdit" CommandArgument='<%#Eval("fileid") %>' />
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

<%--  </ContentTemplate>
                      </asp:UpdatePanel>--%>
</asp:Content>

