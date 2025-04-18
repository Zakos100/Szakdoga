using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Scheduler
{
    public class Operation
    {
        public int TaskID { get; set; }
        public int MachineIndex { get; set; } // gép vagy műveleti lépés
        public int ProcessingTime { get; set; }
    }
}
