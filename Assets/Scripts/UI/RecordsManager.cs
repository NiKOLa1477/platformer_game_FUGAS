using DataManager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Records
{
    public class RecordsManager : MonoBehaviour
    {
        [SerializeField] private List<TMP_Text> recordsText;
        private void Awake()
        {
            var topPlayers = PlayerManager.topPlayers;
            for (int i = 0; i < topPlayers.Count; i++)
            {
                recordsText[i].text = (i + 1).ToString() + ". " + topPlayers[i] + ": " + PlayerManager.Players[topPlayers[i]].Score;
            }
            setMinTextSize();           
        }
        private void setMinTextSize()
        {
            float min = 100;
            for (int i = 0; i < PlayerManager.topPlayers.Count; i++)
            {
                if (recordsText[i].fontSizeMin < min) min = recordsText[i].fontSizeMin;
            }
            foreach (var top in recordsText)
            {
                top.enableAutoSizing = false;
                top.fontSize = min;
            }
        }
    }
}