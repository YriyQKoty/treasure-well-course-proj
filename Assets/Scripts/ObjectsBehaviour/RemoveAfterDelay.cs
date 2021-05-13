using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectsBehaviours
{
    /// <summary>
    /// Компонент, що видаляє об'єкт після затримки
    /// </summary>
    public class RemoveAfterDelay : MonoBehaviour
    {
        /// <summary>
        /// Перерва перед видаленням
        /// </summary>
        public float delay;
    
        void Start()
        {
            StartCoroutine(Remove());
        }
    
        IEnumerator Remove()
        {
            yield return new WaitForSeconds(delay);

            Destroy(this.gameObject);
        }
    }
}