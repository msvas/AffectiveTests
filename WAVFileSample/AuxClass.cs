using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDtw
{
    public class AuxClass
    {
        public double[] leftAudio;
        public double[] rightAudio;
        public double[] leftCompared;
        public double[] rightCompared;
        string soundName = "wavTest.wav";
        string comparedSound = "wavTest2.wav";
        Program program = new Program();

        public AuxClass()
        {
            program.openWav(soundName, out leftAudio, out rightAudio);
            program.openWav(comparedSound, out leftCompared, out rightCompared);

            //double cost = new Dtw(leftAudio, leftCompared).GetCost();
            //Console.WriteLine(cost);

            //Console.WriteLine(leftAudio.Length + " " + leftCompared.Length);
            //SimpleDTW comparison = new SimpleDTW(leftAudio, leftCompared);
            //comparison.computeDTW();
            //Console.WriteLine(comparison.getSum());
        }
    }
}
