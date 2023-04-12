using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistical_Analysis
{
    internal static class Defs      //Definitions for constants used all over the program
    {
        public const int MaxCharNumber = 62; //(26 uppecase letters + 26 lowercase letters + 10 numbers)

        enum AppModes 
        { 
            SimulationMode =0,          //Mode in which user enter the input text file
            InteractiveMode =1          //Mode in which user has already entered the iput text file, and show output data
        };
    }
}
