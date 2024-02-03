using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIToolkitViewExample : MonoBehaviour
{
    

    public Label PointsLabel
    {
        get
        {
            return uiDocument.rootVisualElement.Query<Label>("pointsLabel");
        }
    }

    public TextField NameTextField
    {
        get
        {
            return uiDocument.rootVisualElement.Query<TextField>("txtName");
        }
    }

    public VisualElement GameView
    {
        get
        {
            return uiDocument.rootVisualElement.Query<VisualElement>("StartGameView");
        }
    }

    public Button RestartButton
    {
        get
        {
            if (uiDocument != null)
            {
                return uiDocument.rootVisualElement.Query<Button>("RestartButton");
            }else
            {
                return null;
            }  
        }
    }

    public Button StartGameButton
    {
        get
        {
            return uiDocument.rootVisualElement.Query<Button>("StartGameButton");
        }
    }


    public VisualElement Content
    {
        get
        {
            return uiDocument.rootVisualElement.Query<VisualElement>("Content");
        }
    }

    



    [SerializeField] private UIDocument uiDocument;

    void Start()
    {
        Debug.Log($"<color=cyan> Testing UIToolkitViewExample {GetType().Name} </color>");

        RestartButton.RegisterCallback<NavigationSubmitEvent>(RestartButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
        StartGameButton.RegisterCallback<NavigationSubmitEvent>(StartGameButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
    }


    private void RestartButton_NavigationSubmitEvent(NavigationSubmitEvent submitEvent)
    {}

    private void StartGameButton_NavigationSubmitEvent(NavigationSubmitEvent submitEvent)
    {}

    public void Hide()
    {

        this.gameObject.SetActive(false);

    }
        

}
