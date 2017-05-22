<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArtLogin.aspx.cs" Inherits="ArtWebApp.ArtLogin" %>


<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Atraco ERP -WE CAN DO IT</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/landing-page.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="http://fonts.googleapis.com/css?family=Lato:300,400,700,300italic,400italic,700italic" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
   
</head>

<body>

<div class="login-wrapper">

<div class="login-main-box">

<div class="login-logo"><img src="image/atraco-logo.png"></div><!--login-logo end-->

<div class="login-field-main">
<form  id="signin" role="form" runat="server">

    <div class="text-field-main">
    	<!--<input type="text" placeholder="Username" name="" required class="text-field username">-->
        <asp:TextBox ID="email" placeholder="Username" runat="server" class="text-field username"></asp:TextBox>
    </div><!--text-field-main end-->
    
    
     <div class="text-field-main">
    	<!--<input type="password" placeholder="Password" name="" required class="text-field password">-->
        <asp:TextBox ID="password" placeholder="Password"  runat="server" TextMode="Password" class="text-field password"></asp:TextBox>
        
    </div><!--text-field-main end-->
    
    
    
    <div class="text-field-main">
    	<!--<input type="submit" onclick="valContactForm()" value="Login" class="text-field-but">-->
        
        <asp:Button ID="btn_login" runat="server" Text="Login" OnClick="btn_login_Click" class="text-field-but" />
        
    </div><!--text-field-main end-->
    
    
    
    <div class="text-field-main">
    
    <div class="remember">
    <input type="checkbox" id="option">
    <label for="option"> <span></span> &nbsp; Remember Me</label>
    </div>
    
    <div class="forgot-pass"><a href="#">Forgot password ?</a></div>
  
  
  <div class="clear"></div>
    </div><!--text-field-main end-->
    
    
                            
</form>                            
                            
</div><!--login-field-main end-->



</div><!--login-main-box end-->


<div class="footer-main"> © 2016 All Rights Reserved. atracogroup.com VER :1.0.0.1</div><!--footer-main end-->

</div><!--login-wrapper end-->  
   
   
   
   
   
</body>

</html>