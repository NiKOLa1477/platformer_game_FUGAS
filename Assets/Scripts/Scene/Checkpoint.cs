using Scene.SceneControl;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Items.Manager;
using UnityEngine;

namespace Scene.Checkpoint
{
    public class Checkpoint : MonoBehaviour, ILoadable
    {
        private bool isActivated;
        [SerializeField] private Transform torch;
        [SerializeField] private ItemManager items;
        public void Load()
        {
            if (!isActivated)
            {
                isActivated = true;
                torch.gameObject.SetActive(true);
                SceneController.getInstance().ClearObjects();
                items.saveData();
                PlayerPrefs.SetFloat("checkX", transform.position.x);
                PlayerPrefs.SetFloat("checkY", transform.position.y + 1);
            }
        }
    }
}
