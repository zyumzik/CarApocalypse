using System;
using UnityEngine;

namespace CameraModule
{
    public class CameraTargetController : MonoBehaviour
    {
        [SerializeField] private float _globalOffsetX  = 0;

        private Vector3 _initialPosition;

        private void Awake()
        {
            _initialPosition = transform.localPosition;
        }

        private void Update()
        {
            var local = _initialPosition;

            var world = transform.parent != null 
                ? transform.parent.TransformPoint(local)
                : local;

            world.x = _globalOffsetX;

            transform.position = world;

            transform.forward = Vector3.forward;
        }
    }
}