using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.UserBLL
{
    public class UserData
    {

        public int UserID { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }






        public void insertUserdata()
        {
            using (DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {
                UserMaster um = new UserMaster();
                um.UserName = this.UserName;
                um.Password = this.Password;
                //um.UserLoc_PK = this.UserLoc_PK;
               
                enty.UserMasters.Add(um);
                enty.SaveChanges();
            }

        }

        /// <summary>
        /// check whether mrn  is made  for a spodetpk
        /// </summary>
        /// <param name="skudetPK"></param>
        /// <returns></returns>
        public Boolean isUserPresent(String username)
        {
            Boolean ispresent = false;
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                if (!entty.UserMasters.Any(f => f.UserName.Trim () == username.Trim()))
                {
                    ispresent = false;
                }
                else
                {
                    ispresent = true;
                }

            }
            return ispresent;
        }



        public void UpdatePassword()
        {
            using (DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {
                
                
                var q = from user in enty.UserMasters
                           where user.User_PK == this.UserID
                       
                           select user;


                foreach (var element in q)
                {
                    element.Password = this.Password;
                }

                enty.SaveChanges();
            }
        }



        public Boolean IsUserAuthenicated()
        {
            Boolean isauthenicated = false;

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                try
                {
                    var q = from usr in enty.UserMasters
                            join loc in enty.LocationMasters
                            on usr.UserLoc_PK equals loc.Location_PK
                            where usr.UserName.Trim() == this.UserName && usr.Password == this.Password
                            select new
                            {

                                usr.UserName,
                                usr.UserLoc_PK,
                                loc.LocationPrefix

                            };

                    foreach (var user in q)
                    {
                      
                        isauthenicated = true;
                    }
                }
                catch (Exception ex)
                {


                }


            }


            return isauthenicated;
        }


        public Boolean IsUserAuthenicated(String username,String Password)
        {
            Boolean isauthenicated = false;

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                try
                {
                    var q = from usr in enty.UserMasters
                            join loc in enty.LocationMasters
                            on usr.UserLoc_PK equals loc.Location_PK
                            where usr.UserName.Trim() == username && usr.Password == Password
                            select new { 
                            
                                usr.UserName,usr.UserLoc_PK,loc.LocationPrefix,usr.User_PK

                            };

                    foreach (var user in q)
                    {
                        HttpContext.Current.Session["Username"] = user.UserName;
                        HttpContext.Current.Session["User_PK"] = user.User_PK;
                        HttpContext.Current.Session["UserLoc_pk"] = user.UserLoc_PK;
                        HttpContext.Current.Session["lOC_Code"] = user.LocationPrefix;
                        isauthenicated = true;

                        var q1 = from usssr in enty.UserMasters
                                 where usssr.User_PK == user.User_PK
                                 select usssr;

                        foreach (var userk in q1)
                        {
                            userk.LastLogin = DateTime.Now;
                        }

                       

                        }
                    enty.SaveChanges();
                }
                catch (Exception ex)
                {
                    
                    
                }


            }


            return isauthenicated;
        }





        /// <summary>
        /// check whether mrn  is made  for a spodetpk
        /// </summary>
        /// <param name="skudetPK"></param>
        /// <returns></returns>
        public Boolean isUserAdmin(int userpk )
        {
            Boolean ispresent = false;
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                if (!entty.ApprovalTables.Any(f => f.User_PK == userpk))
                {
                    ispresent = false;
                }
                else
                {
                    ispresent = true;
                }

            }
            return ispresent;
        }







    }
}