using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Level;

namespace Gameplay.Items.Manager
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] private int score, hearts = 3;
        [SerializeField] private LvlUIManager UIManager;
        private void Awake()
        {
            if (PlayerPrefs.HasKey("Score"))
                loadData();
        }
        public void addScore(int amount)
        {
            score += amount;
            UIManager.UpdScore(score);
        }
        public void addLive()
        {
            hearts++;
            UIManager.UpdLives(hearts);
        }
        public void RemoveLife()
        {
            if (hearts > 0)
                hearts--;
            UIManager.UpdLives(hearts);
        }
        public int getLives() { return hearts; }
        public int getScore() { return score; }
        public void loadData()
        {
            score = PlayerPrefs.GetInt("Score");
        }
        public void saveData()
        {
            PlayerPrefs.SetInt("Score", getScore());
        }
    }
}
