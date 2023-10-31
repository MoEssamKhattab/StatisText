using System;

public class Character
{
    public class Character
    {
        public char Char { set; get; }
        public int Number { set; get; }

        int mapToRandomVariable(char c)
        {
            if (char.IsNumber(c))           //It's a number//
            {
                return (int)c;
            }
            else if (char.IsLetter(c))
            {
                if (char.IsUpper(c))        //I's an uppercase letter//
                {
                    return 11 + 2 * ((int)c - (int)'A');
                }
                else                        //It's a lowercase letter//
                {
                    return 10 + 2 * ((int)c - (int)'a');
                }
            }
            else                            //It's a symbol//
            {
                return -1; //Do nothing
            }
        }
    }
}
