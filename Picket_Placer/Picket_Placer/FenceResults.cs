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

        public FenceResults(int numBoards, string result, double exact)
        {
            boardNumber = numBoards;
            roundedResult = result;
            exactResult = exact;
        }
        public int boardNumber { get; set; }

        public string roundedResult { get; set; }

        public double exactResult { get; set; }

    }
}
