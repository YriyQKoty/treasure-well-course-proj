using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement;
using GnomeBehaviour;
using UnityEngine;

namespace TeleportLogic
{
	/// <summary>
	/// Клас відповідає за поведінку телепортації гнома
	/// </summary>
	public class TeleportController : MonoBehaviour
	{
		private GameController GC = null;
		private Gnome Gnome = null;


		// телепорт-об'єкти
		private Teleport [] teleports_obj;


		void Start ( )
		{
			Init_Teleport();
		}

		/// <summary>
		/// Ініціюємо основні об'єкти телепорту
		/// </summary>
		public void Init_Teleport ( )
		{
			GC = GameObject.Find( "GameController" ).gameObject.GetComponent<GameController>();
			if ( GC == null ) {
				Error( " null GameController object" );
			} else {
				SetObjectTeleport();
			}
		}

		private void SetObjectTeleport ( )
		{
			teleports_obj = transform.GetComponentsInChildren<Teleport>();

			if ( teleports_obj.Length == 0 ) {
				Error( "додайте об'єкти телепорту - об'єкт телепорт має бути дочірнім до цього об'єкту" );
			} else {
				for ( int i = 0; i < teleports_obj.Length; i++ ) {
					teleports_obj[i].onCollision += new Teleport.TeleportEvent( CollisionGnome );
				}
			}
		}

		/// <summary>
		/// Скидаємо значення телепорта
		/// </summary>
		/// <param name="teleport_obj"></param>
		public IEnumerator RevertTeleportCollision ( bool value, int i_ )
		{
			if ( !value ) {
				// дать невразливість !!!!!!!!!!!!!!!!
				yield return new WaitForSeconds( 1.5f );
				// забрати невразливість
			}
			for ( int i = 0; i < teleports_obj.Length - 1; i++ ) {
				if ( i != i_ ) {
					teleports_obj [ i ].collisionFlag = value;
				}
			}
		}



		/// <summary>
		/// Гравець увійшов у телепорт
		/// </summary>
		/// <param name="teleport_obj_collision">Об'єкт телепорта, у який увійшов гравець</param>
		public void CollisionGnome ( GameObject teleport_obj_collision ) {
			for ( int i = 0; i < teleports_obj.Length - 1; i++ ) {
				if ( teleports_obj [ i ].gameObject.name == teleport_obj_collision.name ) {

					StartCoroutine( RevertTeleportCollision( true, -1 ) );

					if ( Gnome == null ) {
						Gnome = GC.current;
					}
					Gnome.gameObject.transform.position = new Vector2( 0, teleports_obj [ i + 1 ].gameObject.transform.position.y );

					//delete old rope display
					GC.rope.RemoveRope(Mathf.Abs(teleports_obj [ i + 1 ].gameObject.transform.position.y) + GC.rope.whatTheRopeIsConnectedTo.transform.position.y);
					//detach rope connection
					var connected = GC.rope.whatIsHangingFromTheRope;
					GC.rope.whatIsHangingFromTheRope = null;
					//teleport gnome

					//attach rope connection
					GC.rope.whatIsHangingFromTheRope = connected;
				
					//reset length
					GC.rope.ResetLength();

					StartCoroutine( RevertTeleportCollision( false, i + 1 ) );
				
					return;
				}
			}
		}

		public void Error ( string e ) {
			Debug.Log("Error" + e);
		}
	}
}