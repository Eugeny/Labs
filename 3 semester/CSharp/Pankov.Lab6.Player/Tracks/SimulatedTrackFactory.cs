using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pankov.Lab6.Player.Tracks
{
    public class SimulatedTrackFactory : TrackFactory
    {
        public override Track CreateTrack(string descriptor)
        {
            Track t = new SimulatedTrackImpl();
            t.Initialize(descriptor);
            return t;
        }
    }
}
