using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Pankov.Lab6.Player.Tracks
{
    [DataContract]
    [KnownType(typeof(SimulatedTrackImpl))]
    public abstract class Track : IDisposable
    {
        [DataMember]
        public string Name { get; protected set; }
        [DataMember]
        public string Artist { get; protected set; }

        public abstract int Length
        {
             get;
        }

        public abstract int Position
        {
            get;
            set;
        }

        internal abstract void Initialize(string descriptor);
        public abstract void Play();
        public abstract void Pause();
        public abstract bool IsPlaying();

        public abstract void Dispose();
    }
}
