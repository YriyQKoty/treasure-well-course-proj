using Singletons;
using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
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
	
	public void Exit_Menu_Event ( )
	{
		SceneManager.UnloadSceneAsync( "Menu_scene" );
		Debug.Log( "вийшли + відновлення гри" );
		GameObject gc = GameObject.Find("GameController");
		gc.GetComponent<GameController>().SetPause( false );
	}
}
