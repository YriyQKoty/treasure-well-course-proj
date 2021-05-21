using Singletons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class MenuMain : Menu
{
    // кнопка вибору меню
    public Button Level_button;

    private static int _increment = 0;

    // кнопка виходу зі гри
    public Button Exit_app_button;

    // кнопка налаштувань
    public Button Settings_button;
    
    void Start()
    {
        if (Level_button == null || Exit_app_button == null)
        {
            MenuController.ErrorMenu("NULL resource menu");
        }
        else
        {
            Level_button.onClick.AddListener(ChoiseLevel);
            //Exit_app_button.onClick.AddListener(Exit_app);
            //Settings_button.onClick.AddListener(Settings);
        }
    }

    private void Settings()
    {
        BackMenu(MenuController.MenuType.Settings, gameObject);
    }

    // вибор рівня меню
    private void ChoiseLevel()
    {
        BackMenu(MenuController.MenuType.Level, gameObject);
    }

    /// <summary>
    /// виходимо з меню
    /// </summary>
    private void Exit_app()
    {
        if (_increment < 2)
        {
            MenuController.Instance.LoadRandomLevel();

            if (_increment == 0)
            {
                MenuController.Instance.sceneLoadingOperation.allowSceneActivation = true;
            }
            else if (_increment == 1)
            {
                var egg = this.gameObject.transform.GetChild(4).gameObject;

                if (egg != null)
                {
                    StartCoroutine(ShowEgg(egg));
                }
            }
        }
        else
        {
            Settings_button.gameObject.SetActive(false);
            Level_button.gameObject.SetActive(false);
            Exit_app_button.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Exit_app_button.gameObject.GetComponent<Image>().enabled = false;
            Exit_app_button.gameObject.GetComponent<Button>().enabled = false;
            _increment = -1;
        }

        _increment++;
    }

    /// <summary>
    /// вихід зі гри
    /// </summary>
    public void ExitApplication()
    {
        Application.Quit();
        Debug.Log("4124");
    }

    /// <summary>
    /// Повертаємось до меню
    /// </summary>
    public void BackToMenu()
    {
        //enabling main menu objects
        Exit_app_button.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Level_button.gameObject.SetActive(true);
        Settings_button.gameObject.SetActive(true);
        Exit_app_button.gameObject.GetComponent<Image>().enabled = true;
        Exit_app_button.gameObject.GetComponent<Button>().enabled = true;
    }

    /// <summary>
    /// Співпроцес для пасхального івенту
    /// </summary>
    /// <param name="egg"></param>
    /// <returns></returns>
    private IEnumerator ShowEgg(GameObject egg)
    {
        var destination = egg.gameObject.transform.GetChild(0).GetComponent<Text>();

        var text = destination.text;

        destination.text = "";

        var typeWriter = egg.GetComponent<UiTextTypeWriter>();
            
        egg.SetActive(true);
        
        yield return new WaitForSecondsRealtime(1.5f);

        typeWriter.StartTypeWriting(destination, text);
        
        yield return new WaitForSecondsRealtime(4f);
        
        typeWriter.StopTypeWriting();
        
        egg.SetActive(false);

        MenuController.Instance.sceneLoadingOperation.allowSceneActivation = true;
    }
}