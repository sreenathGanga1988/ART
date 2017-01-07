using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser
{
    public partial class AtcStylemaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonAtc_Click(object sender, EventArgs e)
        {
            int atcid = int.Parse(cmb_atc.SelectedValue.ToString());

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var Ourstylequery = from ourstyle in enty.AtcDetails
                                    where ourstyle.AtcId == atcid
                                    select new
                                    {
                                        name = ourstyle.OurStyle,
                                        pk = ourstyle.OurStyleID
                                    };
                cmb_ourstyle.DataSource = Ourstylequery.ToList();
                cmb_ourstyle.DataBind();
            }
        }
    }
}