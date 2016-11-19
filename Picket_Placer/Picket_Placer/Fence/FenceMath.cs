using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picket_Placer
{
    class FenceMath
    {
        /**********************************************************************************************
         *  Function:       CalcResults()
         *  Description:    Calculates the results given a Fence object and returns a List of FenceResults
         *  Input:          myFence - the Fence object to calculate results from
         *  Output:         results - the List of FenceResults returned with calculated spacing and 
         *                              measurements
         **********************************************************************************************/
         public static List<FenceResults> CalcResults(Fence myFence, double roundedTo)
        {
            int numPickets = 0;
            double spacing = 0.0;
            double sectionWidth = 0.0;

            //make a list to return
            List<FenceResults> results = new List<FenceResults>();

            //calculate the number of pickets
            numPickets = FenceMath.GetNumPickets(myFence);

            //if no pickets, return no pickets fit
            if(numPickets > 0)
            {
                //calculate spacing rounded to double accuracy
                spacing = FenceMath.GetSpacing(myFence, numPickets);

                //calculate section width (spacing + picket width)
                sectionWidth = myFence.PicketWidth + spacing;

                //Console.Write("\nAll measurements are in inches and starting at the left edge of the workspace rounded to the nearest 1/16th\n");

                //put fence object into results list to return
                for (int i = 1; i <= numPickets; i++)
                {
                    FenceResults fence = new FenceResults(i,Calculations.DecimalToFraction(spacing, roundedTo).ToString(), spacing);
                    results.Add(fence);
                    spacing = spacing + sectionWidth;
                }
            }

            return results;
        }

        /**********************************************************************************************
         *  Function:       GetNumPickets()
         *  Description:    Calculates the number of pickets that are required to ensure the spacing
         *                  between pickets does not exceed the user input fence.MaxSpace by calculating
         *                  a double value and rounding upwards to reduce the spacing.
         *  Input:          fence - Fence object to be used to calculate the number of pickets
         *  Output:         ans - the rounded number of pickets used in calculating space between 
         *                  pickets
         **********************************************************************************************/
        public static int GetNumPickets(Fence fence)
        {
            int ans = 0;
            double temp = 0.0;

            //avoid dividing by 0
            if(fence.PicketWidth == 0 || fence.MaxSpace == 0)
            {
                ans = 0;
            }

            //calculate the picket amount based on a max length
            else
            {
                temp = (fence.WallLength - fence.MaxSpace) / (fence.MaxSpace + fence.PicketWidth);
                ans = Convert.ToInt32(Math.Ceiling(temp));
            }

            return ans;
        }

        /**********************************************************************************************
         *  Function:       GetSpacing()
         *  Description:    Calculates the spacing between numTimes pickets over the fence WallLength
         *                  to ensure that the spacing is equal at every gap.
         *  Input:          fence - Fence object to be used to calculate the spacing
         *                  numTimes - number of pickets to be used in calculating space
         *  Output:         value - the spacing between each picket rounded to size of double datatype
         **********************************************************************************************/
        public static double GetSpacing(Fence fence, int numTimes)
        {
            double value = 0.0;

            value = (fence.WallLength - (fence.PicketWidth * numTimes)) / (numTimes + 1);
            Console.WriteLine("\n" + numTimes + " pickets will evenly fit with a space of " + value + '"');

            return value;
        }
    }
}
