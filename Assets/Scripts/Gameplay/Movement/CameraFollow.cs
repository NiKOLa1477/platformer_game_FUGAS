using UnityEngine;

namespace Gameplay.Camera.Follow
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothSpeed;
        [SerializeField] private Vector3 offset;

        private void LateUpdate()
        {
            var targetPosition = target.position + offset;
            var resultPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            transform.position = resultPosition;
        }
    }
}
