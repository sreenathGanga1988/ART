
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
        public static IEnumerable<SubMenuMaster>  GetsubMenulist(Decimal? parentid ,int userprofilepk = 0)
        {
            using (Enty.NewDBEnty enty = new NewDBEnty())
            {
             
                if (parentid == 0)
                {
                    parentid = null;
                }

                var q = (from submenumstr in enty.SubMenuMasters
                        join userProfileRights in enty.UserProfileRights on submenumstr.Menu_PK equals userProfileRights.Menu_PK
                        where userProfileRights.UserProfile_Pk == userprofilepk && submenumstr.ParentID==parentid
                        select submenumstr).ToList();
               //  var q = enty.SubMenuMasters.Where(u => u.ParentID == parentid).ToList();

                return q;
            }
        }

    

    }
}
