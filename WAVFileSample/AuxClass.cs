using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILNumerics;
using System.Diagnostics;
using System.Timers;

namespace NDtw
{
    public class AuxClass
    {
        public static double[] leftAudio;
        public static double[] rightAudio;
        public static double[] leftCompared;
        public static double[] rightCompared;
        private static string soundName = @"stroking\001.wav";
        static Program program = new Program();
        public static LomontFFT fft = new LomontFFT();
        static Correlation crossCorr;
        public static int offset;
        private static RealTime recorder = new RealTime();
        private static Stopwatch stopwatchProcess = new Stopwatch();
        private static Stopwatch stopwatchRecord = new Stopwatch();
        private static Timer recordWindow = new System.Timers.Timer(500);

        public AuxClass()
        {
            if (recorder.checkMic())
            {
                //Console.WriteLine("Press any key to start recording");
                //Console.ReadKey(); 
                Console.WriteLine("Recording...");
                stopwatchRecord.Start();
                recorder.startRecording();
                //Console.ReadKey();

                recordWindow.Elapsed += OnTimedEvent;
                recordWindow.Enabled = true;

                Console.ReadKey();
                recordWindow.Enabled = false;
            }
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            recorder.stopRecording();
            stopwatchRecord.Stop();

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
            //Console.WriteLine("Press spacebar to close, anything else to start over \r\n");

            Console.WriteLine("Recording...");
            recorder.startRecording();
            stopwatchRecord.Start();
        }
    }
}
