using ArtBL.Masters.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtBL.Masters.Item
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

