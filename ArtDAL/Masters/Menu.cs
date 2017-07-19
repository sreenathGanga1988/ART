
using ArtDAL.Enty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtDAL.Masters
{
    public static class MenuDal
    {


        /// <summary>
        /// get the submenu of an item
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static IEnumerable<SubMenuMaster>  GetsubMenulist(Decimal? parentid)
        {
            using (Enty.NewDBEnty enty = new NewDBEnty())
            {

                if (parentid == 0)
                {
                    parentid = null;
                }
                 var q = enty.SubMenuMasters.Where(u => u.ParentID == parentid).ToList();

                return q;
            }
        }

    

    }
}
