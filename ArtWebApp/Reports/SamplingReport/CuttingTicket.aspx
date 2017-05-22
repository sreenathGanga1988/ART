<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CuttingTicket.aspx.cs" Inherits="ArtWebApp.Reports.SamplingReport.CuttingTicket" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 600px;
            height: 851px;
        }
        .auto-style2 {
            width: 100%;
            font-size: medium;
            font-family: Calibri;
            height: 4px;
        }
        .auto-style3 {
            width: 329px;
        }
        .auto-style4 {
            text-align: left;
            font-size: x-small;
            border-left-style: solid;
            border-top-style: outset;
            border-bottom-style: outset;
        }
        .auto-style10 {
            font-size: 16pt;
        }
        .auto-style11 {
            text-align: center;
            font-size: large;
            border-left-style: solid;
            border-top-style: outset;
            border-bottom-style: outset;
        }
        .auto-style12 {
            width: 141px;
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            font-size: x-small;
        }
        .auto-style13 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            text-align: left;
            width: 141px;
            font-size: x-small;
        }
        .auto-style14 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
        }
        .auto-style15 {
            border-left-style: solid;
            border-top-style: outset;
            border-bottom-style: outset;
        }
        .auto-style16 {
            text-align: center;
            border-left-style: solid;
            border-top-style: outset;
            border-bottom-style: outset;
        }
        .auto-style17 {
            font-size: x-small;
        }
        .auto-style19 {
            font-size: small;
        }
        .auto-style20 {
            font-size: x-small;
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
        }
        .auto-style24 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            height: 16px;
            width: 70px;
        }
        .auto-style25 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            height: 12px;
        }
        .auto-style27 {
            width: 100%;
            font-size: small;
            font-family: Calibri;
        }
        .auto-style28 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            text-align: center;
        }
        .auto-style30 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            width: 71px;
            font-weight: bold;
        }
        .auto-style32 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            width: 70px;
        }
        .auto-style34 {
            font-size: x-small;
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            height: 7px;
        }
        .auto-style35 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            width: 71px;
            font-weight: bold;
            height: 7px;
        }
        .auto-style36 {
            width: 70px;
        }
        .auto-style37 {
            font-size: x-small;
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            width: 117px;
        }
        .auto-style38 {
            font-size: x-small;
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            height: 7px;
            width: 117px;
        }
        .auto-style39 {
            font-size: medium;
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
        }
        .auto-style40 {
            font-size: xx-small;
        }
        .auto-style41 {
            width: 605px;
            height: 817px;
        }
        .auto-style42 {
            width: 100%;
            font-size: medium;
            font-family: Calibri;
            height: 238px;
        }
        .auto-style43 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            text-align: left;
            font-size: x-small;
        }
        </style>
