using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollEntryApp
{
    public class JVObject
    {
        public DateTime PayrollDate { get; set; }
        public string GLAccount { get; set; }
        public string CostCenter { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}
