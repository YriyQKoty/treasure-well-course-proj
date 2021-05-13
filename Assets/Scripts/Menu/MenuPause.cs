using Singletons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : Menu
{
	public Button Main = null;
	
	void Start()
    {
		if ( Main == null ) {
			MenuController.ErrorMenu( "null" );
		} else {
			Main.onClick.AddListener( Main_menu );
		}
    }

	/// <summary>
	/// обробник натиснення на кнопку main меню
	/// </summary>
	private void Main_menu ( )
	{
		BackMenu( MenuController.MenuType.Main, gameObject );
	}	
}
