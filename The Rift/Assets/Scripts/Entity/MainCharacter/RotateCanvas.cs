using UnityEngine;
using VContainer;

namespace MainCharacter
{
    public class RotateCanvas : MonoBehaviour
    {
        [SerializeField] public Transform mainCamera;
        [SerializeField] public Transform _object;
        [SerializeField] public Transform _canvas;
        
        
        [SerializeField] public Vector3 offset;
        void Awake()
        {
            _object = transform.parent;
            transform.SetParent(_canvas);
        }
        void Update()
        {
            if (!_object)
            {
                Destroy(this.gameObject);
                return;
            }
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
            transform.position = _object.position + offset;
        }
    }
}