using System;
using UnityEngine;

namespace Utils
{
    public class Rotator : MonoBehaviour
    {
        #region --- Inspector ---
        
        [SerializeField] private float _speed = 1f;
        [SerializeField] private Transform _target;
        [SerializeField] private Axis _axis;
        
        #endregion
        
        
        #region --- Private Methods ---

        private void Start()
        {
            if (_target == null)
            {
                _target = transform;
            }
        }

        private void Update()
        {
            switch (_axis)
            {
                case Axis.X:
                    _target.Rotate(_speed * Time.deltaTime, 0f, 0f);
                    break;
                case Axis.Y:
                    _target.Rotate(0f, _speed * Time.deltaTime, 0f);
                    break;
                case Axis.Z:
                    _target.Rotate(0f, 0f, _speed * Time.deltaTime);
                    break;
            }
        }
        
        #endregion
    }
}