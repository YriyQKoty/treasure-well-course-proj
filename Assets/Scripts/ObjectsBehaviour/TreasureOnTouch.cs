using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectsBehaviours
{
    /// <summary>
    /// Відповідає за логіку поведінки скарбу
    /// </summary>
    public class TreasureOnTouch : MonoBehaviour
    {
        /// <summary>
        /// Подія
        /// </summary>
        public UnityEvent onTouch;
        
        /// <summary>
        /// Чи відігравати аудіо
        /// </summary>
        public bool playAudioOnTouch = true;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                SendSignal(); //send signal
            }
        }

        private void SendSignal()
        {
            if (playAudioOnTouch)
            {
                var audio = GetComponent<AudioSource>();

                if (audio && audio.gameObject.activeInHierarchy)
                {
                    audio.Play();
                }
            }

            onTouch.Invoke();
        }
    }
}