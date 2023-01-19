using Scene.SceneControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
