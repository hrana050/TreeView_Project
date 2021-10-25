<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>KRM | Admin Login Panel</title>

  <!-- Google Font: Source Sans Pro -->
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
  <!-- Font Awesome -->
  <link rel="stylesheet" type="text/css" href="../plugins/fontawesome-free/css/all.min.css" />
  <!-- icheck bootstrap -->
  <link rel="stylesheet" type="text/css" href="../plugins/icheck-bootstrap/icheck-bootstrap.min.css" />
  <!-- Theme style -->
    <link href="css/style.css" rel="stylesheet" />
</head>
<body class="login-page">
    <form id="form1" runat="server" defaultbutton="btnLogin">
         <asp:ScriptManager ID="sp1" runat="server"></asp:ScriptManager>
    <div class="login-box">
  <div class="login-logo">
    <a href="#"><b>KRM</b> Admin Panel</a>
  </div>
  <!-- /.login-logo -->
  <div class="card">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <div class="card-body login-card-body">
        <center>
        <asp:Label ID="lblloginstatus" runat="server" Visible="false" style="color:red; font-size:18px;"></asp:Label></center>
      <p class="login-box-msg">Sign in to start your session</p>
                            <p class='login-box-msg'>
								
								<span style="color:red"></span>
							
								</p>
        <div class="input-group mb-3">
            <asp:TextBox ID="txt_loginName" runat="server"  class="form-control" placeholder="User Name" AutoComplete="off"></asp:TextBox>
          <div class="input-group-append">
            <div class="input-group-text">
              <span class="fas fa-envelope"></span>
            </div>
          </div>
        </div>
        <div class="input-group mb-3">
          <asp:TextBox ID="txt_loginpwd" runat="server"  class="form-control" placeholder="Password" AutoComplete="off" TextMode="Password"></asp:TextBox>
          <div class="input-group-append">
            <div class="input-group-text">
              <span class="fas fa-lock"></span>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-8">
            <div class="icheck-primary">
              <input type="checkbox" id="remember">
              <label for="remember">
                Remember Me
              </label>
            </div>
          </div>
          <!-- /.col -->
          <div class="col-4">
           <asp:Button ID="btnLogin" runat="server" ValidationGroup="Save" Text="Login" class="btn btn-primary btn-block" />
          </div>
             <asp:CustomValidator ErrorMessage="Invalid Captcha, Please try again." ForeColor="Red" OnServerValidate="ValidateCaptcha" ValidationGroup="Save"
                            runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_loginName" ValidationGroup="Save"
                            ErrorMessage=""></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_loginpwd" ValidationGroup="Save"
                            ErrorMessage=""></asp:RequiredFieldValidator>
          <!-- /.col -->
        </div>
      <!-- /.social-auth-links -->

      <p class="mb-1">
        <a href="forgotpassword.aspx">I forgot my password</a>
      </p>

    </div>
          </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnLogin" />
            </Triggers>
        </asp:UpdatePanel>
    <!-- /.login-card-body -->
  </div>
</div>
    </form>
</body>
</html>
