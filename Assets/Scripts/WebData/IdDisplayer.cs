using UnityEngine;
using UnityEngine.UI;

public class IdDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject IdBox;
    [SerializeField] private Text _text;

    [SerializeField] private GameObject StartGameObject;
    [SerializeField] private GameObject FinishGameObject;
    // private void Awake()
    // {
    //     IdBox.SetActive(true);
    // }

    public void DeactivateBox()
    {
        IdBox.SetActive(false);
        Application.OpenURL("");
        ActivateMainButtons();
    }

    private void ActivateMainButtons()
    {
        StartGameObject.SetActive(true);
        FinishGameObject.SetActive(true);
    }
}
