<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
          <!-- Content Wrapper. Contains page content -->

    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0">Dashboard</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">Home</a></li>
              <li class="breadcrumb-item active">Dashboard</li>
            </ol>
          </div><!-- /.col -->
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->

    <!-- Main content -->
    <section class="content">
      <div class="container-fluid">
        <!-- Small boxes (Stat box) -->
        <div class="row">
          <div class="col-lg-3 col-6">
            <!-- small box -->
            <div class="small-box bg-info">
              <div class="inner">
                <h3>
              <asp:Label ID="lbl_staff" runat="server"></asp:Label>

                </h3>

                <p>Total Staff</p>
              </div>
              <div class="icon">
                 <i class="ion ion-person-add"></i>
              </div>
              <a href="#" class="small-box-footer">  <span>Active -   <asp:Label ID="lblactivestaff" runat="server"></asp:Label></span> <span>Deactive - <asp:Label ID="lbldeactivestaff" runat="server"></asp:Label></span></a>
            </div>
          </div>
          <!-- ./col -->
          <div class="col-lg-3 col-6">
            <!-- small box -->
            <div class="small-box bg-success">
              <div class="inner">
                <h3>
              <asp:Label ID="lbllevel" runat="server"></asp:Label>
                </h3>
                <p>Total Level</p>
              </div>
              <div class="icon">
                <i class="ion ion-stats-bars"></i>
              </div>
              <a href="#" class="small-box-footer">  <span>Active -   <asp:Label ID="lblactivelevel" runat="server"></asp:Label></span> <span>Deactive - <asp:Label ID="lbldeactivelevel" runat="server"></asp:Label></span></a>
            </div>
          </div>
          <!-- ./col -->
          <div class="col-lg-3 col-6">
            <!-- small box -->
            <div class="small-box bg-danger">
              <div class="inner">
                <h3>
                      <asp:Label ID="lblfile" runat="server"></asp:Label>
                </h3>
                <p>Total File</p>
              </div>
              <div class="icon">
                <i class="ion ion-filing"></i>
              </div>
              <a href="#" class="small-box-footer">  <span>Active -   <asp:Label ID="lblactivefile" runat="server"></asp:Label></span> <span>Deactive - <asp:Label ID="lbldeactivefile" runat="server"></asp:Label></span></a>
            </div>
          </div>
          <!-- ./col -->
          <div class="col-lg-3 col-6">
            <!-- small box -->
            <div class="small-box badge-primary">
              <div class="inner">
                <h3>65</h3>

                <p>Unique Visitors</p>
              </div>
              <div class="icon">
                <i class="ion ion-pie-graph"></i>
              </div>
              <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
            </div>
          </div>
          <!-- ./col -->
        </div>
        <!-- /.row -->

           <!-- Main row -->
        <div class="row" runat="server" id="admindashboard">
          <!-- Left col -->
          <div class="col-md-6">
            <!-- TABLE: LATEST ORDERS -->
            <div class="card">
              <div class="card-header border-transparent">
                <h3 class="card-title">Last 10 Users</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-card-widget="collapse">
                    <i class="fas fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-card-widget="remove">
                    <i class="fas fa-times"></i>
                  </button>
                </div>
              </div>
              <!-- /.card-header -->
              <div class="card-body p-0">
                <div class="table-responsive">
                     <asp:GridView ID="grdrecord" runat="server" AutoGenerateColumns="false" Width="100%" class="table table-bordered table-striped">
                          
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
                                <asp:BoundField HeaderText="User Name" DataField="username" />
                                <asp:BoundField HeaderText="Contact No" DataField="contactno" />
                                <asp:BoundField HeaderText="Email Id" DataField="emailid" />
                               <asp:BoundField HeaderText="Status" DataField="userStatus" />
                                
                            </Columns>
                            <EmptyDataTemplate>
                                No Record Found
                            </EmptyDataTemplate>
                        </asp:GridView>
                </div>
                <!-- /.table-responsive -->
              </div>
              <!-- /.card-body -->
              <div class="card-footer clearfix">
                <a href="../Setup/Createuser.aspx" class="btn btn-sm btn-secondary float-right">View All User</a>
              </div>
              <!-- /.card-footer -->
            </div>
            <!-- /.card -->
          </div>
          <!-- /.col -->

          <div class="col-md-6">
            <!-- TABLE: LATEST ORDERS -->
            <div class="card">
              <div class="card-header border-transparent">
                <h3 class="card-title">Last 10 Uploaded File</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-card-widget="collapse">
                    <i class="fas fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-card-widget="remove">
                    <i class="fas fa-times"></i>
                  </button>
                </div>
              </div>
              <!-- /.card-header -->
              <div class="card-body p-0">
                <div class="table-responsive">
                     <asp:GridView ID="grdfile" runat="server" AutoGenerateColumns="false" Width="100%" class="table table-bordered table-striped">
                          
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
                                <asp:BoundField HeaderText="Level" DataField="levelname" />
                                <asp:BoundField HeaderText="File Name" DataField="filename" />
                             <asp:TemplateField HeaderText="Uploaded File">
                                    <ItemTemplate>
                                        <a href="/upload/<%# Eval("filepath") %>" target="_blank">View</a> 
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:BoundField HeaderText="Status" DataField="dashfilestatus" />
                                
                            </Columns>
                            <EmptyDataTemplate>
                                No Record Found
                            </EmptyDataTemplate>
                        </asp:GridView>
                </div>
                <!-- /.table-responsive -->
              </div>
              <!-- /.card-body -->
              <div class="card-footer clearfix">
                <a href="../Setup/levelmap.aspx" class="btn btn-sm btn-secondary float-right">View All Files</a>
              </div>
              <!-- /.card-footer -->
            </div>
            <!-- /.card -->
          </div>
          <!-- /.col -->
        </div>
        <!-- /.row -->
      </div><!-- /.container-fluid -->
    </section>
    <!-- /.content -->

  <!-- /.content-wrapper -->
</asp:Content>

