using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILNumerics;
using System.Diagnostics;

namespace NDtw
{
    public class AuxClass
    {
        public double[] leftAudio;
        public double[] rightAudio;
        public double[] leftCompared;
        public double[] rightCompared;
        string soundName = @"howling\001.wav";
        int fileId;
        Program program = new Program();
        LomontFFT fft = new LomontFFT();
        Correlation crossCorr;
        public int offset;
        RealTime recorder = new RealTime();
        Stopwatch stopwatchProcess = new Stopwatch();
        Stopwatch stopwatchRecord = new Stopwatch();

        public AuxClass()
        {
            do
            {
                if (recorder.checkMic())
                {
                    Console.WriteLine("Press any key to start recording");
                    Console.ReadKey(); Console.WriteLine("Recording..."); stopwatchRecord.Start();
                    recorder.startRecording();
                    Console.ReadKey();
                    recorder.stopRecording(); stopwatchRecord.Stop();
                    Console.WriteLine("Sound recorded. Processing...");

                    stopwatchProcess.Start();
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

                    stopwatchProcess.Stop();
                    Console.WriteLine("Value: " + value);

                    if (value > 0.4)
                        Console.WriteLine("Action: Stroking");
                    else
                        Console.WriteLine("Action: Not Stroking");
                    Console.WriteLine("Time elapsed recording: " + stopwatchRecord.ElapsedMilliseconds + " ms");
                    Console.WriteLine("Time elapsed processing: " + stopwatchProcess.ElapsedMilliseconds + " ms");
                    Console.WriteLine("Press spacebar to close, anything else to start over \r\n");
                }
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }
    }
}
