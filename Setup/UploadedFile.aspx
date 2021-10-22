<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="UploadedFile.aspx.cs" Inherits="Setup_UploadedFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function somefun(value) {
            $.ajax({
                type: "POST",
                url: "UploadedFile.aspx/GetCurrentTime",
                data: '{name: "' + value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var xmlDoc = $.parseXML(response.d);
                    var xml = $(xmlDoc);
                    var customers = xml.find("Table");
                    var rows = '';
                    $.each(customers, function () {
                        $('#divopen').show();
                        var levlename = $(this).find("LevelName").text();
                        var filename = $(this).find("file_names").text();
                        var filepath = $(this).find("filepath").text();
                        rows += "<tr><td>" + levlename + "</td><td>" + filename + ' ' + "</td><td>  <a href='/upload/"+filepath+"' target='_blank'>View</a></td></tr>";
                    });
                    $('#tblCustomers tbody').append(rows);
                },
                error: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                }
            });
            // $('#myModal').modal('show');
        }

  
</script>
      <section class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1>File Tree View</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><a href="#">Master</a></li>
            <li class="breadcrumb-item"><a href="#">Manage File View</a></li>
            <li class="breadcrumb-item active">File View</li>
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
            
              <div class="card-header">
                <h3 class="card-title">File Tree View</h3>
                <div class="card-tools">
                
                </div>
              </div>
                 
              <div class="update">
                
              <div class="card-body">
                <div class="form-group">
                          <div class="row">
                     <div class="col-md-12">
                           <asp:TreeView ID="TreeViewProducts" runat="server" OnSelectedNodeChanged="TreeViewProducts_SelectedNodeChanged">
           <HoverNodeStyle BackColor="#ffff" ForeColor="#0000" />
           <NodeStyle Font-Names="Tahoma" Font-Size="10.5pt" ForeColor="Black" HorizontalPadding="2px"
        NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
    <SelectedNodeStyle BackColor="#ffc107" Font-Underline="False" HorizontalPadding="0px"
        VerticalPadding="0px" />
        </asp:TreeView>
        <br />
        <asp:Label ID="labelCurrentPath" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                        </div>
                          
                  </div>
                  
                </div>
              
               
              </div>
              </div>
           
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
          </div>
          
          <div class="col-md-7" id="divopen" style="display:none">
            <div class="card">
              <div class="card-header" style="background-color: #ffffff;">
                <div class="card-body">
              
               
                      <table class="table table-bordered table-striped" id="tblCustomers" border="1">
        <thead>
            <tr>
                <th>Level Name</th>
                <th>File Name</th>
                <th>Uploaded File</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    <!-- Modal -->

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

