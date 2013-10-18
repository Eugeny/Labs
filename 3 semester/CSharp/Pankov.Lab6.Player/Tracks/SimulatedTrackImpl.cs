using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Serialization;

namespace Pankov.Lab6.Player.Tracks
{
    [DataContract]
    internal class SimulatedTrackImpl : Track
    {
        private Thread player;
        private bool playing = false;
        private bool abort = false;

        [DataMember]
        public override int Length
        {
            get { return 80 * 5000; }
        }

        public override int Position
        {
            get;
            set;
        }

        internal override void Initialize(string descriptor)
        {
            Name = descriptor;
            Artist = "DJ Simulator";
            PostLoad(new StreamingContext());
        }

        [OnDeserializing]
        internal void PostLoad(StreamingContext ctx)
        {
            player = new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    bool nowplaying = playing;
                    SpinWait.SpinUntil(new Func<bool>(delegate
                    {
                        return nowplaying != playing;
                    }), 50);
                    if (Position >= Length)
                        Pause();
                    if (playing)
                        Position += 50;
                    if (abort) return;
                }
            }));
            player.Start();
        }

        public override void Play()
        {
            playing = true;
        }

        public override void Pause()
        {
            playing = false;
        }

        public override bool IsPlaying()
        {
            return playing;
        }

        public override void Dispose()
        {
            abort = true;
            player.Join();
        }
    }
}
