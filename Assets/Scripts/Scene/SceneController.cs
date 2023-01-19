using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene.SceneControl
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController instance;
        private int lastLevelIndex = 0;
        private List<Transform> objectsPos = new List<Transform>();              
        [SerializeField] private Transform Hero;
        [SerializeField] private ItemManager items;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            instance = this;
            DontDestroyOnLoad(instance);
            DeleteCheckpoint();
            ClearObjects();
        }

        public static SceneController getInstance() { return instance; }
        public void FirstOrLastLevel()
        {
            SceneManager.LoadScene(lastLevelIndex);
        }
        public void NextLevel()
        {
            DeleteCheckpoint();
            ClearObjects();
            if (SceneManager.sceneCountInBuildSettings <= lastLevelIndex + 1)
                SceneManager.LoadScene(0);
            else
                SceneManager.LoadScene(lastLevelIndex + 1);
        }
        public void Restart()
        {
            if (PlayerPrefs.HasKey("checkX") && items.getLives() > 0)
            {
                items.RemoveLife();
                Hero.transform.position = new Vector2(PlayerPrefs.GetFloat("checkX"), PlayerPrefs.GetFloat("checkY"));
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
            for (int i = 0; i < objectsPos.Count; i++)
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
