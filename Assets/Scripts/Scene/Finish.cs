using Scene.SceneControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace Scene.Finish
{
    public class Finish : MonoBehaviour, ILoadable
    {
        [SerializeField] private ItemManager items;
        public void Load()
        {
            items.saveData();
            SceneController.getInstance().NextLevel();
        }
    }
}
