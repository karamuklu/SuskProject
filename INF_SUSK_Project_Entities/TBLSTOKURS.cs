using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INF_SUSK_Project_Entities
{
	public class TBLSTOKURS
	{
		[Key]
		public string URETSON_FISNO { get; set; }
        public decimal URETSON_MIKTAR { get; set; }
    }
}
