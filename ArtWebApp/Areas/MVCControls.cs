using System;
using System.Collections.Generic;
using System.Data;

using System.Web.Mvc;





namespace ArtWebApp.Areas
{
    public static class MVCControls
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Valuecolumnname">Column which should used as value</param>
        /// <param name="textcolumnname">Column which should used as text</param>
        /// <param name="dt">Actual datatable</param>
        /// <param name="OptionalInstialText"> optional first item</param>
        /// <returns></returns>
        public static SelectList DataTabletoSelectList(String Valuecolumnname, String textcolumnname, DataTable dt, string OptionalInstialText = "")
        {
            List<SelectListItem> list = new List<SelectListItem>();

            if (OptionalInstialText != "")
            {
                list.Add(new SelectListItem { Text = OptionalInstialText, Value = "0" });
            }
            foreach (DataRow row in dt.Rows)
            {

                list.Add(new SelectListItem { Text = Convert.ToString(row[textcolumnname]), Value = Convert.ToString(row[Valuecolumnname]) });

            }
            return new SelectList(list, "Value", "Text");
           
        }



        //public static JsonResult ConvertListtoJson(List<SelectListItem> list)
        //{
           

            

        //    return jsd;
        //}

    }
}