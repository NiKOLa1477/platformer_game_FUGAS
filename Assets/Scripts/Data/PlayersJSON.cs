using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManager
{
    [Serializable]
    public class PlayersJSON
    {
        public Dictionary<string, PlayerManager.Data> players { get; private set; }
        public List<string> top { get; private set; }
        public PlayersJSON(Dictionary<string, PlayerManager.Data> players, List<string> top)
        {
            this.players = players;
            this.top = top;
        }
    }
}
