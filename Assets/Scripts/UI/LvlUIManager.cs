using Gameplay.Movement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Level
{
    public class LvlUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text ScoreText, LivesText;
        [SerializeField] private GameObject PauseWind;
        [SerializeField] private Movement Hero;
        [SerializeField] private Button PauseBtn;
        private void Awake()
        {
            LivesText.text = "3";
            ScoreText.text = (PlayerPrefs.HasKey("Score")) ? PlayerPrefs.GetInt("Score").ToString() : "0";
        }
        public void UpdScore(int score) { ScoreText.text = score.ToString(); }
        public void UpdLives(int lives) { LivesText.text = lives.ToString(); }

        public void onPauseBtn()
        {
            Hero.blockMovement();
            PauseWind.SetActive(!PauseWind.activeInHierarchy);
        }
        public void blockPauseBtn() { PauseBtn.interactable = !PauseBtn.interactable; }
    }
}
