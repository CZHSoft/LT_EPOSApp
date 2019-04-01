using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace OrderDish.Common
{
    public class SoundPlayer : IDisposable
    {
        [DllImport("aygshell.dll")]
        static extern uint SndOpen(string pszSoundFile, ref IntPtr phSound);

        [DllImport("aygshell.dll")]
        static extern uint SndPlayAsync(IntPtr hSound, uint dwFlags);

        [DllImport("aygshell.dll")]
        static extern uint SndClose(IntPtr hSound);

        [DllImport("aygshell.dll")]
        static extern uint SndStop(int SoundScope, IntPtr hSound);

        [DllImport("aygshell.dll")]
        static extern uint SndPlaySync(string pszSoundFile, uint dwFlags);

        const int SND_SCOPE_PROCESS = 0x1;


        string mySoundLocation = string.Empty;

        public string SoundLocation
        {
            get { return mySoundLocation; }
            set { mySoundLocation = value; }
        }

        IntPtr mySound = IntPtr.Zero;
        Thread myThread = null;
        public void PlayLooping()
        {
            myThread = new Thread(() =>
            {
                while (true)
                {
                    SndPlaySync(mySoundLocation, 0);
                }
            }
            );
            myThread.Start();
        }

        public void Play()
        {
            SndOpen(mySoundLocation, ref mySound);
            SndPlayAsync(mySound, 0);
        }

        public void Stop()
        {
            if (myThread != null)
            {
                SndStop(SND_SCOPE_PROCESS, IntPtr.Zero);
                myThread.Abort();
                myThread = null;
            }
            if (mySound != IntPtr.Zero)
            {
                SndStop(SND_SCOPE_PROCESS, IntPtr.Zero);
                SndClose(mySound);
                mySound = IntPtr.Zero;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Stop();
        }

        #endregion
    }
}