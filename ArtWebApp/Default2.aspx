<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Default2" CodeBehind="Default2.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
     .badge-notify{
   background:red;
   position:relative;
   top: -20px;
   left: -35px;
  }
   </style>


    <link href="css/bootstrap.css" rel="stylesheet" />
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <script>
        $(document).ready(function () {          
            var User_PK = '<%= Session["User_PK"] %>';                      
            $.ajax({
               // url: 'http://localhost:60029/api/Task/GetAllPendingTask',
                url: '/api/Task/GetAllPendingTask',
                type: 'GET',
                dataType: 'jsonp',
                ContentType: 'xml',
                data: { id: User_PK },
                success: function (data) {

                    $('#DashBoard').empty();
                    $.each(data, function (index, val) {                        

                        $('#DashBoard').append('<div class="col-md-4"> <div><button class="btn btn-default btn-lg btn-link" style="font-size: 150px;" ><span class="glyphicon glyphicon-comment"></span></button><span class="badge badge-notify" style="font-size: 30px;">' + val.LightColorStatus + '</span></div><span class="d-block bg-primary" style="font-size: 30px;" >' + val.TaskName + '</span></div > ');
                    });
                },
                error: function (error) {
                   
                }
            });
            
            $('body').on('click', 'div.col-md-4', function () {
                alert("Iam Clicked");
               
                var planpk = 0;
                PageMethods.DeletePlanAysnc(planpk, onSucess, onError);
                function onSucess(result) {
                   
                }
                function onError(result) {
                    alert('Something wrong.');
                }
                   
            });
        });

    </script>



    <div id="DashBoard" class="container">

    </div>
    
    


</asp:Content>

