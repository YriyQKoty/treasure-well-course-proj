using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameManagement
{
    public class Resetter : MonoBehaviour
    {
        //field which sets the method to reset
        public UnityEvent onReset;

        public void Reset()
        {
            onReset.Invoke();
        }
    }
}