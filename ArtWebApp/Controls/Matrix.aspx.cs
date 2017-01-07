using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArtWebApp.DataModels;
using System.Linq;
namespace ArtWebApp.Controls
{
    public partial class Matrix : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         DataTable dt= createdatatable(30008);


         foreach (DataColumn col in dt.Columns)
         {
             //Declare the bound field and allocate memory for the bound field.
             TemplateField bfield = new TemplateField();

             //Initalize the DataField value.
             bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.ColumnName);

             //Initialize the HeaderText field value.
             bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName);

             //Add the newly created bound field to the GridView.
             GrdDynamic.Columns.Add(bfield);
         }

         GrdDynamic.DataSource = dt;
         GrdDynamic.DataBind();
        }

        public DataTable createdatatable(int ourstyleid)
        {
            DataTable dt = new DataTable();
            int i=0;
            dt.Columns.Add("Color", typeof(String));
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
                    dt.Columns.Add(sizedet.SizeName.Trim (), typeof(String));
                }





                var Colordetails = (from color in enty.StyleColors
                                   where color.OurStyleID==ourstyleid
                                   select new
                                            {
                                                color.GarmentColor
                                            }).Distinct();

                foreach (var colordet in Colordetails)
                {
                    dt.Rows.Add();
                    dt.Rows[i]["Color"] = colordet.GarmentColor;
                    i++;
                   

                }


                if (dt != null)
                {

                    var popackdetail = from popackdetails in enty.POPackDetails
                                       where popackdetails.OurStyleID == ourstyleid
                                       select new
                                       {
                                           popackdetails.ColorName,
                                           popackdetails.SizeName,
                                           popackdetails.PoQty
                                       };

                    if (dt.Rows.Count >= 1 && (dt.Rows.Count > 1))
                    {


                        for (int rowcount = 0; rowcount < dt.Rows.Count; rowcount++)
                        {
                            String Colorname = dt.Rows[rowcount]["Color"].ToString().Trim();
                            for (int coloumncount = 1; coloumncount < dt.Columns.Count; coloumncount++)
                            {
                                String Sizename = dt.Rows[rowcount][coloumncount].ToString().Trim();

                                var qp = (from pd in popackdetail
                                          where pd.ColorName == Colorname && pd.SizeName == Sizename
                                          select new { pd.PoQty }).ToList();
                                var Qty = qp.Select(c => c.PoQty).Sum();
                                dt.Rows[rowcount][coloumncount] = Qty;

                            }

                        }

                    }

                }



            }


           

            return dt;
        }
    }
}