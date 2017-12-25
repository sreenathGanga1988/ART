using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.CuttingMVC.Models
{
    public class CutplanViewModel
    {

        public CutplanViewModel(int cutplanPk)
        {
            this.cutplanPk = cutplanPk;
        }
        public CutplanViewModel()
        {

        }

        public int cutplanPk { get; set; }

        public int cutid { get => GetCutorder(this.cutplanPk); set => cutid = value; }

        public CutPlanMaster cutPlanMaster { get; set; }

        public String[] CutPlanMarkerTypes { get; set; }

        public ASQMAster aSQMAster { get; set; }


        public String MarkerMade { get; set; }


        public decimal[] SelectedPopackID{ get; set; }

        private List<ASQMAster> aSQMAsters;

        public List<ASQMAster> ASQMAsters { get => GetASQMAstersList(); set => aSQMAsters = value; }
        public DataTable HeaderTableData { get => GetHeaderData(); set => HeaderTableData = value; }

        

        public List<ASQMAster> GetASQMAstersList()
        {
            cutplantransactionedit cutplantransactionedit = new cutplantransactionedit();
            List<ASQMAster> asqlist = new List<ASQMAster>();
            DataTable podetaildata = BLL.CutOrderBLL.CutPlan.GetCutPlanASQSizeData(this.cutplanPk);
            DataView view = new DataView(podetaildata);
            DataTable distinctcolorValues = view.ToTable(true, "ASQ", "BuyerPO", "PoPackId", "OurStyleID", "OurStyle", "BuyerStyle", "CutPlan_PK", "SeasonName", "HandoverDate", "ColorName");


            String ourstyleid = "0";
            String colorname = "";
            String ourstylename = "";
            String BuyerStyle = "";
            String Handoverdate = "";
            String CutplanPk = "";
            foreach (DataRow row in distinctcolorValues.Rows)
            {
                ASQMAster asq = new ASQMAster();
                asq.ASQ = row["ASQ"].ToString();

                asq.BuyerPO = row["BuyerPO"].ToString();
                asq.Popackid = row["PoPackId"].ToString();

                asq.OurStyleID = row["OurStyleID"].ToString();
                asq.OurStyle = row["OurStyle"].ToString();

                asq.CutPlan_PK = row["CutPlan_PK"].ToString();
                asq.ColorName = row["ColorName"].ToString();


                asq.BuyerStyle = row["BuyerStyle"].ToString();

                asq.SeasonName = row["SeasonName"].ToString();

                asq.HandoverDate = row["HandoverDate"].ToString();

                ourstyleid = asq.OurStyleID;
                colorname = asq.ColorName;
                ourstylename = asq.OurStyle; ;
                BuyerStyle = asq.BuyerStyle;
               Handoverdate = asq.HandoverDate;
                CutplanPk = asq.CutPlan_PK;

            
                asq.SizewiseDetails = cutplantransactionedit.getSizewisedetails(podetaildata, int.Parse(asq.Popackid), int.Parse(asq.OurStyleID));
                asq.SizewiseDetailsWithDetPK= podetaildata.Select("PoPackId = " + asq.Popackid + " AND OurStyleID=" + asq.OurStyleID + "").CopyToDataTable();

                asqlist.Add(asq);


            }




            if (this.SelectedPopackID != null)
            {
                foreach (Decimal Popackidtemp in this.SelectedPopackID)
                {

                    using (ArtEntitiesnew enty = new ArtEntitiesnew())
                    {

                        ASQMAster q = (from popackmastr in enty.PoPackMasters
                                      where popackmastr.PoPackId == Popackidtemp
                                      select new ASQMAster
                                      {
                                          ASQ = popackmastr.PoPacknum,
                                          BuyerPO = popackmastr.BuyerPO,
                                          Popackid = popackmastr.PoPackId.ToString(),
                                          OurStyleID = ourstyleid,
                                          OurStyle = ourstylename,
                                          CutPlan_PK = CutplanPk,
                                    ColorName = colorname,
                                    BuyerStyle = BuyerStyle,
                                    SeasonName = popackmastr.SeasonName,
                                    HandoverDate = Handoverdate
                                }).FirstOrDefault();


                        q.SizewiseDetailsWithDetPK = BLL.CutOrderBLL.CutPlan.GetASQSizeData(int.Parse(Popackidtemp.ToString()), int.Parse(q.OurStyleID.ToString()),colorname);
                                                
                        asqlist.Add(q);
                    }
                }
            }





            return asqlist;


        }


        public int GetCutorder(int cutplanpk)
        {
            int cutid = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var cutidvar = enty.CutOrderMasters.Where(u => u.CutPlan_Pk == cutplanpk).Select(u => u.CutID).FirstOrDefault();
                cutid = int.Parse(cutidvar.ToString());
            }

            return cutid;
        }



        public DataTable GetHeaderData()
        {

            cutplantransactionedit cutplantransactionedit = new cutplantransactionedit();


            int cutorderdetpk = this.cutid;

            DataTable dt = cutplantransactionedit.Fillsizedata(int.Parse(this.cutPlanMaster.OurStyleID.ToString()));
            return cutplantransactionedit.GenerateCutorderTable(dt, cutorderdetpk);
        }







    }


    public class AsqCollection
    {
       

        public String PoPackId { get; set; }
        public String OurStyleID { get; set; }
        public String CutPlan_PK { get; set; }
        public String ColorName { get; set; }
        public String SizeName { get; set; }
        public String Qty { get; set; }
       
    }


    public class AsqDetCollection
    {


        public String PoPackdetID { get; set; }
        public String CutPlan_PK { get; set; }
        public String ActualCutPlan_PK { get; set; }
        public String Qty { get; set; }


    }



    public class ASQMAster
    {

        public String ASQ { get; set; }

        public String BuyerPO { get; set; }
        public String Popackid { get; set; }

        public String PoPackId { get; set; }
        public String OurStyleID { get; set; }

        public String OurStyle { get; set; }
        public String BuyerStyle { get; set; }

        public String CutPlan_PK { get; set; }
        public String ColorName { get; set; }
        public String SizeName { get; set; }
        public String SeasonName { get; set; }
        public String Qty { get; set; }
        public String HandoverDate { get; set; }
        
        public DataTable SizewiseDetails { get; set; }

        public DataTable SizewiseDetailsWithDetPK { get; set; }

    }


  public class cutplantransactionedit
    {


        public DataTable Fillsizedata(int ourstyleid)
        {
            DataTable dt = new DataTable();
            try
            {


                
                    dt = Createdatatable(ourstyleid);
                    
               

            }
            catch
            {

            }

            return dt;
        }
        public System.Data.DataTable Createdatatable(int ourstyleid)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("", typeof(String));

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var sizedetails = (from size in enty.StyleSizes
                                   where size.OurStyleID == ourstyleid
                                   select new
                                   {
                                       size.SizeName
                                   }).Distinct();

                foreach (var sizedet in sizedetails)
                {
                    dt.Columns.Add(sizedet.SizeName.Trim(), typeof(String));
                }

                dt.Columns.Add("Total", typeof(String));

                DataRow row = dt.NewRow();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row[i] = 0;
                }

                dt.Rows.Add(row);


                DataRow row1 = dt.NewRow();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row[i] = 0;
                }

                dt.Rows.Add(row1);

                DataRow AlreadyCutrow = dt.NewRow();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row[i] = 0;
                }

                dt.Rows.Add(AlreadyCutrow);
                DataRow BalanceCutrow = dt.NewRow();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row[i] = 0;
                }

                dt.Rows.Add(BalanceCutrow);



            }




            return dt;
        }
        public DataTable GenerateCutorderTable(DataTable dt ,int cutplanpk)
        {
            BLL.CutOrderBLL.CutDetailsData cddetdataclass = new BLL.CutOrderBLL.CutDetailsData();
            int cutorderpk = cutplanpk;
            DataTable cutdetdata = new DataTable();
           cutdetdata = cddetdataclass.GetCutOrderMasterSizeDataofCutorder(cutorderpk);

          //  cutdetdata = BLL.CutOrderBLL.CutPlan.fillCutplanqty(cutorderpk);
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ReadOnly = false;
                String SIZENAME = dt.Columns[i].ColumnName.ToString();


                try
                {
                    object Sumtrim = cutdetdata.Compute("Sum(Qty)", " Size ='" + SIZENAME + "'");

                    if (Sumtrim.ToString().Trim() == "")
                    {
                        Sumtrim = "0";
                    }

                    dt.Rows[0][i] = Sumtrim.ToString();
                }
                catch (Exception)
                {

                    dt.Rows[0][i] = "0";
                }
                try
                {
                    object Sumtrim = cutdetdata.Compute("Sum(Ratio)", " Size ='" + SIZENAME + "'");



                    if (Sumtrim.ToString().Trim() == "")
                    {
                        Sumtrim = "0";
                    }

                    dt.Rows[1][i] = Sumtrim.ToString();




                }
                catch (Exception ex)
                {

                    dt.Rows[1][i] = "0";
                }
                try
                {
                    object Sumtrim = cutdetdata.Compute("Sum(CutQty)", " Size ='" + SIZENAME + "'");



                    if (Sumtrim.ToString().Trim() == "")
                    {
                        Sumtrim = "0";
                    }

                    dt.Rows[2][i] = Sumtrim.ToString();



                }
                catch (Exception ex)
                {

                    dt.Rows[2][i] = "0";
                }



                dt.Rows[3][i] = (float.Parse(dt.Rows[0][i].ToString()) - float.Parse(dt.Rows[2][i].ToString())).ToString();



            }


            //  dt = BLL.ProductionBLL.LaySheetfunction.CalculateAlreadyCut(dt);
            DataRow row1 = dt.NewRow();
            row1[0] = "New Value";
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                row1[i] = dt.Rows[0][i].ToString();
            }
            dt.Rows.Add(row1);

            dt = ArtWebApp.Controls.DataTableFunction.SumOfDataColumns(1, dt.Columns.Count - 2, 0, dt.Rows.Count - 1, dt.Columns.Count - 1, dt.Rows.Count - 1, dt);


            dt.Rows[0][0] = "Planned";

            dt.Rows[1][0] = "Ratio";

            dt.Rows[2][0] = "Layed";

            dt.Rows[3][0] = "Balance";


            return dt;
        }

        public DataTable getSizewisedetails( DataTable dttemp, int popackid ,int ourstyleid)
        {
           DataTable dt = BLL.popackupdater.createdatatableforCutplanASQ(dttemp, popackid, ourstyleid);
            DataRow dataROW = dt.Rows[dt.Rows.Count - 1];
            DataRow row1 = dt.NewRow();
            row1[0] = "New Value";
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                row1[i] = dataROW[i].ToString();
            }

            dt.Rows.Add(row1);
            return dt;
        }

    }

}