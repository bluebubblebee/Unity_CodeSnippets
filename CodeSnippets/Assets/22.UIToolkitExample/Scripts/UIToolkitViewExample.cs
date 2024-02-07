using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeSnippets.UIToolkitExamples
{
    public class UIToolkitViewExample : MonoBehaviour
    {
        [SerializeField] private UIDocument document;

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

        public Label TimerLabelText
        {
            get
            {
                return uiDocument.rootVisualElement.Query<Label>("TimerLabel");
            }
        }

        public Label MessageLabel
        {
            get
            {
                return uiDocument.rootVisualElement.Query<Label>("MessageLabel");
            }
        }

        public Button RestartButton
        {
            get
            {
                return uiDocument.rootVisualElement.Query<Button>("RestartButton");
            }
        }

        public Button StartTimerButton
        {
            get
            {
                return uiDocument.rootVisualElement.Query<Button>("StartTimerButton");
            }
        }

        public Button CloseButton
        {
            get
            {
                return uiDocument.rootVisualElement.Query<Button>("CloseUIButton");
            }
        }



        [SerializeField] private UIDocument uiDocument;

        private void OnEnable()
        {
            Debug.Log($"<color=cyan> Testing UIToolkitViewExample {GetType().Name} </color>");

            RestartButton.RegisterCallback<NavigationSubmitEvent>(RestartButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
            StartTimerButton.RegisterCallback<NavigationSubmitEvent>(StartTimerButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
            CloseButton.RegisterCallback<NavigationSubmitEvent>(CloseButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
        }

        private void OnDisable()
        {
            RestartButton.UnregisterCallback<NavigationSubmitEvent>(RestartButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
            StartTimerButton.UnregisterCallback<NavigationSubmitEvent>(StartTimerButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
            CloseButton.UnregisterCallback<NavigationSubmitEvent>(CloseButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
        }

        private void Start()
        {
            var root = document.rootVisualElement;
        }


        private void RestartButton_NavigationSubmitEvent(NavigationSubmitEvent submitEvent)
        { }

        private void StartTimerButton_NavigationSubmitEvent(NavigationSubmitEvent submitEvent)
        { }

        private void CloseButton_NavigationSubmitEvent(NavigationSubmitEvent submitEvent)
        { }


        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

    }
}
