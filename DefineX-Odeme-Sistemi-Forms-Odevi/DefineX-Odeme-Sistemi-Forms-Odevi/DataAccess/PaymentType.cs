using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefineX_Odeme_Sistemi_Forms_Odevi.Attributes;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.DataAccess
{
    public class PaymentType
    {
       
        public int Id { get; set; }

        [Column("Display_member")]
        public string DisplayMember { get; set; }

        [Column("Display_value")]
        public string DisplayValue { get; set; }
    }
}
