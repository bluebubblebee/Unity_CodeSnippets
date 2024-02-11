using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeSnippets.UIToolkitExamples
{
    public class UIToolkitViewExample : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;

        public delegate void OnUIViewInitialized();
        public OnUIViewInitialized OnUIInitialized;

        public VisualElement Root
        {
            get
            {
                return uiDocument.rootVisualElement;
            }
        }

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
            get; private set;
        }

        public Button StartTimerButton
        {
            get; private set;
        }

        public Button CloseButton
        {
            get; private set;            
        }

        public Button TestAddressablesButton
        {
            get; private set;
        }


        public void Initialize()
        {
            SetUIReferences();

            RegisterEvents();

            OnUIInitialized?.Invoke();
        }

        private void SetUIReferences()
        {
            StartTimerButton = uiDocument.rootVisualElement.Query<Button>("StartTimerButton");
            RestartButton = uiDocument.rootVisualElement.Query<Button>("RestartButton");
            CloseButton = uiDocument.rootVisualElement.Query<Button>("CloseUIButton");
            TestAddressablesButton = uiDocument.rootVisualElement.Query<Button>("TestAddressabeButton");
        }

        private void RegisterEvents()
        {
            Debug.Log($"<color=cyan> Testing UIToolkitViewExample {GetType().Name} </color>");

            if (StartTimerButton != null)
            {
                StartTimerButton.RegisterCallback<NavigationSubmitEvent>(StartTimerButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
            }

            if (RestartButton != null)
            {
                RestartButton.RegisterCallback<NavigationSubmitEvent>(RestartButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
            }          

            if (CloseButton != null)
            {
                CloseButton.RegisterCallback<NavigationSubmitEvent>(CloseButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
            }

            if (TestAddressablesButton != null)
            {
                TestAddressablesButton.RegisterCallback<NavigationSubmitEvent>(TestAddressablesButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
            }


        }

        private void OnDisable()
        {
            RestartButton.UnregisterCallback<NavigationSubmitEvent>(RestartButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
            StartTimerButton.UnregisterCallback<NavigationSubmitEvent>(StartTimerButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
            CloseButton.UnregisterCallback<NavigationSubmitEvent>(CloseButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
        }

        private void RestartButton_NavigationSubmitEvent(NavigationSubmitEvent submitEvent)
        { }

        private void StartTimerButton_NavigationSubmitEvent(NavigationSubmitEvent submitEvent)
        { }

        private void CloseButton_NavigationSubmitEvent(NavigationSubmitEvent submitEvent)
        { }

        private void TestAddressablesButton_NavigationSubmitEvent(NavigationSubmitEvent submitEvent)  { }

        


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
