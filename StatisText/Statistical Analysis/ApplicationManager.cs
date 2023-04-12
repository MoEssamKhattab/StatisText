using LiveCharts.WinForms;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiveCharts.Wpf;
using System.Windows.Ink;
using System.Reflection.Emit;
using System.Windows.Documents;
using System.Windows;

namespace Statistical_Analysis
{
    internal class ApplicationManager
    {
        public void intialize(ref List<Character> Characters)
        {

            for (int i = 0; i < Defs.MaxCharNumber; i++)        //Filling the Characters_List with objects of all possible characters (62 chars)
            {
                Characters.Add(new Character());
            }

            for (int i = 0; i <= 9; i++)  //Assigning values to the Numbers
            {
                Characters[i].Char = Convert.ToChar($"{i}");        //Convert int to string (first), then to char; to avoid coverting our int
                                                                    //as ASCII code into its corresponding char (lose our number)
                Characters[i].occurrence_no = 0;                    //Initially

                //Characters[i].RV = i;               //No need to allocate memory for the property of RV map,as we save characters into the list ordered,
                                                      //hence, the index can represent that RV map.
            }

            //Assigning values to the Uppercase letters
            for (int i = 0; i < 26; i++)   //looping on 26 letters (uppercase)
            {
                char c = Convert.ToChar('A' + i);
                int index = Character.convertCharToRV(c);
                Characters[index].Char = c;
                Characters[index].occurrence_no = 0;                     //Initially
                //Characters[index].RV = index;
            }

            //Assigning values to the Lowercase letters
            for (int i = 0; i < 26; i++)   //looping on 26 letters (lowercase)
            {
                char c = Convert.ToChar('a' + i);
                int index = Character.convertCharToRV(c);
                Characters[index].Char = c;
                Characters[index].occurrence_no = 0;                     //Initially
                //Characters[index].RV = index;
            }
        }

        public void resetCharactersList(ref List<Character> CharactersList)    //ref int totalCharCount)
        {
            #region Reset
            CharactersList.Sort((x, y) => (Character.convertCharToRV(x.Char)).CompareTo(Character.convertCharToRV(y.Char))); //Reordering the list descendingly
            foreach (Character c in CharactersList)
            {
                c.occurrence_no = 0;
                //c.PMF = 0; c.CDF = 0;
            }
            #endregion

            //The above Rest region could be replaced by the following
            /*  CharactersList.Clear();
             *  initialize(CharactersList)
             */

            //totalCharCount = 0;     //No need to update the totalCharCount here as extractFileDate() function always handles it.
        }
        public void sortList(ref List<Character> List)
        {
            List.Sort((y, x) => x.occurrence_no.CompareTo(y.occurrence_no));      //Sorting the list ascendingly according to occurrence times
        }

        public StreamReader readFile(string path)       //reads the input file, 
        {
            StreamReader inFile = new StreamReader(path);
            return inFile;
        }

        public int extractFileData(StreamReader inFile, ref List<Character> charList)   //Read file data, update the characters list and return total number of characters
        { 
            char C;
            int total_characters = 0;

            while (inFile.Peek() != -1)
            {
                C = Convert.ToChar(inFile.Read());
                int RV = Character.convertCharToRV(C);

                if (RV == -1)
                    continue;
                else
                {
                    charList[RV].occurrence_no += 1;
                    total_characters += 1;
                }
            }
            return total_characters;                                     //In case of empty file, it returns 0
        }

        //public void updatePMFandCDF(List<Character> charList, int totalCharCount)
        //{
        //    double sum = 0.0;
        //    foreach (Character c in charList) 
        //    {
        //        c.PMF = (double)c.occurrence_no / (double)totalCharCount;
        //        sum += c.PMF;

        //        c.CDF= sum;
        //    }
        //}




        public void startInteractiveMode(LiveCharts.WinForms.CartesianChart PropabilityChart,
            LiveCharts.WinForms.CartesianChart PMFChart, LiveCharts.WinForms.CartesianChart CDFChart, 
            List<Character> CharactersList, int totalCharCount)
        {
            //Change view
            //UpdateCharts(PropabilityChart, PMFChart, CDFChart, CharactersList, totalCharCount);

        }



        #region test
        public void printList(List<Character> Characters)
        {
            for (int i = 0; i< 20; i++)  
            {
                MessageBox.Show($"Char [{Characters[i].Char}]     RV= {i}      Occurrence_no= {Characters[i].occurrence_no}");
            }
        }
        #endregion
    }
}
