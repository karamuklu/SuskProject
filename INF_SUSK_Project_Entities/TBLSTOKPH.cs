using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INF_SUSK_Project_Entities
{
    public class TBLSTOKPH
    {
        [Key]
        public string STOK_KODU { get; set; }
        public decimal TOP_GIRIS_MIK { get; set; }
        public decimal TOP_CIKIS_MIK { get; set; }

    }
}
