using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollEntryApp
{
    public static class ExtentionMethods
    {
        public static void ReleaseObject(this object ob)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ob);
            ob = null;
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
