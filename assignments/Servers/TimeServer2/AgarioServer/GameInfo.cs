using System.Collections.Generic;

namespace AgarioServer
{
    public class GameInfo
    {
        public bool started;
        public Dictionary<Players, PlayerInfo> players = new ();
    }
    
    
}