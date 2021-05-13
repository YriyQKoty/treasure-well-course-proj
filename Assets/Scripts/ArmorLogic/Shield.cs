using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement;
using UnityEngine;


namespace ArmorLogic
{
	/// <summary>
	/// Описує логіку щита
	/// </summary>
	public class Shield : MonoBehaviour
	{
		public bool isActive;

		// Start is called before the first frame update
		void Start()
		{
			GameController.Instance.createPlayer += new GameController.GnomeEvent( Init_Shield );
		}

		private void Init_Shield ( GameObject obj )
		{
			isActive = true;
			GetComponent<SpriteRenderer>().enabled = true;
			GetComponent<PolygonCollider2D>().isTrigger = false;
		}

		internal void activeOff ( )
		{
			isActive = false;
			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<PolygonCollider2D>().isTrigger = true;
		}
	}
}