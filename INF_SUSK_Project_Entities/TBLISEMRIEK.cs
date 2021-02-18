using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INF_SUSK_Project_Entities
{
    public class TBLISEMRIEK
    {
        [Key]
        public string ISEMRI { get; set; }
        public int? KT_TEKNIKSERVIS { get; set; }
    }
}
