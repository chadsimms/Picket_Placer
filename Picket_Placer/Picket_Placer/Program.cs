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
            double spacing = 0.0,               //calculated spacing between pickets
                   sectionWidth = 0.0;          //width of one picket + calculated spacing

            int numPickets = 0;                 //number of pickets that fit in wall length

            Fence myFence = new Fence();        //new fence object 

            //get wall length of the fence that pickets will go on
            Console.Write("Enter the Length of the Space to place pickets (in inches): ");
            myFence.WallLength = FenceMath.GetInput(Console.ReadLine());

            //get picket width from user 
            Console.Write("Enter the Width of the pickets (in inches): ");
            myFence.PicketWidth = FenceMath.GetInput(Console.ReadLine());

            //get max space from the user between pickets
            Console.Write("Enter the Max Spacing between pickets (in inches): ");
            myFence.MaxSpace = FenceMath.GetInput(Console.ReadLine());

            Console.Write("\nFence Length: \t"); FenceMath.DecimalToFraction(myFence.WallLength); Console.Write('"');
            Console.Write("\nPicket Width: \t"); FenceMath.DecimalToFraction(myFence.PicketWidth); Console.Write('"');
            Console.Write("\nMax Spacing: \t"); FenceMath.DecimalToFraction(myFence.MaxSpace); Console.WriteLine('"');

            //calculate number of pickets
            numPickets = FenceMath.GetNumPickets(myFence);

            if(numPickets <= 0)
            {
                Console.WriteLine("No Pickets will fit in desired space!  Make sure the right measurements were entered and try again");

            }
            else
            {
                //calculate spacing rounded to double accuracy
                spacing = FenceMath.GetSpacing(myFence, numPickets);

                //calculate sectionwidth (spacing + picket width)
                sectionWidth = myFence.PicketWidth + spacing;

                Console.Write("\nAll measurements are in inches and starting at the left edge of the workspace rounded to the nearest 1/16th\n");

                //  display measurement of each picket from the left side of workspace to left side of 
                //  each picket
                for (int i = 1; i <= numPickets; i++)
                {
                    Console.Write("\nLeft edge of picket " + i + " is at: \t");
                    FenceMath.DecimalToFraction(spacing); Console.Write('"');
                    Console.Write("  \t | Exact measurement is: " + spacing);                                                 //TEST
                    spacing = spacing + sectionWidth;
                }

                Console.WriteLine("\n");
            }
            
        } 
    }
}
