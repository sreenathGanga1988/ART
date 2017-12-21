<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Default2" Codebehind="Default2.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
     <style type="text/css">
     
   </style>
<%--    <script src="Scripts/jquery-1.6.4.js"></script>
    <script src="Scripts/jquery.signalR-2.2.1.js"></script>


       <script type="text/javascript">
        $(function () {
            debugger;
            var connection = $.hubConnection("")
            var hub = connection.createHubProxy('artHub');
            hub.on('SayMessage', function () {

                alert('hi');
            });


            connection.start().done();
            //connection.start(function () {

            //    hub.invoke('SayMessage');
            //});
        });

    </script>--%>

    <link href="css/bootstrap.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div class="container ">
        <div class="col-md-8">
          <asp:HyperLink ID="hprlink" NavigateUrl="/ArtMVCMaster/UserMasters" runat="server">Dear Art user if you are viewing this message you User Profile or password is too weak or not updated correctly. Please Update it Either By clicking this Link or By clicking your  User Name in the Top right of this page </asp:HyperLink>
         
    </div>

    <div class="col-md-2">
                       
             
  

       
  
        
       
       
  
      
       
  
        
</div>
    </div>

    
  
</asp:Content>