</head>
<body>

    <form id="form1" runat="server">
    <div class="auto-style41">
        <table class="auto-style1">
        <tr>
            <td>
                <table class="auto-style2">
                    <tr>
                        <td class="auto-style3">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style10"><strong>ATRACO INDUSTRIAL ENTERPRISES</strong></td>
                                </tr>
                                <tr>
                                    <td>P.O Box 16798,Dubai -U.A.E</td>
                                </tr>
                                <tr>
                                    <td>Tel:(971 4)8812686</td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-family: Calibri">
                <table class="auto-style2">
                    <tr>
                        <td class="auto-style11"><strong>SAMPLE BOOK</strong></td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            
                            <table class="auto-style2">
                                <tr class="auto-style19">
                                    <td class="auto-style12"><strong>ATC #:</strong></td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_atc" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style43"><strong>BUYER STYLE # :</strong></td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_buyerstyle" runat="server" Text="Label"></asp:Label>
                                                </td>
                                </tr>
                                <tr class="auto-style19">
                                    <td class="auto-style13"><strong>BUYER #:</strong></td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_buyer" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style43">&nbsp;</td>
                                    <td class="auto-style14">
                                                    &nbsp;</td>
                                </tr>
                                <tr class="auto-style19">
                                    <td class="auto-style13"><strong>MARCHANDISER NAME</strong></td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_merch" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style43">MASTER&nbsp;</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_master" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style16">BUYER REQUIREMNET</td>
                    </tr>
                    <tr>
                        <td class="auto-style15">
                            <table class="auto-style2">
                                <tr>
                                    <td>
                                        <table class="auto-style42">
                                            <tr>
                                                <td class="auto-style20"><strong>CUT ID</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_cutid" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style20"><strong>FABRIC TYPE</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_fabrictype" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style20"><strong>WIDTH</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_width" runat="server"  CssClass="auto-style40" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style20"><strong>DESCRIPTION</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_description" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style34"><strong>ORIGINAL SAMPLE</strong></td>
                                                <td class="auto-style35">
                                                    <asp:Label ID="lbl_originalsample" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style20"><strong>SPEC</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_spec" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style20"><strong>STYLE DIGRAME</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_styledaigram" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style20"><strong>PATTERN</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_pattern" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table class="auto-style2">
                                            <tr class="auto-style17">
                                                <td class="auto-style32">
                                                    <strong>DEVELOPMENT</strong></td>
                                                <td class="auto-style32">
                                                    <asp:CheckBox ID="chk_development"  runat="server" />
                                                </td>
                                                <td class="auto-style32"><strong>PROTO</strong></td>
                                                <td class="auto-style32">
                                                    <asp:CheckBox ID="chk_proto" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style32">
                                                    <strong>1ST FIT SAMPLE</strong></td>
                                                <td class="auto-style32">
                                                    <asp:CheckBox ID="chk_1stfitsample" runat="server" />
                                                </td>
                                                <td class="auto-style32">
                                                    <strong>2ND FIT SAMPLE</strong></td>
                                                <td class="auto-style32">
                                                    <asp:CheckBox ID="chk_2ndfitsample" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style32">
                                                    <strong>3RD FIT SAMPLE</strong></td>
                                                <td class="auto-style32">
                                                    <asp:CheckBox ID="chk_3rdfitsample" runat="server" />
                                                </td>
                                                <td class="auto-style32">
                                                    <strong>SIZE SET</strong></td>
                                                <td class="auto-style32">
                                                    <asp:CheckBox ID="chk_sizeset" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style32">
                                                    <strong>PP SAMPLE</strong></td>
                                                <td class="auto-style32">
                                                    <asp:CheckBox ID="chk_ppsample" runat="server" />
                                                </td>
                                                <td class="auto-style32">
                                                    <strong>PHOTO SAMPLE</strong></td>
                                                <td class="auto-style32">
                                                    <asp:CheckBox ID="chk_photosample" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style24">
                                                    <strong>STYLING</strong></td>
                                                <td class="auto-style24">
                                                    <asp:CheckBox ID="chk_styling" runat="server" />
                                                </td>
                                                <td class="auto-style24">
                                                    <strong>MTL</strong></td>
                                                <td class="auto-style24">
                                                    <asp:CheckBox ID="chk_mtl" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style32">
                                                    <strong>BOOKING</strong></td>
                                                <td class="auto-style32">
                                                    <asp:CheckBox ID="chk_booking" runat="server" />
                                                </td>
                                                <td class="auto-style32">
                                                    <strong>COSTING</strong></td>
                                                <td class="auto-style32">
                                                    <asp:CheckBox ID="chk_costing" runat="server" />
                                                </td>
                                            </tr>
                                            </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="auto-style2">
                    <tr>
                         
                        <td class="auto-style19">
                            <table class="auto-style27">
                                <tr>
                                    <td class="auto-style28" colspan="2">&nbsp;</td>
                                    <td class="auto-style28">&nbsp;</td>
                                    <td class="auto-style28">&nbsp;</td>
                                    <td class="auto-style28">&nbsp;</td>
                                    <td class="auto-style28"><strong>TOTAL</strong></td>
                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14"><strong>SIZE</strong></td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_size1" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_size2" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_size3" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_size4" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="auto-style14">
                                                    &nbsp;</td>
                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14"><strong>QTY</strong></td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_qty1" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_qty2" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_qty3" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_qty4" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_totalqty" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14">&nbsp;</td>
                                    <td class="auto-style14">
                                                    &nbsp;</td>
                                    <td class="auto-style14">
                                                    &nbsp;</td>
                                    <td class="auto-style14">
                                                    &nbsp;</td>
                                    <td class="auto-style14">
                                                    &nbsp;</td>
                                    <td class="auto-style14">
                                                    &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                </table>
                        </td>
                        <td>
                            <table class="auto-style27">
                                <tr>
                                    <td class="auto-style28" colspan="4">&nbsp;</td>


                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14"><strong>POCKETING</strong></td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_pocketing" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style14">&nbsp;</td>
                                    <td class="auto-style14">
                                                    &nbsp;</td>
                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14"><strong>INTERLININ</strong></td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_interlinin" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style14">&nbsp;</td>
                                    <td class="auto-style14">
                                                    &nbsp;</td>
                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14">&nbsp;</td>
                                    <td class="auto-style14">
                                                    &nbsp;</td>
                                    <td class="auto-style14">&nbsp;</td>
                                    <td class="auto-style14">
                                                    &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
              




            </td>
        </tr>
            <td class="auto-style15">
         <table class="auto-style2">
                                <tr>
                                    <td>
                                        <table class="auto-style2">
                                            <tr>
                                                <td class="auto-style39" colspan="2"><strong>LABLES</strong></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style37"><strong>MAIN LABLE</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_mainlable" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style37"><strong>CARE LABLE</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_carelable" runat="server" CssClass="auto-style40" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style37"><strong>SIZE LABLE</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_sizelable" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style38"><strong>JOKER</strong></td>
                                                <td class="auto-style35">
                                                    <asp:Label ID="lbl_joker" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style38"><strong>TAPE LABLE</strong></td>
                                                <td class="auto-style35">
                                                    <asp:Label ID="lbl_tapelable" runat="server" CssClass="auto-style40" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style38"><strong>THREAD</strong></td>
                                                <td class="auto-style35">
                                                    <asp:Label ID="lbl_thread" runat="server" CssClass="auto-style17" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style37"><strong>BUTTONS</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_buttons" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style37"><strong>ZIPPER-NICK EL</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_zippernickel" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style37"><strong>LINED</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_lined" runat="server" CssClass="auto-style17" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style37"><strong>OTHERS</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_others" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style37"><strong>BUTTON HOLES</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_buttonholes" runat="server" CssClass="auto-style40" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="auto-style36">
                                        <table class="auto-style2">
                                            <tr class="auto-style17">
                                                <td class="auto-style32"><strong>WASHING INSTRUCTION</strong></td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style32">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style32">
                                                    &nbsp;</td>
                                            </tr>
                                            </table>
                                    </td>
                                    <td style="font-weight: 700">
                                        <table class="auto-style2">
                                            <tr class="auto-style17">
                                                <td class="auto-style14">&nbsp;</td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style14">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style14">
                                                    <asp:Label ID="lbl_washinginstruction" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style14">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style25">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style14">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style14">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
        </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>

