using System;
using System.Collections;
using System.Collections.Generic;
using ObjectsBehaviours;
using UnityEngine;
using UnityEngine.Events;

namespace HealthLogic
{
    /// <summary>
    /// Відповідає за поведінку аптечки
    /// </summary>
    public class BandageOnTouch : MonoBehaviour
    {
        private void Reset()
        {
            //set bandage active on resetting
            this.gameObject.SetActive(true);
        }

        //if player collide with bandage
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //if health is suitable to use bandage
                if (Health.Instance.CurrentHealth < Health.Instance.MAXHealth && Health.Instance.CurrentHealth > 0)
                {
                    Health.Instance.AddHealth();
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}