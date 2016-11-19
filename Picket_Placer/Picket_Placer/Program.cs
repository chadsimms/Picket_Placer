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
            double accuracy = 0.0;
            string frac;
            Fence myFence = new Fence();        //new fence object 

            //get wall length of the fence that pickets will go on
            Console.Write("Enter the Length of the Space to place pickets (in inches as 2-1/4 or 2.25): ");
            myFence.WallLength = GetInput(Console.ReadLine());

            //get picket width from user 
            Console.Write("Enter the Width of the pickets (in inches as 1-1/8 or 1.125): ");
            myFence.PicketWidth = GetInput(Console.ReadLine());

            //get max space from the user between pickets
            Console.Write("Enter the Max Spacing between pickets (in inches as 4-1/2 or 4.5): ");
            myFence.MaxSpace = GetInput(Console.ReadLine());

            //get answers rounded to what place?
            Console.Write("Enter what you want answers rounded (in inches as 1/16 or 1/4): ");

            //store input as string to display for testing                                                                      //TEST
            //bypass to accuracy if not displayed                                                                               //TEST
            frac = Console.ReadLine();

            //combine above statement with this when working properly                                                           //TEST
            accuracy = GetInput(frac);

            //output info to console for verification
            Console.WriteLine("\nFence Length rounded to the nearest " + frac + ": \t" + 
                Calculations.DecimalToFraction(myFence.WallLength, accuracy) + '"');                                            //TEST
            Console.WriteLine("Picket Width rounded to the nearest " + frac + ": \t" + 
                Calculations.DecimalToFraction(myFence.PicketWidth, accuracy) + '"');                                           //TEST
            Console.WriteLine("Max Spacing rounded to the nearest  " + frac + ": \t" + 
                Calculations.DecimalToFraction(myFence.MaxSpace, accuracy) + '"');                                              //TEST

            List<FenceResults> results = FenceMath.CalcResults(myFence, accuracy);

            if(results.Count == 0)
            {
                Console.WriteLine("No Pickets will fit in desired space!  Make sure the right measurements were entered and try again");
            }
            else
            {
                foreach (FenceResults r in results)
                {
                    Console.Write("\nLeft edge of picket " + r.boardNumber + " is at: \t" + r.roundedResult + '"');
                    Console.Write("     \t | Exact measurement is: " + r.exactResult);                                          //TEST
                }
            }

            Console.ReadLine();

        }

        /**********************************************************************************************
         *  Function:       GetInput()
         *  Description:    Converts the input if fractions to decimal equivalent for calculating also
         *                  removes any negative numbers if they're entered
         *  Input:          userInput - the input received from the user in whole, decimal, or fraction
         *                  entered as (2-1/4 NOT 2 1/4)
         *  Output:         value - the decimal equivalent of the userInput
         **********************************************************************************************/
        public static double GetInput(string userInput)
        {
            string input = userInput;
            string[] a;             //[0] = whole number, [1] = fraction
            string[] fraction;      //[0] = Numerator, [1] = Denominator
            double value = 0.0;

            //remove any negative information that was input
            if (input.StartsWith("-"))
            {
                input = input.Remove(0, 1);
            }

            //if it contains a hyphen
            if (input.Contains('-') || input.Contains('/'))
            {
                if (input.Contains('-'))
                {
                    //then split the string into whole number and fraction 
                    a = input.Split('-');

                    //split the fraction into numerator and denominator
                    fraction = a[1].Split('/');

                    // numerator / denominator to get decimal value
                    value = Convert.ToDouble(Convert.ToDouble(fraction[0]) / Convert.ToDouble(fraction[1]));

                    //add decimal value back to whole number
                    value = value + Convert.ToDouble(a[0]);
                    //Console.WriteLine("Decimal Value: \t" + value);                                                       //TEST
                }
                else
                {
                    fraction = input.Split('/');

                    // numerator / denominator to get decimal value
                    value = Convert.ToDouble(Convert.ToDouble(fraction[0]) / Convert.ToDouble(fraction[1]));
                }
            }

            //if the value entered was a whole number, return it
            else if (Convert.ToDouble(input) - Math.Floor(Convert.ToDouble(input)) == 0)
            {
                value = Convert.ToDouble(input);
                //Console.WriteLine("Whole Number: \t" + value + '"');                                                  //TEST
            }

            //if the value entered was a decimal, then return it as a decimal
            else if (input.Contains('.'))
            {
                value = Convert.ToDouble(input);
                //Console.WriteLine("Decimal Value: \t" + value + '"');                                                 //TEST
            }

            //return the absolute value of the calculated decimal
            return value;
        }

    }
}
