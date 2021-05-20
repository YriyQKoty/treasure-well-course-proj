using Singletons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuVictory : Menu
{
	public Button Main = null;

	// Start is called before the first frame update
	void Start()
    {
		if ( Main == null ) {
			MenuController.ErrorMenu( "null" );
		} else {
			Main.onClick.AddListener( Main_menu );
		}
    }

	public void OnVictoryChangeLevel()
	{
		if (SceneManager.GetActiveScene().buildIndex == 3)
		{
			MenuController.Instance.LoadLevel(0);
			BackMenu( MenuController.MenuType.Main, gameObject );
		}
		else
		{
			MenuController.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

	/// <summary>
	/// обробник натиснення на кнопку main меню
	/// </summary>
	private void Main_menu ( )
	{
		
	}	
}
