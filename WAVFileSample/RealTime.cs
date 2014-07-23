using System;
using NAudio.Wave;
using Microsoft.Xna.Framework.Audio;

namespace NDtw
{
    public class RealTime
    {
        public WaveInEvent waveSource = null;
        public WaveFileWriter waveFile = null;
        Microphone  mic = Microphone.Default;
        //bool isMicrophoneRecording;
        bool hasData = false;
        int timer = 0;

        public bool checkMic()
        {
            if (mic == null)
            {
                return false; // No microphone is attached to the device
            }
            return true;
        }

        public bool startRecording()
        {
            waveSource = new WaveInEvent();
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            waveFile = new WaveFileWriter("Test0001.wav", waveSource.WaveFormat);

            waveSource.StartRecording();
            return true;
        }

        public bool stopRecording()
        {
            if (waveSource != null)
            {
                waveSource.StopRecording();
                waveFile.Dispose();
                waveFile = null;
            }

            return true;
        }

        #region button
        /*public void StartBtn_Click(object sender, EventArgs e)
        {
            //StartBtn.Enabled = false;
            //StopBtn.Enabled = true;

            //waveSource = new WaveInEvent();
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            waveFile = new WaveFileWriter(@"C:\Users\msvas\Desktop\Test0001.wav", waveSource.WaveFormat);

            waveSource.StartRecording();
        }

        public void StopBtn_Click(object sender, EventArgs e)
        {
            //StopBtn.Enabled = false;

            waveSource.StopRecording();
        }*/
        #endregion

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }

            //StartBtn.Enabled = true;
        }
    }
}
