﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DBTransaction;
using System.Data;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Web.UI.HtmlControls;
using ArtWebApp.DataModels;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ArtWebApp.Controls
{
    public static class Messagebox
    {

        public static void MessgeboxUpdate(System.Web.UI.HtmlControls.HtmlGenericControl Messaediv, String Messagetype, String Messg)
        {
            if (Messagetype == "sucess")
            {
                Messaediv.Attributes["class"] = "success";
                Messaediv.InnerText = Messg;
            }
            else
            {
                Messaediv.Attributes["class"] = "error-message";
                Messaediv.InnerText = Messg;
            }
        }









    }

    public static class currencyConvertor
    {



        public static Decimal converttousd(int pocurrency, Decimal povalue)
        {
            Decimal unitrateinusd = 0;


            Decimal convfact = 0;
            if (pocurrency == 18)
            {
                convfact = 1;
            }
            else
            {
                convfact = Getconversionfact(pocurrency);
            }
            unitrateinusd = povalue / convfact;


            return unitrateinusd;
        }


        public static Decimal Getconversionfact(int currencyid)
        {
            Decimal conv = 1;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                enty.Configuration.AutoDetectChangesEnabled = false;
                var CONVFACT = enty.POCurrExRates.Where(u => u.CurrencyID == currencyid).Select(u => u.Convrate).FirstOrDefault();
                if (CONVFACT != null)
                {
                    conv = Decimal.Parse(CONVFACT.ToString());
                }
                else
                {
                    conv = 0;
                }
            }
            return conv;
        }


    }

    public static class ConvertUOMandUnitPrice
    {
        public static ArrayList ConvertCurrencyAndUOM(int uomPK, int auomPk, float balqtyinBaseuom, float cuunitrate, String CurrencyCode, int CurrencyPk)
        {

            ArrayList QTYandPricelist = new ArrayList();




            float unitrateinpocurrecncy = 0;


            float convfact = 0;

            if (CurrencyCode.Trim() == "USD")
            {
                convfact = 1;
            }
            else
            {
                convfact = float.Parse(Getconversionfact(CurrencyPk).ToString());
            }



            unitrateinpocurrecncy = cuunitrate * convfact; //calculate the baseUOmsUnitprice






            float converttobaseqty = 0;
            float operendforUOM = 1;
            String operatorusedforUOM = "*";

            if (uomPK == auomPk)
            {
                operendforUOM = 1;
                operatorusedforUOM = "*";

                converttobaseqty = balqtyinBaseuom;

            }
            else
            {
                DataTable dt = getAltuomdata(uomPK, auomPk);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        operendforUOM = float.Parse(dt.Rows[0]["Conv_fact"].ToString());
                        operatorusedforUOM = dt.Rows[0]["Operator"].ToString().Trim();
                        if (operatorusedforUOM == "*")
                        {

                            converttobaseqty = balqtyinBaseuom * operendforUOM;

                            unitrateinpocurrecncy = unitrateinpocurrecncy / operendforUOM;
                        }
                        else if (operatorusedforUOM == "/")
                        {
                            converttobaseqty = balqtyinBaseuom / operendforUOM;

                            unitrateinpocurrecncy = unitrateinpocurrecncy * operendforUOM;
                        }
                    }
                }


            }





            QTYandPricelist.Add(Math.Ceiling(converttobaseqty));

            QTYandPricelist.Add(unitrateinpocurrecncy);







            return QTYandPricelist;


        }


        public static Decimal Getconversionfact(int currencyid)
        {
            Decimal conv = 1;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                enty.Configuration.AutoDetectChangesEnabled = false;
                var CONVFACT = enty.POCurrExRates.Where(u => u.CurrencyID == currencyid).Select(u => u.Convrate).FirstOrDefault();
                if (CONVFACT != null)
                {
                    conv = Decimal.Parse(CONVFACT.ToString());
                }
                else
                {
                    conv = 0;
                }
            }
            return conv;
        }


        /// <summary>
        /// Get the conversion factor and Operator for altuom conversion
        /// </summary>
        /// <param name="baseuom_pk"></param>
        /// <param name="altuom_pk"></param>
        /// <returns></returns>

        public static DataTable getAltuomdata(int baseuom_pk, int altuom_pk)
        {
            DataTable dt = new DataTable();






            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        Conv_fact, Operator
FROM            AltUOMMaster
WHERE        (Uom_PK = @baseuom) AND (AltUom_PK = @altuom)";
            cmd.Parameters.AddWithValue("@baseuom", baseuom_pk);
            cmd.Parameters.AddWithValue("@altuom", altuom_pk);
            SqlDataReader rdr = cmd.ExecuteReader();






            return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }


    }


    public static class DataTableFunction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromcolumn">from which column</param>
        /// <param name="tocolumn"> to which column</param>
        /// <param name="fromrow">from which row</param>
        /// <param name="torow"> to which row</param>
        /// <param name="summarycolumn">summary column index</param>
        /// <param name="summaryrow"></param>
        /// <param name="dt"> d</param>
        /// <returns></returns>

        public static DataTable SumOfDataColumns(int fromcolumn, int tocolumn, int fromrow, int torow, int summarycolumn, int summaryrow, DataTable dt)
        {


            for (int rowindex = fromrow; rowindex <= torow; rowindex++)
            {
                float rowsum = 0;
                for (int columnindex = fromcolumn; columnindex <= tocolumn; columnindex++)
                {
                    float columnvalue = 0;

                    try
                    {
                        columnvalue = float.Parse(dt.Rows[rowindex][columnindex].ToString());
                    }
                    catch (Exception)
                    {
                        dt.Rows[rowindex][columnindex] = 0;
                        columnvalue = 0;
                    }


                    rowsum = rowsum + columnvalue;



                }

                dt.Rows[rowindex][summarycolumn] = rowsum.ToString();




            }



            return dt;

        }


      




    }





    public static class Gridviewvalidation
        {


        public static int countofRowselected(GridView tbldata, String checkboxname)
        {

            int selectedrowscount = 0;
            foreach (GridViewRow di in tbldata.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl(checkboxname);

                if (chkBx != null && chkBx.Checked)
                {

                    //get the uniqueID of that row
                    selectedrowscount = selectedrowscount + 1;
                }
            }



            return selectedrowscount;
        }
    }



    
    




}