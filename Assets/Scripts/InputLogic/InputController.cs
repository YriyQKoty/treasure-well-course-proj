using System.Collections;
using System.Collections.Generic;
using SingletonTemplate;
using UnityEngine;

namespace Singletons
{
    /// <summary>
    /// Регулює ввід користувача
    /// </summary>
    public class InputController : Singleton<InputController>
    {
        private float _sidewaysMotion = 0.0f;
       [SerializeField] private float sensitivity;
        public float SideWaysMotion => _sidewaysMotion;
        
        void Update()
        {
            Vector3 acceleration = Input.acceleration;

            _sidewaysMotion = acceleration.x * sensitivity;
        }
    }
}