using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pankov.Lab6.Player.Tracks
{
    public class TrackFactory 
    {
        public virtual Track CreateTrack(string descriptor) { return null; }

        public static TrackFactory GetFactory()
        {
            return new SimulatedTrackFactory();
        }
    }
}
