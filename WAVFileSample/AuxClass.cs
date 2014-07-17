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
        string soundName = @"stroking\011.wav";
        int fileId;
        Program program = new Program();
        LomontFFT fft = new LomontFFT();
        Correlation crossCorr;
        public int offset;
        RealTime recorder = new RealTime();

        public AuxClass()
        {
            do
            {
                if (recorder.checkMic())
                {
                    Console.ReadKey();
                    recorder.startRecording();
                    Console.ReadKey();
                    recorder.stopRecording();

                    program.openWav(soundName, out leftAudio, out rightAudio);
                    program.openWav("Test0001.wav", out leftCompared, out rightCompared);

                    #region stuff

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

                    #endregion

                    double value;
                    double[] result;
                    alglib.corrr1d(leftAudio, leftAudio.Length, leftCompared, leftCompared.Length, out result);

                    crossCorr = new Correlation(result, leftAudio, leftCompared, out offset, out value);

                    Console.WriteLine(value);
                }
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }
    }
}
