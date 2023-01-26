using System.Collections;
using System.Collections.Generic;
using UI.Level;
using Unity.VisualScripting;
using Gameplay.Items.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene.SceneControl
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController instance;
        private int lastLevelIndex;
        private static int firstLevelIndex = 2;
        private List<Transform> objectsPos = new List<Transform>();              
        [SerializeField] private Transform Hero;
        [SerializeField] private ItemManager items;
        [SerializeField] private LvlUIManager UIManager;

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
            lastLevelIndex = (lastLevelIndex < firstLevelIndex) ? getLastLevelInd() : lastLevelIndex;
            PlayerPrefs.SetInt("LastLvlInd", lastLevelIndex);
        }

        public static SceneController getInstance() { return instance; }
        public void FirstOrLastLevel()
        {
            SceneManager.LoadScene(getLastLevelInd());
        }
        public void Menu()
        {
            SceneManager.LoadScene(0);
        }
        public void Exit() { Application.Quit(); }
        public void NextLevel()
        {
            DeleteCheckpoint();
            ClearObjects();
            if (SceneManager.sceneCountInBuildSettings <= lastLevelIndex + 1)
                Menu();
            else
            {                         
                SceneManager.LoadScene(lastLevelIndex + 1);
            }              
        }
        public void LoadLevel(int index) { SceneManager.LoadScene(index); }
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        public void AddObject(Transform obj)
        {          
            objectsPos.Add(obj);           
        }
        public void ClearObjects()
        {
            for (int i = objectsPos.Count - 1; i >= 0; i--)
            {
                objectsPos.RemoveAt(i);                
            }              
        }
        public void RestoreItems()
        {
            for (int i = 1; i < objectsPos.Count; i++)
            {
                objectsPos[i].gameObject.SetActive(true);
            }
        }
    }
}
