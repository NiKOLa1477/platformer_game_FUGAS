using Gameplay.Items.Manager;
using Scene.SceneControl;
using UnityEngine;

namespace Gameplay.Items
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