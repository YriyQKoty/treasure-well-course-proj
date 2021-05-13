using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Відповідає за переміщення камери на сцені
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// Об'єкт за яким рухається камера
        /// </summary>
        public Transform followedObject;

        /// <summary>
        /// Нижній ліміт камери
        /// </summary>
        [SerializeField]private float bottomLimit = -5f;

        /// <summary>
        /// Верхній ліміт камери
        /// </summary>
        [SerializeField]private float topLimit = 5f;

        private float _followSpeed = 0.5f;
    
        /// <summary>
        /// Оновлення стану камери
        /// </summary>
        void LateUpdate()
        {
            if (followedObject != null)
            {
                var newPosition = this.transform.position;
            
                newPosition.y = Mathf.Lerp(newPosition.y, followedObject.position.y, _followSpeed);

                newPosition.y = Mathf.Min(newPosition.y, topLimit);
                newPosition.y = Mathf.Max(newPosition.y, bottomLimit);

                this.transform.position = newPosition;
            }
        }

    }
}