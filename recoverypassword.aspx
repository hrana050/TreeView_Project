<%@ Page Language="C#" AutoEventWireup="true" CodeFile="recoverypassword.aspx.cs" Inherits="recoverypassword" %>

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
      <script src="http://code.jquery.com/jquery-1.11.3.js" type="text/javascript"></script>

    <script>
        function checkPasswordMatch() {
            var msg = $("#spnMsg")[0];
            var password = $("#<%=txt_pwd.ClientID%>")[0].value;
            var confirmPassword = $("#<%=txt_confirm.ClientID%>")[0].value;

            if (password != confirmPassword) {
                if (confirmPassword != "") {
                    msg.style.display = "block";
                    msg.style.color = "red";
                    msg.innerHTML = "Passwords do not match!";
                    document.getElementById('<%=btn_forgot.ClientID %>').disabled = true;
                }
                else
                {
                    msg.style.display = "none";
                    document.getElementById('<%=btn_forgot.ClientID %>').disabled = false;
                }
            }
            else {
                msg.style.display = "block";
                msg.style.color = "green";
                msg.innerHTML = "Passwords match.";
                document.getElementById('<%=btn_forgot.ClientID %>').disabled = false;
            }
        }
    </script>
</head>
<body class="login-page">
    <form id="form1" runat="server">
         <asp:ScriptManager ID="sp1" runat="server"></asp:ScriptManager>
    <div class="login-box">
  <div class="login-logo">
    <a href="#"><b>KRM</b> Admin Panel</a>
  </div>
<div class="card">
    <div class="card-body login-card-body">
      <asp:Label ID="Label1" runat="server" style="color:green" Visible="false" class="login-box-msg"></asp:Label>

      <p class="login-box-msg"><asp:Label ID="Label2" runat="server" Text="You are only one step a way from your new password, recover your password now."></asp:Label></p>
        <div class="input-group mb-3">
          <asp:TextBox ID="txt_pwd" runat="server" class="form-control" placeholder="New Password" TextMode="Password" required="autofocus"></asp:TextBox>
          <div class="input-group-append">
            <div class="input-group-text">
              <span class="fas fa-lock"></span>
            </div>
          </div>
        </div>
        <div class="input-group mb-3">
         <asp:TextBox ID="txt_confirm" runat="server" class="form-control" placeholder="Confirm Password" onChange="checkPasswordMatch();" TextMode="Password" required="autofocus"></asp:TextBox>
          <div class="input-group-append">
            <div class="input-group-text">
              <span class="fas fa-lock"></span>
            </div>
          </div>
        </div>
           <div class="input-group mb-3">
                  <span id="spnMsg"></span>
               </div>
        <div class="row">
          <div class="col-12">
            <asp:Button ID="btn_forgot" runat="server" Text="Submit" class="btn btn-primary btn-block" OnClick="btn_forgot_Click"/>
          </div>
          <!-- /.col -->
        </div>
      <p class="mt-3 mb-1">
        <a href="Login.aspx">Login</a>
             <a href="forgotpassword.aspx" style="float:right">Back</a>
      </p>
    </div>
    <!-- /.login-card-body -->
  </div>
</div>
    </form>
</body>
</html>

