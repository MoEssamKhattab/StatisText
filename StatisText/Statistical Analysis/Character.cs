using System;

namespace Statistical_Analysis
{
    internal class Character
    {
        public char Char { get; set; }
        public int occurrence_no { get; set; }
        //public int RV { get; set; }                     //No need for RV, PMF or CDF for the best use of memory
        //public double PMF { get; set; }
        //public double CDF { get; set; }


        static public int convertCharToRV(char c)         //Converts Character to its corresponding RV mapping
        {
            if (char.IsNumber(c))           //It's a number
            {
                return (int)(char.GetNumericValue(c));    //**** Casting char to double (first), using char.GetNumericValue(c),
                                                          //then to int; to avoid casting our char to the corresponding ASCII code,
                                                          //then get a totally different intger value in case of direct casting from [char] to [int]
            }
            else if (char.IsLetter(c))      //It's a letter
            {
                if (char.IsUpper(c))        //Uppercase letter
                {
                    return 11 + 2 * ((int)c - (int)'A');
                }
                else                        //Lowercase letter
                {
                    return 10 + 2 * ((int)c - (int)'a');
                }
            }
            else                            //It's a symbol
            {
                return -1;
            }
        }
    }
}
