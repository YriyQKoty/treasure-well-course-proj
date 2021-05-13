using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArmorLogic
{
	/// <summary>
	/// Відповідає за поведінку броні
	/// </summary>
	public class Armor : MonoBehaviour
	{
		private bool importantPiece = false; //чи важлива частина тіла

		private int damage = 0;

		// Start is called before the first frame update
		void Start ( )
		{
			if ( gameObject.transform.parent.gameObject.name == "ArmHoldEmpty" ||
			     gameObject.transform.parent.gameObject.name == "LegRope" ) {
				importantPiece = true;
			}
		}

		/// <summary>
		/// Активуємо броню
		/// </summary>
		public int ArmorActive ( )
		{
			damage = 2;
			gameObject.GetComponent<SpriteRenderer>().enabled = true;

			return damage;
		}

		/// <summary>
		/// Надати шкоди броні
		/// </summary>
		internal int ArmorDeactive ( )
		{
			if ( importantPiece ) {
				damage = 2;
			} else {
				damage = 1;
			}

			return damage;
		}
	}
}