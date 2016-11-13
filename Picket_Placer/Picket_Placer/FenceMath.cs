using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picket_Placer
{
    class FenceMath
    {
        const double REL_ERROR = 0.001;

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
            if(input.Contains('-') || input.Contains('/'))
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

        /**********************************************************************************************
         *  Function:       RoundToSixteenth()
         *  Description:    Rounds a number to the nearest 1/16th (0.0625)
         *  Input:          value - the decimal value to be rounded to the nearest 1/16th 
         *  Output:         ans - value rounded to the nearest 1/16th
         **********************************************************************************************/
        public static double RoundToSixteenth(double value)
        {
            double ans = 0.0;
            double accuracy = 0.0625 * 10000;           // 1/16 accuracy

            //used to test which value is closer to the original
            double down = 0.0;                          // rounded down
            double up = 0.0;                            // rounded up

            //Round to nearest 1/16th of an inch
            ans = value - Math.Floor(value);

            ans = ans * 10000;

            down = Math.Floor(ans / accuracy) * accuracy;
            up = Math.Ceiling(ans / accuracy) * accuracy;

            //determine if closer to round up or down to the nearest 1/16th
            if (Math.Abs(down - ans) < (Math.Abs(up - ans)))
            {
                ans = down;
            }
            else
            {
                ans = up;
            }

            ans = ans / 10000;
            return (Math.Floor(value) + ans);
        }

        /**********************************************************************************************
        *  Function:       DecimalToFraction()
        *  Description:    Converts a Decimal number to a fraction with REL_ERROR (const defined) 
        *                  accuracy
        *  Input:          value - to be converted to a fraction rounded to 1/16th
        *  Calls To:       RoundToSixteenth(), RealToFraction()
        **********************************************************************************************/
        public static void DecimalToFraction(double value)
        {
            double temp = RoundToSixteenth(value);
            double wholeNum = Math.Floor(temp);
            double fraction = temp - Math.Floor(temp);   //round the spacing value to 1/16

            if (fraction == 0)
            {
                Console.Write(wholeNum);
            }
            else
            {
                Fraction num = RealToFraction(fraction, REL_ERROR);

                if(wholeNum == 0)
                {
                    Console.Write(num.N + "/" + num.D);
                }
                else
                {
                    Console.Write(wholeNum + "-" + num.N + "/" + num.D);
                }
                
            }
        }

        /**********************************************************************************************
         *  Function:       RealToFraction()
         *  Description:    Converts a decimal between 0 and 1 to a fraction equivalent with REL_ERROR
         *                  accuracy or displays error if not found.
         *  Input:          value - the decimal value to be converted to fraction
         *                  error - between 0 and 1, the max relative error calculation
         *  Output:         Fraction object - with numerator(N) and denominator(D).
         **********************************************************************************************/
        public static Fraction RealToFraction(double value, double error)
        {

            if (error <= 0.0 || error >= 1.0)
            {
                throw new ArgumentOutOfRangeException("error", "Must be between 0 and 1 (exclusive).");
            }

            int sign = Math.Sign(value);

            if (sign == -1)
            {
                value = Math.Abs(value);
            }

            if (sign != 0)
            {
                // error is the maximum relative error; convert to absolute
                error *= value;
            }

            int n = (int)Math.Floor(value);
            value -= n;

            if (value < error)
            {
                return new Fraction(sign * n, 1);
            }

            if (1 - error < value)
            {
                return new Fraction(sign * (n + 1), 1);
            }

            // The lower fraction is 0/1
            int lower_n = 0;
            int lower_d = 1;

            // The upper fraction is 1/1
            int upper_n = 1;
            int upper_d = 1;

            while (true)
            {
                // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
                int middle_n = lower_n + upper_n;
                int middle_d = lower_d + upper_d;

                if (middle_d * (value + error) < middle_n)
                {
                    // real + error < middle : middle is our new upper
                    upper_n = middle_n;
                    upper_d = middle_d;
                }
                else if (middle_n < (value - error) * middle_d)
                {
                    // middle < real - error : middle is our new lower
                    lower_n = middle_n;
                    lower_d = middle_d;
                }
                else
                {
                    // Middle is our best fraction
                    return new Fraction((n * middle_d + middle_n) * sign, middle_d);
                }
            }
        }
    }
}
