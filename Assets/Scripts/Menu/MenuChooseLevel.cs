using Singletons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuChooseLevel : Menu
{
	/// <summary>
	/// картинки для вибору рівня
	/// </summary>
	public Sprite [] levelSprite = new Sprite [ 9 ];

	/// <summary>
	/// об'єкт контенту для кнопок
	/// </summary>
	public GameObject levelBottom = null;

	// Start is called before the first frame update
	void Start()
    {
		if ( levelBottom == null ) {
			MenuController.ErrorMenu( "null" );
		} else {
			Init_MenuLevel();
		}
    }

	protected override void Exit_Menu_Event ( )
	{
		BackMenu( MenuController.MenuType.Main, gameObject );
	}

	/// <summary>
	/// розташування кнопок для вибору рівня
	/// </summary>
	private void Init_MenuLevel ( )
	{
		float x = levelBottom.transform.position.x;
		float y = levelBottom.transform.position.y ;
		int count = 1;

		for ( int i = 0; i < levelSprite.Length; i++ ) {

			// init button
			GameObject levelBottom_new = ( GameObject ) Instantiate( levelBottom, levelBottom.transform.position, Quaternion.identity );

			//выравнивание
			RectTransform rt = levelBottom_new.GetComponent<RectTransform>();

			if ( count == 3 ) {  // крайній правий стовпчик
				x = 200f;
				rt.anchorMin = new Vector2( 1, 0.5f );
				rt.anchorMax = new Vector2( 1, 0.5f );
			} else
			if ( count == 2 ) { // середній
				x = 0f;
				rt.anchorMin = new Vector2( 0.5f, 0.5f );
				rt.anchorMax = new Vector2( 0.5f, 0.5f );
			} else
			if ( count == 1 ) { // лівий
				x = -200f;
				rt.anchorMin = new Vector2( 0, 0.5f );
				rt.anchorMax = new Vector2( 0, 0.5f );
			}

			if ( i == 0 ) {
				rt.anchorMin = new Vector2( 0, 1f );
				rt.anchorMax = new Vector2( 0, 1f );
			}

			if ( i == 1 ) {
				rt.anchorMin = new Vector2( 0.5f, 1f );
				rt.anchorMax = new Vector2( 0.5f, 1f );
			}

			if ( i == 2 ) {
				rt.anchorMin = new Vector2( 1f, 1f );
				rt.anchorMax = new Vector2( 1f, 1f );
			}

			if ( i == levelSprite.Length - 4 ) {
				rt.anchorMin = new Vector2( 0, 0 );
				rt.anchorMax = new Vector2( 0, 0 );
			}

			if ( i == levelSprite.Length - 3 ) {
				rt.anchorMin = new Vector2( 0.5f, 0 );
				rt.anchorMax = new Vector2( 0.5f, 0 );
			}

			if ( i == levelSprite.Length - 2 ) {
				rt.anchorMin = new Vector2( 1f, 0 );
				rt.anchorMax = new Vector2( 1f, 0 );
			}

			//------------------------------------

			levelBottom_new.transform.SetParent( levelBottom.transform.parent );
			levelBottom_new.transform.localScale = levelBottom.transform.localScale;
			levelBottom_new.transform.localPosition = new Vector2( x, levelBottom.transform.localPosition.y + y);
			levelBottom_new.transform.GetChild( 0 ).gameObject.GetComponent<Image>().sprite = levelSprite [ i ];
			levelBottom_new.name = levelBottom.name + i;
			levelBottom_new.GetComponent<LevelButton>().level = i;
			levelBottom_new.SetActive( true );

			if ( i != levelSprite.Length - 1 ) {
				count++;
			} 

			if ( count > 3 ) {
				count = 1;
				y -= 200f;
			}

			levelBottom_new.GetComponent<Button>().onClick.AddListener( ( ) => ChoiceLevel( levelBottom_new ) );
		}
	}

	/// <summary>
	/// Користувач обрав рівень
	/// </summary>
	private void ChoiceLevel ( GameObject sender )
	{
		//Debug.Log( sender.GetComponent<LevelButton>().level );
		MenuController.Instance.LoadLevel( 1 );
	}
}
