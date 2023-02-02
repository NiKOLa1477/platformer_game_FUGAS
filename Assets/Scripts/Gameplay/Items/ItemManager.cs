using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Items.Manager
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] private UnityEvent<int> onScoreChanged, onLivesChanged; 
        private int score, hearts = 3;
        private void Awake()
        {
            if (PlayerPrefs.HasKey("Score"))
                loadData();
        }
        public void addScore(int amount)
        {
            score += amount;
            onScoreChanged?.Invoke(score);
        }
        public void addLive()
        {
            hearts++;
            onLivesChanged?.Invoke(hearts);
        }
        public void RemoveLife()
        {
            if (hearts > 0)
                hearts--;
            onLivesChanged?.Invoke(hearts);
        }
        public int getLives() { return hearts; }
        public int getScore() { return score; }
        public void loadData()
        {
            score = PlayerPrefs.GetInt("Score");
            onScoreChanged?.Invoke(score);
        }
        public void saveData()
        {
            PlayerPrefs.SetInt("Score", getScore());
        }
    }
}
