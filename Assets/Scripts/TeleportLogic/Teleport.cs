using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeleportLogic
{
	/// <summary>
	/// Відповідає за логіку телепорту
	/// </summary>
	public class Teleport : MonoBehaviour
	{
        /// <summary>
        /// Тип події для телепорту
        /// </summary>
        /// <param name="obj">The object.</param>
        public delegate void TeleportEvent ( GameObject obj );
        /// <summary>
        /// Occurs when [on collision] with teleport.
        /// </summary>
        public event TeleportEvent onCollision;

		public bool collisionFlag = false;

		private void OnTriggerEnter2D ( Collider2D collision )
		{
			if ( !collisionFlag ) { // ще не торкались )
				if ( collision.gameObject.tag == "Player" ) {
					onCollision?.Invoke( gameObject );
				
					collisionFlag = true;
				}
			}
		}
	}
}