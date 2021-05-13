using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    /// <summary>
    /// Клас-друкар, містить логіку для виводу тексту як на машинці
    /// </summary>
    public class UiTextTypeWriter : MonoBehaviour
    {
        [SerializeField] private float delay = 0.025f;
        private bool _isTypeWriting;
        private IEnumerator coroutine;
    
        public void StartTypeWriting(Text destination, string text)
        {
            coroutine = TypeWriteTo(destination, text);
            StartCoroutine(coroutine);
        }

        public void StopTypeWriting()
        {
            if (_isTypeWriting)
            {
                StopCoroutine(coroutine);
                _isTypeWriting = false;
            }
        
        }
        private IEnumerator TypeWriteTo(Text destination, string text)
        {
            _isTypeWriting = true;
            foreach ( var c in text)
            {
                yield return new WaitForSecondsRealtime(delay);
                destination.text += c;
            }

            _isTypeWriting = false;
        }
   
    }
}