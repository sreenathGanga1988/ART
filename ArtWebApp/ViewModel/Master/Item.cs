using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtWebApp.ViewModel.Master
{
    public class Itemmenu
    {
        public Decimal itemmenupk { get; set; }
        public int profilepk { get; set; }
        public SubMenusiewModal SubMenusiewModal
        {
            get
            {
                return new SubMenusiewModal(this.itemmenupk,this.profilepk);
            }

            set
            {
                subMenusiewModal = value;
            }
        }

        private SubMenusiewModal subMenusiewModal;




    }

    public class ItemGroupMaster
    {

        [Key]
        public decimal ItemGroupID { get; set; }
        [Required]
        public string ItemGroupName { get; set; }
        [Required]
        [Display( Name ="Description")]
        public string ItemGroupDescription { get; set; }

      
    }
}