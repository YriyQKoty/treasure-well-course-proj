using System;
using System.Collections;
using System.Collections.Generic;
using ArmorLogic;
using GameManagement;
using HealthLogic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ObjectsBehaviours
{
	[RequireComponent(typeof(Collider2D))]
	public class SignalOnTouch : MonoBehaviour
	{
		public UnityEvent onTouch;

		private GameObject player; //finding player with tag

		private float distance;

		private GameObject tagHolder;
    
		public bool playAudioOnTouch = true;

		//event func for getting treasure
		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.gameObject.CompareTag("Player"))
			{
				SendSignal();
			}
		}

		//event func for detecting collision between traps and gnome
		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Player")) {

				if ( gameObject.tag == "Shield" ) {
					if ( gameObject.GetComponent<Shield>().isActive ) {
						gameObject.GetComponent<Shield>().activeOff();
						ArmorController.Instance.CollisionArmor( true, collision.gameObject ); // увімкнемо броню
					}
				} else {
					ArmorController.Instance.CollisionArmor( false, collision.gameObject ); // вимкнемо броню

					tagHolder = collision.gameObject.transform.Find( "TagHolder" ).gameObject; //отримує тегхолдер

					if ( tagHolder != null ) {

						if ( GameController.Instance.body == null ) {
							GameController.Instance.body = GameObject.Find( "Body" );
						}

						GameController.Instance.body.GetComponent<Rigidbody2D>().AddForce( ( GameController.Instance.body.transform.position - this.gameObject.transform.position ).normalized * 1000, ForceMode2D.Impulse );

						if (!GameController.Instance.IsInvincible)
						{
							Health.Instance.RemoveHealthUnit( tagHolder ); //зменшуємо здоров'я
						}
					}
				}
				SendSignal();

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