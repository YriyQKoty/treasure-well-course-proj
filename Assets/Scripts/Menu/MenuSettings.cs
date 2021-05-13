using Singletons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : Menu
{
	[Space (height:10f)]

	// ads
	[Header("ADS")]
	public Button Ads = null;
	public Sprite AdsOff = null;
	public Sprite AdsOn = null;
	public GameObject Ads_icon = null;
	public Sprite AdsOff_icon = null;
	public Sprite AdsOn_icon = null;

	[Space (height:10f)]

	// music
	[Header("MUSIC")]
	public Button Music = null;
	public Sprite MusicOn = null;
	public Sprite MusicOf = null;
	public GameObject Music_icon = null;
	public Sprite MusicOn_icon = null;
	public Sprite MusicOff_icon = null;

	// Start is called before the first frame update
	void Start()
    {
		if ( Ads == null || AdsOff == null || AdsOn == null || AdsOff_icon == null || AdsOn_icon == null || Ads_icon == null||
			Music == null || MusicOf == null || MusicOn == null || MusicOff_icon == null || MusicOn_icon == null || Music_icon == null) {
			MenuController.ErrorMenu( "null" );
		} else {
			Music.onClick.AddListener( Music_menu );
			Ads.onClick.AddListener( Ads_menu );

			Music_menu_settings( true );
			Ads_menu_settings( true );
		}
    }

	#region ADS

	/// <summary>
	/// обробник кліку по кнопки
	/// </summary>
	private void Ads_menu ( )
	{
		Ads_menu_settings( !MenuController.ADS );
	}

	private void Ads_menu_settings ( bool value )
	{
		if ( value ) {
			Ads.GetComponent<Image>().sprite = AdsOn;
			Ads_icon.GetComponent<Image>().sprite = AdsOn_icon;
		} else {
			Ads.GetComponent<Image>().sprite = MusicOf;
			Ads_icon.GetComponent<Image>().sprite = AdsOff_icon;
		}

		MenuController.ADS = value;
	}
	#endregion

	#region Music

	/// <summary>
	/// обработчик клика
	/// </summary>
	private void Music_menu ( )
	{
		Music_menu_settings( !MenuController.MUSIC );
	}

	private void Music_menu_settings ( bool value ) {
		if ( value ) {
			Music.GetComponent<Image>().sprite = MusicOn;
			Music_icon.GetComponent<Image>().sprite = MusicOn_icon;
		} else {
			Music.GetComponent<Image>().sprite = MusicOf;
			Music_icon.GetComponent<Image>().sprite = MusicOff_icon;
		}

		MenuController.MUSIC = value;
	}
	#endregion


	protected override void Exit_Menu_Event ( )
	{
		BackMenu( MenuController.MenuType.Main, gameObject );
	}
}
