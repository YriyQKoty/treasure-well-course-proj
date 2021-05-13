using SingletonTemplate;
using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement;
using UnityEngine;

namespace ArmorLogic
{
	/// <summary>
	/// Контроллер логіки броні
	/// </summary>
	public class ArmorController : Singleton<ArmorController>
	{
		// частини броні
		private Armor [] armors;

        // броня		
        /// <summary>
        /// Вмк/вимк броню
        /// </summary>
        public bool ARMOR;

        /// <summary>
        /// Очки захисту
        /// </summary>
        public int defencePoints;

		private bool gnomeIsInvincible; // гном безсмертний?

		private void Start ( )
		{
			GameController.Instance.createPlayer += new GameController.GnomeEvent(Init_armor);
		}

		/// <summary>
		/// Ініціалізація об'єктів
		/// </summary>
		/// <param name="objSender"></param>
		private void Init_armor ( GameObject objSender )
		{
			ARMOR = false;
			armors = objSender.GetComponentsInChildren<Armor>();
			defencePoints = 0;
			gnomeIsInvincible = false;
		}

		/// <summary>
		///  Відбулось зіштовхнення із бронею
		/// </summary>
		/// <param name="sender">Ініціатор зіштовхнення</param>
		public void CollisionArmor ( bool value, GameObject sender )
		{
			for ( int i = 0; i < armors.Length; i++ ) {
				if ( value ) { // вмикаємо броню
					ARMOR = true;
					defencePoints = defencePoints + armors [ i ].GetComponent<Armor>().ArmorActive();
				} else { // вимикаємо
					if ( !gnomeIsInvincible && armors [ i ].transform.parent.name == sender.name ) {
						if ( defencePoints > 0 ) {
							defencePoints = defencePoints - armors [ i ].GetComponent<Armor>().ArmorDeactive();
						}

						if ( defencePoints <= 0 ) {
							ARMOR = false;
							for ( int i_ = 0; i_ < armors.Length; i_++ ) {
								armors [ i_ ].GetComponent<SpriteRenderer>().enabled = false;
							}
						}
						StartCoroutine( InvincibleSecond() );
					}
				
			
			
				}
			}
		}

		private IEnumerator InvincibleSecond ( )
		{
			gnomeIsInvincible = true;
			yield return new WaitForSeconds( 0.5f );
			gnomeIsInvincible = false;
		}
	}
}