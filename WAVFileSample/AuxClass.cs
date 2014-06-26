using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILNumerics; 

namespace NDtw
{
    public class AuxClass
    {
        public double[] leftAudio;
        public double[] rightAudio;
        public double[] leftCompared;
        public double[] rightCompared;
        string soundName = "ambientWav.wav";
        string comparedSound = "strokingWav3.wav";
        Program program = new Program();
        LomontFFT fft = new LomontFFT();
        Correlation crossCorr;
        public int offset;

        public AuxClass()
        {
            program.openWav(soundName, out leftAudio, out rightAudio);
            program.openWav(comparedSound, out leftCompared, out rightCompared);

            //double cost = new Dtw(leftAudio, leftCompared).GetCost();
            //Console.WriteLine(cost);

            /*
            Console.WriteLine(leftAudio.Length + " " + leftCompared.Length);
            SimpleDTW comparison = new SimpleDTW(leftAudio, leftCompared);
            comparison.computeDTW();
            Console.WriteLine(comparison.getSum());
            */

            /*
            foreach (double i in leftAudio)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
            */

            //fft.RealFFT(leftAudio, true);

            /*
            foreach (double i in leftCompared)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
            */

            //fft.RealFFT(leftCompared, true);

            
            double value;
            double[] result;
            alglib.corrr1d(leftAudio, leftAudio.Length, leftCompared, leftCompared.Length, out result);

            /*
            foreach (double i in result)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
            */

            crossCorr = new Correlation(result, leftAudio, leftCompared, out offset, out value);
            
            /*double[] result;
            alglib.corrr1d(leftAudio, leftAudio.Length, leftCompared, leftCompared.Length, out result);

            foreach (double i in result)
            {
                Console.WriteLine(i);
            }*/

            Console.WriteLine(value);
            Console.ReadKey();

        }
    }
}
