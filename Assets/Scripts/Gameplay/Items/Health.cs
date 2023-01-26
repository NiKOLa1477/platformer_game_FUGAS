using System.Collections;
using System.Collections.Generic;
using Gameplay.Items.Manager;
using Scene.SceneControl;
using UnityEngine;

namespace Gameplay.Items.Lives
{
    public class Health : MonoBehaviour, ICollectable
    {
        [SerializeField] private ItemManager items;
        public void Collect()
        {
            items.addLive();
            SceneController.getInstance().AddObject(transform);
            gameObject.SetActive(false);
        }
    }
}