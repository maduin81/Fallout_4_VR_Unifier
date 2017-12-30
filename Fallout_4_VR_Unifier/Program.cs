using System;
using System.IO;
using System.Linq;

namespace Fallout_4_VR_Unifier
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            if (Environment.GetCommandLineArgs().Any(s => s.ToLower().Contains("/m")))
            {
                var merger = new Merger();
                merger.RunMerge();
            }
            else
            {
                var form = new Fallout4UnifierForm();
                form.ShowDialog();
            }
        }

        
    }
}
