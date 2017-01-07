<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Userform.aspx.cs" Inherits="ArtWebApp.Administrator.Userform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <div class="Headerdiv"> User Creation
                </div>
                <div>
                    <div class="coldiv">UserName</div>
                    <div class="coldiv"><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
                

                <br/>
               
                    <div class="coldiv">Password</div>
                    <div class="coldiv"><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></div>
               

               
                   <br/>
              
                    <div class="coldiv">Location</div>
                    <div class="coldiv"><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></div>
       
                

                     <br/>
              
                    <div class="coldiv">Location</div>
                    <div class="coldiv"><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></div>
       
                       <div class="coldiv"><asp:Button ID="submit" Text="save user" runat="server"></asp:Button></div>
                </div>
              
                
               
            </asp:View>
        </asp:MultiView>
    
    </div>
    </form>
</body>
</html>
