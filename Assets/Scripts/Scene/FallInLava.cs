using System.Collections;
using System.Collections.Generic;
using Scene.SceneControl;
using UI.Level;
using UnityEngine;

namespace Gameplay.Fall_in_Lava
{
    public class FallInLava : MonoBehaviour, ILoadable
    {        
        [SerializeField] private LvlUIManager UIManager;
        [SerializeField] private GameObject Hero;
        public void Load()
        {
            Hero.SetActive(!Hero.activeInHierarchy);
            UIManager.blockPauseBtn();
            UIManager.onPauseBtn();
        }
    }
}
