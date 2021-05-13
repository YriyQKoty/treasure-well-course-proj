using System;
using System.Collections;
using System.Collections.Generic;
using Singletons;
using UnityEngine;

namespace GnomeBehaviour
{
    /// <summary>
    /// Відповідає за фізичні властивості коливань головного персонажу
    /// </summary>
    public class Swinging : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] private float swingSensitivity;

        [SerializeField] private Rigidbody2D rigidbody;
    
        private void FixedUpdate()
        {
            if (rigidbody == null)
            {
                Destroy(this);
                return;
            }
        
            float swing = InputController.Instance.SideWaysMotion;

            Vector2 force = new Vector2(swing*swingSensitivity, 0);
        
            rigidbody.AddForce(force);
        }
    }
}