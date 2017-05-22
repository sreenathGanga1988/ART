<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Default2" Codebehind="Default2.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/style.css" rel="stylesheet" />   
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

  
     <link href="css/MasterPage.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div>
       
  
      
       
  
        
        <table class="FullTable" >
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr class="DataEntryTable">
                <td>
                    
                    <div class="RedHeaddingdIV">
                    Alert
                    </div>

                    <div>
                        <%--<marquee behavior="scroll" direction="left" scrollamount="2">
<asp:HyperLink ID="HyperLink1" runat="server">Department Heads  are requested to create a list of menus which need to be enabled to   each user (userrights) before 15-jan-2017 </asp:HyperLink>

                           
</marquee>--%>

                        

                    </div>
                    <%--<div>
                        <marquee id="mq" direction="DOWN" scrollamount="2" loop="true" onmouseover="this.stop();" önmouseout="this.start();">   
    <asp:HyperLink ID="HyperLink2" runat="server">All Merchandisers are requested to complete all the ADN with ETA before  30 dec asap and any ADN after that will be submitted to shipping dept</asp:HyperLink>
    </marquee>

                    </div>--%>
                        <%--<div>
                        <marquee id="mq" direction="DOWN" scrollamount="2" loop="true" onmouseover="this.stop();" önmouseout="this.start();">   
    <asp:HyperLink ID="HyperLink3" runat="server">It is mandatory for All merchanduiser to select correct ADN Type to help the store and Shipping in clearing the shipment </asp:HyperLink>
    </marquee>

                    </div>--%>
                </td>
            </tr>
           
            <tr>
                <td> </td>
            </tr>
        </table>
       
  
      
       
  
        
</div>
   
</asp:Content>

