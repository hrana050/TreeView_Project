<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgotpassword.aspx.cs" Inherits="forgotpassword" %>

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
    <script type="text/javascript">

        function CheckemailAvailability() {
            //This function call on text change.             
            $.ajax({
                type: "POST",
                url: 'forgotpassword.aspx/CheckemailAvailability', // this for calling the web method function in cs code.  
                data: '{usercontact: "' + $("#<%=txt_emailid.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response);
                }
            });
        }
        function OnSuccess(response) {
            var txtvalue = $("#<%=txt_emailid.ClientID%>")[0].value;
            var msg = $("#spnMsg")[0];
            switch (response.d) {
                case "false":
                    if (txtvalue != "")
                    {
                    msg.style.display = "block";
                    msg.style.color = "red";
                    msg.innerHTML = "(" + txtvalue + ")" + " emailid does not match please enter Valid E-mail id";
                    document.getElementById('<%=btn_forgot.ClientID %>').disabled = true;
                    }
                    else
                    {
                        msg.style.display = "none";
                        document.getElementById('<%=btn_forgot.ClientID %>').disabled = false;
                    }
                    break;
                case "true":
                    msg.style.display = "block";
                    msg.style.color = "green";
                    msg.innerHTML = "(" + txtvalue + ")" + " EmailId Matched";
                    document.getElementById('<%=btn_forgot.ClientID %>').disabled = false;
                     break;
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
      <p class="login-box-msg">You are only one step a way from your new password</p>
        <div class="input-group mb-3">
       <asp:TextBox ID="txt_emailid" runat="server" class="form-control" placeholder="Enter Your Email Id" onchange="CheckemailAvailability()" required="autofocus" AutoComplete="off"></asp:TextBox>
          <div class="input-group-append"> 
            <div class="input-group-text">
              <span class="fas fa-envelope"></span>
            </div>
          </div>
              
        </div>
        <div class="input-group mb-3">
              <span id="spnMsg"></span>
            </div>
       
        <div class="row">
          <div class="col-12">
           <asp:Button ID="btn_forgot" runat="server" Text="Change password" class="btn btn-primary btn-block" OnClick="btn_forgot_Click"/>
          </div>
          <!-- /.col -->
        </div>


      <p class="mt-3 mb-1">
        <a href="Login.aspx">Login</a>
      </p>
    </div>
    <!-- /.login-card-body -->
  </div>
</div>
    </form>
</body>
</html>

