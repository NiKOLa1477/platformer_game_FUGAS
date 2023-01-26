using Scene.SceneControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Items.Manager;

namespace Gameplay.Items.Coins
{
    public class Coin : MonoBehaviour, ICollectable
    {
        [SerializeField] private int amount;
        [SerializeField] private ItemManager items;
        public void Collect()
        {
            items.addScore(amount);
            SceneController.getInstance().AddObject(transform);
            gameObject.SetActive(false);
        }
    }
}