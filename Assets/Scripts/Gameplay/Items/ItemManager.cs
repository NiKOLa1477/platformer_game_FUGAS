using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private int score, hearts = 3;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Score"))
            loadData();
    }
    public void addScore(int amount) { score += amount; }
    public void RemoveLife()
    {
        if (hearts > 0)
            hearts--;
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
