using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INF_SUSK_Project_Entities
{
    public class SUSK_LISTESI
    {
        public DateTime TARIH { get; set; }
        public string ISEMRINO { get; set; }
        public string STOK_KODU { get; set; }
        public decimal MIKTAR { get; set; }
        public int SUSKDEPOGIRIS { get; set; }
        public int SUSKDEPOCIKIS { get; set; }
        public string SERI_NO { get; set; }
        public string REFISEMRINO { get; set; }
        public string ACIKLAMA { get; set; }
    }
}

