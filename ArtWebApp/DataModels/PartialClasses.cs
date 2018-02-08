using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
            [StringLength(18, ErrorMessage = "Minimum 8 characters atleast 1 Alphabet, 1 Number and 1 Special Character.")]
            [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Confirm Password is required")]
            [StringLength(255, ErrorMessage = "Confirm Passwrod and Password should match", MinimumLength = 5)]
            [DataType(DataType.Password)]
            [Compare("Password")]
            [Display(Name = "Confirm Password")]
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

    [MetadataType(typeof(FreightRequestMasterMD))]
    public partial class FreightRequestMaster
    {
        [NotMapped]
        public string AllowedValue { get; set; }
        [NotMapped]
        public string UsedValue { get; set; }
        [NotMapped]
        public string BalanceValue { get; set; }
        public class FreightRequestMasterMD
        {
            public decimal FreightRequestID { get; set; }
            [Display(Name = " FreightRequestNum")]
            public string FreightRequestNum { get; set; }

            [Display(Name = " AddedBy")]
            public string AddedBy { get; set; }
            [Display(Name = " AddedDate")]
            public Nullable<System.DateTime> AddedDate { get; set; }
            [Display(Name = " From Party")]
            public string FromParty { get; set; }
            [Display(Name = " To Party")]
            public string ToParty { get; set; }
            [Display(Name = " Shipper")]
            public string Shipper { get; set; }
            [Display(Name = " Weight")]
            public string Weight { get; set; }
            [Display(Name = " Content of Package")]
            public string ContentofPackage { get; set; }
            [Display(Name = " Debit To")]
            public string DebitTo { get; set; }
            [Display(Name = "Reason")]
            public string Reason { get; set; }
            [Display(Name = " Merchandiser")]
            public string Merchandiser { get; set; }
            [Display(Name = " Forwarder Details")]
            public string ForwarderDetails { get; set; }
            [Display(Name = " Approximate Charges")]
            public string ApproximateCharges { get; set; }
            [Display(Name = " Remark")]
            public string Remark { get; set; }
            public string ApprovedBy { get; set; }
            public Nullable<System.DateTime> ApprovedDate { get; set; }
            public string IsApproved { get; set; }
            public string IsPosted { get; set; }

        }
    }




    [MetadataType(typeof(FreightRequestDetailMD))]
    public partial class FreightChargeDetail
    {
        [NotMapped]
        public string AllowedValue { get; set; }
        [NotMapped]
        public string UsedValue { get; set; }
        [NotMapped]
        public string BalanceValue { get; set; }
    }

    [MetadataType(typeof(LabChargeDetailMD))]
    public partial class LabChargeDetail
    {
        [NotMapped]
        public string AllowedValue { get; set; }
        [NotMapped]
        public string UsedValue { get; set; }
        [NotMapped]
        public string BalanceValue { get; set; }
    }
    public class FreightRequestDetailMD
    {


        // and other properties you want...
    }
    public class LabChargeDetailMD
    {


        // and other properties you want...
    }
}