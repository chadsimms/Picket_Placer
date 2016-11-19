using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picket_Placer
{
    class Calculations
    {
        const double REL_ERROR = 0.00001;

        /**********************************************************************************************
         *  Function:       getAccuracy()
         *  Description:    Takes a user input value such as 1/4 in decimal form (0.25) and calculates 
         *                  what multiple of 10 is required to make the decimal value a whole number.  
         *                  This is used to later round the calculated decimal value to the user defined
         *                  amount.
         *  Input:          dec - the decimal value equivalent to the user defined rounding accuracy
         *  Output:         temp[] - an array of 2 doubles, 
         *                          temp[0] is the multiple of 10 required to get the decimal to a whole number
         *                          temp[1] is the original decimal input by the user
         **********************************************************************************************/
        public static double[] getAccuracy(double dec)
        {
            //temp[0] is the multiplier
            //temp[1] is the actual decimal
            double[] temp = { 1.0, dec };

            //figure out what multiple of 10 to get the decimal to a whole number
            while(Math.Floor(temp[0] * dec) != (temp[0] * dec))
            {
                temp[0] *= 10;
            }

            return temp;
        }

        /**********************************************************************************************
         *  Function:       RoundToValue()
         *  Description:    Rounds a number to the nearest user defined fraction amount.
         *                  Example:
         *                      The user inputs 1/4, which is converted to 0.25 and passed in here as 
         *                      the rounded value.  That value is passed to getAccuracy to find the multiple
         *                      of 10 to multiply by for the accuracy to be a whole number amount.
         *  Input:          value - the decimal value to be rounded to the nearest 1/16th 
         *  Output:         ans - value rounded to the nearest 1/16th
         *  Calls to:       getAccuracy()
         **********************************************************************************************/
        public static double RoundToValue(double value, double rounded)
        {
            double ans = 0.0;

            //split[0] is the multiplier
            //split[1] is the actual decimal
            double[] split = getAccuracy(rounded);
            double accuracy = split[0] * split[1];

            //used to test which value is closer to the original
            double down = 0.0;                          // rounded down
            double up = 0.0;                            // rounded up

            //Round to nearest nth
            ans = value - Math.Floor(value);

            //multiply the decimal by the multiple of 10
            ans = ans * split[0];

            //calculate the next lowest and highest nth
            down = Math.Floor(ans / accuracy) * accuracy;
            up = Math.Ceiling(ans / accuracy) * accuracy;

            //determine if closer to round up or down to the nearest nth
            if (Math.Abs(down - ans) <= (Math.Abs(up - ans)))
            {
                ans = down;
            }
            else
            {
                ans = up;
            }

            //divide by the multiple of 10 from above to get the original decimal point
            ans = ans / split[0];

            return (Math.Floor(value) + ans);
        }

        /**********************************************************************************************
        *  Function:       DecimalToFraction()
        *  Description:    Converts a Decimal number to a fraction with REL_ERROR (const defined) 
        *                  accuracy
        *  Input:          value - to be converted to a fraction rounded to nth(rounded) places
        *                  rounded - the decimal value for the accuracy of rounded values
        *  Calls To:       RoundToValue(), RealToFraction()
        **********************************************************************************************/
        public static string DecimalToFraction(double value, double rounded)
        {
            double temp = RoundToValue(value, rounded);             //round the value to the nth
            double wholeNum = Math.Floor(temp);                     
            double fraction = temp - Math.Floor(temp);              //round the spacing value to nth
            string result = "";

            if (fraction == 0)
            {
                result = wholeNum.ToString();
            }
            else
            {
                Fraction num = RealToFraction(fraction, REL_ERROR);

                if (wholeNum == 0)
                {
                    result = num.N.ToString() + "/" + num.D.ToString();
                }
                else
                {
                    result = wholeNum.ToString() + "-" + num.N.ToString() + "/" + num.D.ToString();
                }
            }

            return result;
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
