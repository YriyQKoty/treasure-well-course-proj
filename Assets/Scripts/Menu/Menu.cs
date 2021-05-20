using Singletons;
using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

public class Menu : MonoBehaviour
{	
	/// <summary>
	/// кнопка виходуи з меню
	/// </summary>
	public Button button_exit_menu = null;


	/// <summary>
	/// метод виходу з меню - додаткові сценарії
	/// </summary>
	protected virtual void Exit_Menu_Event ( )
	{
		SceneManager.UnloadSceneAsync( "Menu_scene" );
		Debug.Log( "вийшли + відновлення гри" );
		GameObject gc = GameObject.Find("GameController");
		gc.GetComponent<GameController>().SetPause( false );
	}

	/// <summary>
	/// повертає з меню
	/// </summary>
	/// <param name="type">Тип меню</param>
	/// <param name="obj"></param>
	public void BackMenu ( MenuController.MenuType type, GameObject obj ) {
		MenuController.MENU_NAME = type;
		GameObject menuController = GameObject.Find("MenuManager");
		menuController.GetComponent<MenuController>().InitMenuType();
		obj.SetActive( false );

		//
			if ( Helper.SceneLevel > -1 && MenuController.Instance.sceneLevels.Length > 0 ) {
			try {
				SceneManager.UnloadSceneAsync( MenuController.Instance.sceneLevels [ Helper.SceneLevel ] );
			} catch { }
			Helper.SceneLevel = -1;
			}
		//
	}
}
