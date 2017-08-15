using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtWebApp.DataModels
{
    public class PartialClasses
    {
    }

    [MetadataType(typeof(UserMasterMD))]
    public partial class UserMaster
    {
        public class UserMasterMD

        {
            [Display(Name = "ID")]
            
            public decimal User_PK { get; set; }
            [Display(Name = "User Name")]
            [Required(ErrorMessage = "Please enter the User Nam")]
            public string UserName { get; set; }
            [Required]
            [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [RegularExpression(@"^ ((?=.*[a - z])(?=.*[A - Z])(?=.*\d)).+$")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Confirm Password is required")]
            [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
            [DataType(DataType.Password)]
            [Compare("Password")]
            [Display(Name = "Password")]
            public string PssWrd { get; set; }

            [Display(Name = " Location")]
            public Nullable<decimal> UserLoc_PK { get; set; }
            public string PassCode { get; set; }
            public string IsAdmin { get; set; }
            [Display(Name = " Last Logged In")]
            public Nullable<System.DateTime> LastLogin { get; set; }
            [Display(Name = " User Profile")]
            public Nullable<decimal> UserProfile_Pk { get; set; }

            [Display(Name = " Virtual Department")]
            public Nullable<decimal> Department_PK { get; set; }
            [Display(Name = " Email")]
            public string EmailId { get; set; }
            [Display(Name = " Mobile")]
            public string MobileNum { get; set; }
            [Display(Name = " Last Password Changed on")]
            public Nullable<System.DateTime> LastPassWordDate { get; set; }

            [Display(Name = "Is Active")]
            public bool IsActiveUser { get; set; }
            [Display(Name = "Is Deleted")]
            public bool IsDeleteduser { get; set; }
            [Display(Name = "Is Locked")]
            public bool IsLockedUser { get; set; }

            [Display(Name = "Is Verified")]
            public bool IsVerified { get; set; }
            [Display(Name = "Is IT Verified")]
            public bool ITVerified { get; set; }

            [Display(Name = "Allowed outside Premise")]
            public bool AllowOutSideAction { get; set; }
        }


       

    
    }

    [MetadataType(typeof(DepartmentMasterMD))]
    public partial class DepartmentMaster
    {
        public class DepartmentMasterMD
        {
        public decimal Deapartment_PK { get; set; }
            [Display(Name = " Virtual Department")]
            public string DepartmentName { get; set; }
        }

       

       
    }



    [MetadataType(typeof(LocationMasterMD))]
    public partial class LocationMaster
    {
        public class LocationMasterMD
        {
            public decimal Location_PK { get; set; }
            [Display(Name = " Location")]
            public string LocationName { get; set; }
        }

      
       

       
    }

    [MetadataType(typeof(UserProfileMaster))]
    public partial class UserProfileMaster
    {
        public class UserProfileMasterMD
        {
            [Display(Name = "User Profile Name")]
            public string UserProfileName { get; set; }
        }

       

        
    }


}