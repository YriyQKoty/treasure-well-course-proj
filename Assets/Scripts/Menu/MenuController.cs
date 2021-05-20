using System.Collections.Generic;
using UnityEngine;
using SingletonTemplate;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
using Utilities;
using Random = UnityEngine.Random;

namespace Singletons
{
	public class MenuController : Singleton<MenuController>
	{
		/// <summary>
		/// Назва меню, яке потрібно актививувати
		/// </summary>
		public enum MenuType
		{
			Main,
			Settings,
			Pause,
			Level,
			Victory,
			None
		}

		public AsyncOperation sceneLoadingOperation;

		public static MenuType MENU_NAME = MenuType.None;
		
		public string[] sceneLevels;

		private int _randomIndex;


		/// <summary>
		/// параметр активності ADS
		/// </summary>
		public static bool ADS = true;

		/// <summary>
		/// Параметр активності музики
		/// </summary>
		public static bool MUSIC = true;

		public GameObject Main;
		public GameObject Settings;
		public GameObject Pause;
		public GameObject Level;
		public GameObject Victory;


		public SceneManager[] scenes;

		private void Start ( )
		{
			if ( Main != null && Settings != null && Pause != null && Level != null ) {
				Main.SetActive( false );
				Settings.SetActive( false );
				Pause.SetActive( false );
				Level.SetActive(false);
		
				scenes = new SceneManager [ sceneLevels.Length ];
				InitMenuType();
			} else {
				ErrorMenu( "Ініціалізуй посилання на об'єкти!" );
			}
		}

		// ініціалізуємо тип меню
		public void InitMenuType ( )
		{
			GameObject objMenu = null;
			if ( MENU_NAME == MenuType.None ) {

				objMenu = Main;

			} else {

				switch ( MENU_NAME ) {
					case MenuType.Main:
						objMenu = Main;
						break;
					case MenuType.Settings:
						objMenu = Settings;
						break;
					case MenuType.Pause:
						objMenu = Pause;
						break;
					case MenuType.Level:
						objMenu = Level;
						break;
					case MenuType.Victory:
						objMenu = Victory;
						break;
					default:
						objMenu = Main;
						break;
				}
			}

			if ( objMenu != null ) {
				objMenu.SetActive( true );
				//objMenu.GetComponent<Menu>().Init_Menu();
			}
		}

		/// <summary>
		///  Відкриває меню типу
		/// </summary>
		public static void OpenGameMenu ( MenuType type ) {
			bool flagLoad = true;
			string nameMenuScene = "Menu_scene";
			for ( int i = 0; i < SceneManager.sceneCountInBuildSettings; i++ ) {
				if ( SceneManager.GetSceneByBuildIndex( i ).name == nameMenuScene ) {
					flagLoad = false;
					return;
				}
			}

			if ( flagLoad ) {
				MENU_NAME = type;
				LoadSceneParameters param = new LoadSceneParameters(LoadSceneMode.Additive);
				Scene scene = SceneManager.LoadScene(nameMenuScene, param);

			}
		}

		public static void ErrorMenu ( string e )
		{
			Debug.Log( "ErrorMenu :" + e );
		}
		
		/// <summary>
		///  Метод завантажує випадкову сцену
		/// </summary>
		public void LoadRandomLevel()
		{
			_randomIndex = Random.Range( 0, sceneLevels.Length );
			LoadLevel( _randomIndex );
			sceneLoadingOperation.allowSceneActivation = false;
		}


		/// <summary>
		/// Завантажити рівень по індексу
		/// </summary>
		/// <param name="i"></param>
		public void LoadLevel ( int i )
		{
			SceneManager.LoadSceneAsync(i);
			
			// if ( sceneLevels.Length > i ) {
			// 	sceneLoadingOperation = SceneManager.LoadSceneAsync( sceneLevels [ i ]);
			// 	sceneLoadingOperation.allowSceneActivation = true;
			// 	Helper.SceneLevel = i;
			// }
		}

	}
}