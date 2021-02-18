using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INF_SUSK_Project_Entities
{
    public class TBLISEMRI_MKA
    {
        [Key]
        public string ISEMRINO { get; set; }
        public DateTime TARIH { get; set; }
        public string STOK_KODU { get; set; }
        public decimal MIKTAR { get; set; }
        public decimal ACIKISEMRI { get; set; }
        public decimal ALTMAMULIHTIYAC { get; set; }
        public string REFISEMRINO { get; set; }
        public string ANAMAMUL { get; set; }
        public string SIPARIS_NO { get; set; }
        public string ACIKLAMA { get; set; }
        public DateTime TESLIM_TARIHI { get; set; }
    }
}
