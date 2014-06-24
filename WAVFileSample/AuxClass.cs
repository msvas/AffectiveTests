using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WAVFileSample
{
    public class AuxClass
    {
        public double[] leftAudio;
        public double[] rightAudio;
        string soundName = "wavTest.wav";
        Program program = new Program();

        public AuxClass()
        {
            program.openWav(soundName, out leftAudio, out rightAudio);
        }
    }
}
