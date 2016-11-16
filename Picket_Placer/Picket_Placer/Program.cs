using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picket_Placer
{
    class Program
    {
        /**********************************************************************************************
         *  Function:       Main()
         *  Description:    
         *  Input:          
         *  Output:         
         **********************************************************************************************/
        static void Main(string[] args)
        {
            int count = 1;
            Fence myFence = new Fence();        //new fence object 

            //get wall length of the fence that pickets will go on
            Console.Write("Enter the Length of the Space to place pickets (in inches as 2-1/4 or 2.25): ");
            myFence.WallLength = FenceMath.GetInput(Console.ReadLine());

            //get picket width from user 
            Console.Write("Enter the Width of the pickets (in inches as 1-1/8 or 1.125): ");
            myFence.PicketWidth = FenceMath.GetInput(Console.ReadLine());

            //get max space from the user between pickets
            Console.Write("Enter the Max Spacing between pickets (in inches as 4-1/2 or 4.5): ");
            myFence.MaxSpace = FenceMath.GetInput(Console.ReadLine());

            //output info to console for verification
            Console.Write("\nFence Length: \t" + FenceMath.DecimalToFraction(myFence.WallLength) + '"');                       //TEST
            Console.Write("\nPicket Width: \t" + FenceMath.DecimalToFraction(myFence.PicketWidth) + '"');                      //TEST
            Console.Write("\nMax Spacing: \t" + FenceMath.DecimalToFraction(myFence.MaxSpace) + '"');                      //TEST

            List<FenceResults> results = FenceMath.CalcResults(myFence);

            if(results.Count == 0)
            {
                Console.WriteLine("No Pickets will fit in desired space!  Make sure the right measurements were entered and try again");
            }
            foreach(FenceResults r in results)
            {
                Console.Write("\nLeft edge of picket " + count + " is at: \t" + r.roundedResult + '"');
                Console.Write("     \t | Exact measurement is: " + r.exactResult);                                                          //TEST
                count++;
            }

            Console.ReadLine();

        } 
    }
}
