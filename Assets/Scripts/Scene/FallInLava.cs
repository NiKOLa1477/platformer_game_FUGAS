using System.Collections;
using System.Collections.Generic;
using Scene.SceneControl;
using UnityEngine;

namespace Gameplay.Fall_in_Lava
{
    public class FallInLava : MonoBehaviour, ILoadable
    {
        [SerializeField] private ItemManager items;
        public void Load()
        {           
            SceneController.getInstance().Restart();
        }
    }
}
