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

                    <div id="messagediv" runat="server"  visible="false" style="font-family: Calibri; font-size: large; font-weight: bold; font-style: normal; font-variant: normal; text-transform: uppercase; width: 100%; height: 100%; border: thick dashed #000080">
                 <%--  <marquee id="mq" direction="DOWN" scrollamount="1"  loop="true" onmouseover="this.stop();" önmouseout="this.start();">   --%>

   <asp:HyperLink ID="hprlink" NavigateUrl="/ArtMVCMaster/UserMasters" runat="server">Dear Art user if you are viewing this message you User Profile or password is too weak or not updated correctly. Please Update it Either By clicking this Link or By clicking your  User Name in the Top right of this page </asp:HyperLink>
                        
                       <%-- </marquee>
                 --%>


                        

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

