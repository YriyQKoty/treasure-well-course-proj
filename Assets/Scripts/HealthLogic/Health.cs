using System;
using System.Collections;
using System.Collections.Generic;
using ArmorLogic;
using SingletonTemplate;
using UnityEngine;

namespace HealthLogic
{
    /// <summary>
    /// Відповідає за здоров'я головного персонажа та зв'язок із його шкалою
    /// </summary>
    public class Health : Singleton<Health>
    {
        [SerializeField] private int maxHealth; //variable for maxHealth

        /// <summary>
        /// Gets the maximum health.
        /// </summary>
        /// <value>
        /// The maximum health.
        /// </value>
        public int MAXHealth => maxHealth;

        private int currentHealth; //variable for current health        
        /// <summary>
        /// Gets the current health.
        /// </summary>
        /// <value>
        /// The current health.
        /// </value>
        public int CurrentHealth => currentHealth;

        private GameObject plusOneSprite;

        private bool gnomeIsInvincible;

        //resetting health        
        /// <summary>
        /// Оновлює здоров'я.
        /// </summary>
        public void ResetHealth()
        {
            currentHealth = maxHealth;
            for (int i = 0; i < currentHealth; i++)
            {
                this.transform.GetChild(i).GetChild(0).gameObject.SetActive(true); //activating sprites of health
            }

            plusOneSprite = transform.GetChild(transform.childCount - 1).gameObject;

            plusOneSprite.SetActive(false);
        }


        // getting tagholder of a collided object        
        /// <summary>
        /// Забирає поділки здоров'я.
        /// </summary>
        /// <param name="tagHolder">Зберігач тегу.</param>
        public void RemoveHealthUnit(GameObject tagHolder)
        {
            if (!gnomeIsInvincible && !ArmorController.Instance.ARMOR)
            {
                if (currentHealth > 0)
                {
                    if (tagHolder.CompareTag("MainParts")) //if main part
                    {
                        if (currentHealth == 1)
                        {
                            GameObject.Find($"HealthUnit ({currentHealth - 1})").gameObject.SetActive(false);
                            currentHealth -= 1;
                            return;
                        }

                        GameObject.Find($"HealthUnit ({currentHealth - 1})").gameObject.SetActive(false);
                        GameObject.Find($"HealthUnit ({currentHealth - 2})").gameObject.SetActive(false);
                        currentHealth -= 2;
                    }
                    else if (tagHolder.CompareTag("SideParts")) //if side
                    {
                        GameObject.Find($"HealthUnit ({currentHealth - 1})").gameObject.SetActive(false);
                        currentHealth -= 1;
                    }

                    StartCoroutine(InvincibleSecond());
                }
            }
        }

        // medkit taken        
        /// <summary>
        /// Додає здоров'я.
        /// </summary>
        public void AddHealth()
        {
            if (currentHealth < maxHealth)
            {
                StartCoroutine(ShowPlusOneHealthUnit());
                this.transform.GetChild(currentHealth).GetChild(0).gameObject.SetActive(true);
                currentHealth += 1;
            }
        }

        IEnumerator ShowPlusOneHealthUnit()
        {
            plusOneSprite.SetActive(true);
            yield return new WaitForSeconds(5f);
            plusOneSprite.SetActive(false);
        }

        IEnumerator InvincibleSecond()
        {
            gnomeIsInvincible = true;
            yield return new WaitForSecondsRealtime(0.75f);
            gnomeIsInvincible = false;
        }
    }
}