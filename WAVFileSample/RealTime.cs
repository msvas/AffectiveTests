using System;
using System.IO;
using NAudio.Wave;
using Microsoft.Xna.Framework.Audio;

namespace NDtw
{
    public class RealTime
    {
        public WaveInEvent waveSource = null;
        public WaveFileWriter waveFile = null;
        public MemoryStream audioStream;
        //public byte[] wavOut;
        Microphone  mic = Microphone.Default;
        //bool isMicrophoneRecording;
        bool hasData = false;
        int timer = 0;
        WaveBuffer buffer;

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
            
            audioStream = new MemoryStream();

            waveFile = new WaveFileWriter(audioStream, waveSource.WaveFormat);

            waveSource.StartRecording();
            return true;
        }

        public bool stopRecording()
        {
            if (waveSource != null)
            {
                waveSource.StopRecording();
            }

            return true;
        }

        public MemoryStream wavMem()
        {
            return audioStream;
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
            }
        }

        public void disposeStream()
        {
            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }
        }
    }
}
