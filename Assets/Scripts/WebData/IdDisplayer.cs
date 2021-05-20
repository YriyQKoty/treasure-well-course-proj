using System.Collections;
using System.Collections.Generic;
using Singletons;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class IdDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject IdBox;
    [SerializeField] private Text _text;

    [SerializeField] private GameObject StartGameObject;
    [SerializeField] private GameObject FinishGameObject;
    private void Awake()
    {
        IdBox.SetActive(true);
    }

    public void DeactivateBox()
    {
        IdBox.SetActive(false);
        ActivateMainButtons();
    }

    private void ActivateMainButtons()
    {
        StartGameObject.SetActive(true);
        FinishGameObject.SetActive(true);
    }
}
