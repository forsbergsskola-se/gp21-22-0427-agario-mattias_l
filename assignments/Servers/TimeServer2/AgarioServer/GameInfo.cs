using System.Collections.Generic;
using AgarioShared.AgarioShared.Enums;
using AgarioShared.AgarioShared.Messages;

namespace AgarioServer
{
    public class GameInfo
    {
        public bool started;
        public Dictionary<PlayerCounter, UpdateMessage> players = new ();
    }
}