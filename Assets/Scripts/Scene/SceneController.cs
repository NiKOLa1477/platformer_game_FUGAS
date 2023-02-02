using System.Collections.Generic;
using UI.Level;
using DataManager;
using Gameplay.Items.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene.SceneControl
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController instance;
        private int lastLevelIndex;
        private static int firstLevelIndex = 4;
        private List<Transform> objectsPos = new List<Transform>();              
        private Transform Hero;
        private ItemManager items;
        private LvlUIManager UIManager;
        private static bool isLoaded;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            instance = this;           
            DeleteCheckpoint();
            ClearObjects();           
            lastLevelIndex = SceneManager.GetActiveScene().buildIndex;
            if(lastLevelIndex == 0 && !isLoaded) 
            { 
                PlayerManager.Init(firstLevelIndex); 
                isLoaded = true; 
            }           
            if (lastLevelIndex >= firstLevelIndex)
            {                
                items = FindObjectOfType<ItemManager>();
                Hero = items.transform;
                UIManager = FindObjectOfType<LvlUIManager>();
            }
            lastLevelIndex = (lastLevelIndex < firstLevelIndex) ? getLastLevelInd() : lastLevelIndex;
            PlayerPrefs.SetInt("LastLvlInd", lastLevelIndex);
        }

        public static SceneController getInstance() { return instance; }
        public void FirstOrLastLevel() { Load(getLastLevelInd()); }
        public void Menu() { PlayerManager.SavePlayers(); Load(0); }
        public void Exit() { PlayerManager.SavePlayers(); Application.Quit(); }
        public void NextLevel()
        {
            DeleteCheckpoint();
            ClearObjects();
            if (SceneManager.sceneCountInBuildSettings <= lastLevelIndex + 1)
                Menu();
            else
            {                         
                Load(lastLevelIndex + 1);
            }              
        }
        public void Levels() { Load(1); }
        public void Options() { Load(2); }
        public void Records() { Load(3); }
        private void Load(int index) { SceneManager.LoadScene(index); }
        public void LoadLevel(int levelCount) { Load(firstLevelIndex - 1 + levelCount); }
        private int getLastLevelInd()
        {
            if (PlayerPrefs.HasKey("LastLvlInd"))
                return PlayerPrefs.GetInt("LastLvlInd");
            else
                return firstLevelIndex;
        }
        public void Restart()
        {
            if (PlayerPrefs.HasKey("checkX") && items.getLives() > 0)
            {
                items.RemoveLife();                
                UIManager.onPauseBtn();
                UIManager.blockPauseBtn();
                Hero.transform.position = new Vector2(PlayerPrefs.GetFloat("checkX"), PlayerPrefs.GetFloat("checkY"));
                Hero.gameObject.SetActive(true);
                items.loadData();
                RestoreItems();
            }
            else
            {
                DeleteCheckpoint();
                ClearObjects();                
                Load(SceneManager.GetActiveScene().buildIndex);
            }
            
        }
        private void DeleteCheckpoint()
        {
            if (PlayerPrefs.HasKey("checkX"))
            {
                PlayerPrefs.DeleteKey("checkX");
                PlayerPrefs.DeleteKey("checkY");
            }
        } 
        public void AddObject(Transform obj) { objectsPos.Add(obj); }
        public void ClearObjects()
        {
            for (int i = objectsPos.Count - 1; i >= 0; i--)
            {
                objectsPos.RemoveAt(i);                
            }              
        }
        private void RestoreItems()
        {
            for (int i = 1; i < objectsPos.Count; i++)
            {
                objectsPos[i].gameObject.SetActive(true);
            }
        }
    }
}
