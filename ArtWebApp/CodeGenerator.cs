using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp
{
    
    public static class CodeGenerator
    {









        public static String GetUniqueCode(String DocType, String Locationcode, int pk)
        {
            String generatednum = "";

            switch (DocType.Trim())
            { 
                //Case Normal PO
                case "APO":

                    generatednum = "PO"  + pk.ToString().PadLeft(6, '0');
                    break;

                case "SPO":

                    generatednum = "SPO" + pk.ToString().PadLeft(6, '0');
                    break;

                //Documentation
                case "DocumentCreation":

                    generatednum = "DC" + Locationcode + pk.ToString().PadLeft(6, '0');
                         break;
                case "DO":

                    generatednum = "DO" + Locationcode + pk.ToString().PadLeft(6, '0');
                    break;

                case "WF":

                         generatednum = "WF" + Locationcode + pk.ToString().PadLeft(6, '0');
                         break;

                case "FW":

                    generatednum = "FW" + Locationcode + pk.ToString().PadLeft(6, '0');
                    break;
                //MRN
                case "MR":

                         if (Locationcode.Trim ()== "ATRW")
                         {
                             generatednum = "AR" + Locationcode + pk.ToString().PadLeft(6, '0');
                         }
                         else
                         {
                             generatednum = "MR" + Locationcode + pk.ToString().PadLeft(6, '0');
                         }
                         
                         break;
                     //Receipt
                 case "RCPT":
                         if (Locationcode.Trim() == "ATRW")
                         {
                             generatednum = "ARC" + Locationcode + pk.ToString().PadLeft(6, '0');
                         }
                         else
                         {
                             generatednum = "RC" + Locationcode + pk.ToString().PadLeft(6, '0');
                         }
                        
                         break;
                    //DO WW
                 case "DOWW":
                         if (Locationcode.Trim() == "ATRW")
                         {
                             generatednum = "AW" + Locationcode + pk.ToString().PadLeft(6, '0');
                         }
                         else
                         {
                             generatednum = "WW" + Locationcode + pk.ToString().PadLeft(6, '0');
                         }

                         break;
                case "SDOWW":
                    if (Locationcode.Trim() == "ATRW")
                    {
                        generatednum = "SAW" + Locationcode + pk.ToString().PadLeft(6, '0');
                    }
                    else
                    {
                        generatednum = "SWW" + Locationcode + pk.ToString().PadLeft(6, '0');
                    }

                    break;

                case "SWF":

                    generatednum = "SWF" + Locationcode + pk.ToString().PadLeft(6, '0');
                    break;


                //Job COntract
                case "JC":

                         generatednum = "JC" +  pk.ToString().PadLeft(6, '0');
                         break;
                 //Job COntract
                 case "JCO":

                         generatednum = "JCO" + pk.ToString().PadLeft(6, '0');
                         break;


                    //Shipment Handover

                 case "SH":

                         generatednum = "SH" + pk.ToString().PadLeft(6, '0');
                         break;

                 case "INV":

                         generatednum = "ASN#" + Locationcode + pk.ToString().PadLeft(6, '0');
                         break;
                case "EXP":

                    generatednum = "EXP#" + Locationcode + pk.ToString().PadLeft(6, '0');
                    break;

                case "LN":

                    generatednum = "LN#" + Locationcode + pk.ToString().PadLeft(6, '0');
                    break;

                case "Support" :

                      generatednum = "TK#" + Locationcode + pk.ToString().PadLeft(6, '0');
                         break;

                case "GT":
                    // trassfer from atc to Gstock
                    generatednum = "GT#" + Locationcode + pk.ToString().PadLeft(6, '0');
                    break;
                case "CPL":
                    //CutPlan
                    generatednum = "CPL#" + Locationcode + pk.ToString().PadLeft(6, '0');
                    break;
                default:

                    break;



            }



            return generatednum;
        }












    }
}