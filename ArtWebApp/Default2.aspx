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
    <script src=""
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>

        $(document).ready(function () {

           
         
            $.ajax({
                url: 'http://192.168.1.4:1938/api/Task/GetAllPendingTask',
                type: 'GET',
                dataType: 'jsonp',
                ContentType: 'xml',
                data: { id: 102534 },              
                success: function (data) {

                    $('#DashBoard').empty();
                    $.each(data, function (index, val) {


                        $('#DashBoard').append(' <div class="col-md-4"> <button class="btn btn-default btn-lg btn-link" style="font-size: 150px;" ><span class="glyphicon glyphicon-comment"></span></button><span class="badge badge-notify"   style="font-size: 30px;background:red">' + val.Pending +'</span> </div>');

                    });
                },
                error: function (error) {
                  
                    alert('Error');
                }
            });


            //$.ajax({
                
            //    url: '/api/ArtApi/GetAtcs',                
            //    type: 'GET',
            //    dataType: 'jsonp',
            //    ContentType: 'json',              
            //    success: function (data) {
            //        alert("Hi");
            //        $('#DashBoard').empty();
            //        $.each(data, function (index, val) {


            //            $('#DashBoard').append(' <div class="col-md-4"> <button class="btn btn-default btn-lg btn-link" style="font-size: 150px;" ><span class="glyphicon glyphicon-comment"></span></button><span class="badge badge-notify"   style="font-size: 30px;background:red">' + val.AtcID + '</span> </div>');

            //        });
            //    },
            //    error: function (error) {

            //        alert('Error');
            //    }
            //});
        });

    </script>



    <div id="DashBoard" class="container">

    </div>
    
    


</asp:Content>

