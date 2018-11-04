using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.InventoryBLL
{
    public class PoReceipt
    {
        public RecieptMasterData rcptmstrdata { get; set; }
        public MrnMasterData mrnmstrdata { get; set; }
        public List<MrnDetailsData> MrnDetailsDataCollection { get; set; }
        


        public String insertMRNData(PoReceipt Porcpt)
        {
            String mrnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                MrnMaster mrnmstr = new MrnMaster();
                mrnmstr.DoNumber = Porcpt.mrnmstrdata.DoNumber;
                mrnmstr.AddedDate = DateTime.Now;
                mrnmstr.Po_PK = Porcpt.mrnmstrdata.Po_PK;
                mrnmstr.AddedBY = Porcpt.mrnmstrdata.AddedBY;

                mrnmstr.Location_Pk = Porcpt.mrnmstrdata.Location_Pk;
                mrnmstr.Reciept_Pk = int.Parse(Porcpt.mrnmstrdata.Reciept_Pk.ToString ());
                mrnmstr.MrnClosed = "N";
                enty.MrnMasters.Add(mrnmstr);
                enty.SaveChanges();


              //  mrnmstr.MrnNum = "MR" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + mrnmstr.Mrn_PK.ToString().PadLeft(6, '0');
                mrnmstr.MrnNum = CodeGenerator.GetUniqueCode("MR", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(mrnmstr.Mrn_PK.ToString()));
                foreach (MrnDetailsData mrnrdet in Porcpt.MrnDetailsDataCollection)
                 {      MrnDetail mrndetdb = new MrnDetail();
                 mrndetdb.Mrn_PK = mrnmstr.Mrn_PK;
                 mrndetdb.PODet_PK = mrnrdet.PODet_PK;
                 mrndetdb.SkuDet_PK = mrnrdet.SkuDet_PK;
                 mrndetdb.ReceiptQty = mrnrdet.ReceiptQty;
                 mrndetdb.ExtraQty = mrnrdet.ExtraQty;
                 mrndetdb.Remark = mrnrdet.Remark;
                 mrndetdb.Uom_PK = mrnrdet.Uom_PK;
                 mrndetdb.Doc_Pk = mrnrdet.Doc_pk;
                 enty.MrnDetails.Add(mrndetdb);

                        enty.SaveChanges();

                        InventoryMaster invmstr = new InventoryMaster();
                        invmstr.MrnDet_PK = mrndetdb.MrnDet_PK;
                        invmstr.PoDet_PK = mrnrdet.PODet_PK;
                        invmstr.SkuDet_Pk = mrnrdet.SkuDet_PK;
                        invmstr.ReceivedQty = mrnrdet.ReceiptQty + mrnrdet.ExtraQty;
                        invmstr.OnhandQty = mrnrdet.ReceiptQty + mrnrdet.ExtraQty;
                        invmstr.DeliveredQty = 0;
                        invmstr.ReceivedVia = "MR";
                        invmstr.Location_PK = mrnmstr.Location_Pk;
                        invmstr.Refnum = mrnmstr.MrnNum;
                        invmstr.CURate = mrnrdet.Curate;
                        invmstr.AddedDate = DateTime.Now.Date;
                        invmstr.Uom_Pk = mrnrdet.Uom_PK;
                        enty.InventoryMasters.Add(invmstr);                    
                }
               
                enty.SaveChanges();

                mrnum = mrnmstr.MrnNum;
            }

            return mrnum;
        }
    }

      public class MrnMasterData
        {
            public int Mrn_PK { get; set; }
            public string MrnNum { get; set; }
            public int Po_PK { get; set; }
            public DateTime DeliveryDate { get; set; }
            public string Remark { get; set; }
            public string AddedBY { get; set; }
            public DateTime AddedDate { get; set; }
            public string DoNumber { get; set; }
            public int Location_Pk { get; set; }

            public int Reciept_Pk { get; set; }


     


         

        }

      public class MrnDetailsData
        {
            public int MrnDet_PK { get; set; }
            public int Mrn_PK { get; set; }
        public int Doc_pk { get; set; }
        public int PODet_PK { get; set; }
            public int SkuDet_PK { get; set; }
            public Decimal ReceiptQty { get; set; }
            public Decimal ExtraQty { get; set; }
            public string Remark { get; set; }

            public string UOMCode { get; set; }
            public int Uom_PK;

            public int Uom_PK1
            {
                get { return getUOM_Pk(UOMCode); }
                set { Uom_PK = value; }
            }
            public int getUOM_Pk(String UOM)
            {
                int uompk = 0;
                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                {
                    var q = (from um in enty.UOMMasters
                             where um.UomCode.Trim() == UOM

                             select um.Uom_PK).FirstOrDefault();

                    uompk = int.Parse(q.ToString());
                }

                return uompk;
            }


            public Decimal Curate { get; set; }
           








        }

      public class RecieptMasterData
        {
            public int Reciept_Pk { get; set; }
            public string RecieptNum { get; set; }
            public int RecptLocation_PK { get; set; }
            public string ContainerNum { get; set; }
            public string BOENum { get; set; }
            public string Remark { get; set; }
            public DateTime  InhouseDate { get; set; }
            public DateTime Deliverydate { get; set; }
            public int Supplier_PK { get; set; }
            public string AddedBy { get; set; }
            public DateTime AddedDate { get; set; }
            public string IsCompleted { get; set; }



            public String  InsertReciptMstr(RecieptMasterData reptmstr)
            {

                String num = "";
                using (ArtEntitiesnew entry = new ArtEntitiesnew())
                {   
                    RecieptMaster reptdb = new RecieptMaster();
                   
                    reptdb.RecieptNum = reptmstr.RecieptNum;
                    reptdb.RecptLocation_PK = reptmstr.RecptLocation_PK;
                    reptdb.ContainerNum = reptmstr.ContainerNum;
                    reptdb.BOENum = reptmstr.BOENum;
                    reptdb.Remark = reptmstr.Remark;
                    reptdb.InhouseDate = DateTime.Now;
                    reptdb.Deliverydate = DateTime.Now;
                    reptdb.Supplier_PK = reptmstr.Supplier_PK;
                    reptdb.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    reptdb.AddedDate = DateTime.Now;
                    reptdb.IsCompleted = reptmstr.IsCompleted;


                    entry.RecieptMasters.Add(reptdb);

                    entry.SaveChanges();

                 //   reptdb.RecieptNum = "R" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + reptdb.Reciept_Pk.ToString().PadLeft(6, '0');

                    reptdb.RecieptNum = CodeGenerator.GetUniqueCode("RCPT", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(reptdb.Reciept_Pk.ToString()));
                    num = reptdb.RecieptNum;

                    entry.SaveChanges();


                }


                return num;

            }


        }




    
}