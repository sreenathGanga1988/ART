using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.ViewModel.Master
{
    public class Itemmenu
    {
        public Decimal itemmenupk { get; set; }

        public SubMenusiewModal SubMenusiewModal
        {
            get
            {
                return new SubMenusiewModal(this.itemmenupk);
            }

            set
            {
                subMenusiewModal = value;
            }
        }

        private SubMenusiewModal subMenusiewModal;




    }
}