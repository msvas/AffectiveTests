using System;
using System.IO;
using NAudio.Wave;
using Microsoft.Xna.Framework.Audio;

namespace WAVComparison
{
    public class RealTime
    {
        public WaveInEvent waveSource = null;
        public WaveFileWriter waveFile = null;
        public MemoryStream audioStream;
        Microphone  mic = Microphone.Default;

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
