using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picket_Placer
{
    class FenceResults
    {
        public FenceResults()
        {

        }

        public FenceResults(string result, double exact)
        {
            roundedResult = result;
            exactResult = exact;
        }

        public string roundedResult { get; set; }

        public double exactResult { get; set; }

    }
}
