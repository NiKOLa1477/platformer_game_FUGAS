using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DataManager
{
    public class PlayerManager
    {
        public struct Data
        {
            public int Score, Level;
            public Data(int score, int level) { Score = score; Level = level; }
            public bool Equal(Data other)
            {
                return other.Score == Score && other.Level == Level;
            }
        }
        public static Dictionary<string, Data> Players { get; private set; }
        public static List<string> topPlayers { get; private set; }
        private static int firstLevel;
        public static void Init(int firstLevelInd)
        {
            firstLevel = firstLevelInd;
            if (File.Exists(Application.persistentDataPath + "/players.txt"))
                LoadPlayers();
            else
            {
                Players = new Dictionary<string, Data>();
                AddNewPlayer("NoName");               
                setTopPlayers();
            }
        }
        public static void AddPlayer(string name)
        {
            if (Players.ContainsKey(name))
            {
                PlayerPrefs.SetString("Name", name);
                PlayerPrefs.SetInt("Score", Players[name].Score);
                PlayerPrefs.SetInt("LastLvlInd", Players[name].Level);
            }
            else
                AddNewPlayer(name);
        }
        private static void setTopPlayers()
        {
            topPlayers = new List<string>();
            for (int i = 0; i < 10 && i < Players.Count; i++)
            {
                int max = -1;
                string name = "";
                int prevMax = -1;
                string prevName = "";
                foreach (var player in Players)
                {                  
                    if (player.Value.Score > max)
                    {
                        prevMax = max;
                        max = player.Value.Score;
                        prevName = name;
                        name = player.Key;
                    }
                    if(topPlayers != null)
                    {
                        foreach (var top in topPlayers)
                        {
                            if (name == top)
                            {
                                max = prevMax;
                                name = prevName;
                                break;
                            }
                        }
                    }                  
                }
                topPlayers.Add(name);
            }
        }
        private static void AddNewPlayer(string name)
        {
            Players[name] = new Data(0, firstLevel);
            PlayerPrefs.SetString("Name", name);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("LastLvlInd", firstLevel);
            setTopPlayers();
            SavePlayers();
        }
        public static void LoadPlayers()
        {
            if (File.Exists(Application.persistentDataPath + "/players.txt"))
            {
                var json = File.ReadAllText(Application.persistentDataPath + "/players.txt");
                var PlayersJson = JsonConvert.DeserializeObject<PlayersJSON>(json);
                Players = PlayersJson.players;
                topPlayers = PlayersJson.top;                
            }
        }
        public static void SavePlayers()
        {
            var data = new Data(PlayerPrefs.GetInt("Score"), PlayerPrefs.GetInt("LastLvlInd"));
            if (!Players[PlayerPrefs.GetString("Name")].Equal(data) || SceneManager.GetActiveScene().buildIndex == 0)
            {
                Players[PlayerPrefs.GetString("Name")] = data;
                setTopPlayers();
                var PlayersJson = new PlayersJSON(Players, topPlayers);
                var json = JsonConvert.SerializeObject(PlayersJson);
                File.WriteAllText(Application.persistentDataPath + "/players.txt", json);
            }           
        }
    }
}
