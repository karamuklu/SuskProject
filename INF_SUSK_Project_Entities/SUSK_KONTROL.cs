using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INF_SUSK_Project_Entities
{
    public class SUSK_KONTROL
    {
        [Key]
        public string ISEMRINO { get; set; }
        public string STOK_KODU { get; set; }
        public decimal ISEMRIMIKTAR { get; set; }
        public decimal SUSKMIKTAR { get; set; }
        public decimal BAKIYE { get; set; }
    }
}
