using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Statistical_Analysis
{
    internal class MomentsOfRV
    {
        public double Mean { get; set; }
        public double Variance { get; set; }
        public double Skewness { get; set; }
        public double Kurtosis { get; set; }

        void updateMean(List<Character> List, int totalCharCount)
        {
            double mean = 0.0;
            for (int i=0; i<List.Count; i++) 
            {
                mean += ((double)i * (((double)List[i].occurrence_no)/(double)totalCharCount));
            }
            Mean = Math.Round(mean, 2, MidpointRounding.ToEven);
        }

        void updateVariance(List<Character> List, int totalCharCount)
        {
            double E_x2 = 0.0;
            for (int i = 0; i < List.Count; i++)
            {
                E_x2 += ((double)(Math.Pow(i,2)) * (((double)List[i].occurrence_no) / (double)totalCharCount));
            }
            Variance = E_x2 - Math.Pow(Mean,2.0);
            Variance = Math.Round(Variance, 2, MidpointRounding.ToEven);
        }

        void updateSkewnessAndKurtosis(List<Character> List, int totalCharCount)
        {
            double E_x3 = 0.0;
            double E_x4 = 0.0;

            for (int i = 0; i < List.Count; i++)
            {
                E_x3 += ((double)(Math.Pow(i, 3)) * (((double)List[i].occurrence_no) / (double)totalCharCount));
                E_x4 += ((double)(Math.Pow(i, 4)) * (((double)List[i].occurrence_no) / (double)totalCharCount));
            }
            //Skewness
            Skewness = (E_x3 - (3.0*Mean*Variance) - Math.Pow(Mean, 3.0))/ Math.Pow(Math.Sqrt(Variance), 3.0);
            Skewness = Math.Round(Skewness, 4, MidpointRounding.ToEven);

            //Kurtosis
            Kurtosis = (E_x4 - (4.0*Mean*E_x3) + (6.0* Math.Pow(Mean, 2.0) *Variance) + (3.0* Math.Pow(Mean, 4.0)))/ Math.Pow(Variance, 2.0);
            Kurtosis = Math.Round(Kurtosis, 3, MidpointRounding.ToEven);
        }

        public void updateMoments(List<Character> List, int totalCharCount)
        {
            updateMean(List, totalCharCount);
            updateVariance(List, totalCharCount);
            updateSkewnessAndKurtosis(List, totalCharCount);
        }
    }
}
