using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.TriggerManager
{
    public class TriggerManager : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ILoadable>(out var scene))
            {
                scene.Load();
            }
            if (collision.TryGetComponent<ICollectable>(out var item))
            {
                item.Collect();
            }
        }
    }
}
