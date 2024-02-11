using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Utility;

namespace CodeSnippets.UIToolkitExamples
{
    public class UIToolkitMain : MonoBehaviour
    {
        [Header("UI Scene")]
        [SerializeField] private UIToolkitViewExample uiViewExample;

        [SerializeField] private GameObject cube1;
        [SerializeField] private GameObject cube2;

        [SerializeField] private Rotate[] shapesRotation;

        [SerializeField] private TestAddressables testAddressable;

        private int points = 0;

        private void Start()
        {
            uiViewExample.OnUIInitialized += OnInitializeCompleted;
            uiViewExample.Initialize();
        }

        private void OnInitializeCompleted()
        {
            uiViewExample.OnUIInitialized -= OnInitializeCompleted;

            Debug.Log($"<color=cyan> Testing UIToolkitMain {GetType().Name} </color>");

            uiViewExample.RestartButton.clicked += RestartButton_onClick;
            uiViewExample.StartTimerButton.clicked += StartTimerButton_onClick;
            uiViewExample.CloseButton.clicked += CloseButton_onClick;
            uiViewExample.TestAddressablesButton.clicked += TestAddressablesButton_onClick;
        }

        private void OnDisable()
        {
            uiViewExample.RestartButton.clicked -= RestartButton_onClick;
            uiViewExample.StartTimerButton.clicked -= StartTimerButton_onClick;
            uiViewExample.CloseButton.clicked -= CloseButton_onClick;
            uiViewExample.TestAddressablesButton.clicked -= TestAddressablesButton_onClick;
        }

        private void RestartButton_onClick()
        {
            points += 1;
            Debug.Log("<color=cyan> RestartButton_onClick called! Points:" + points + "</color>");
            uiViewExample.PointsLabel.text = "Points: " + points;

            uiViewExample.NameTextField.value = "Bea!";
            uiViewExample.NameTextField.label = "Your name:";


            float dotCubes = Vector3.Dot(cube1.transform.forward, cube2.transform.forward);
            uiViewExample.PointsLabel.text = "Dot(cube1, cube2): " + dotCubes;
        }

        private void StartTimerButton_onClick()
        {
            Debug.Log("<color=cyan> StartGameButton_onClick called!</color>");
            Debug.Log("<color=cyan> Calling TimerCoroutine  1</color>");
            timerCoroutine = StartCoroutine(TimerCoroutine());
            Debug.Log("<color=cyan> Calling After TimerCoroutine 5 </color>");
            SortList();
            
            TestShapeRotationTask();
        }

       

        private async void TestShapeRotationTask()
        {
            int delaySeconds = 1;
            await Task.Delay(delaySeconds * 1000);
            Debug.Log("<color=cyan> TestShapeRotationTask Start </color>");

            // Rotate shapes
            Task[] tasks = new Task[shapesRotation.Length];
            for (int i=0; i< shapesRotation.Length; i++)
            {
                float duration = 1.0f + (1.0f * i); 
                tasks[i] = shapesRotation[i].RotateForSeconds(duration);
            }

            // Wait until all task has been completed
            await Task.WhenAll(tasks);

            Debug.Log("<color=cyan> TestShapeRotationTask Task.WhenAll </color>");

            var taskRandomN = await GetRandomNumber(2);
            uiViewExample.MessageLabel.text = "All Rotation Task completed! Random Number: " + taskRandomN;
        }

        async Task<int> GetRandomNumber(int seconds)
        {
            int randomNumber = Random.Range(100, 300);

            await Task.Delay(seconds * 1000);

            return randomNumber;
        }

        private Coroutine timerCoroutine;

        private IEnumerator TimerCoroutine()
        {
            int currentTime = 10;
            uiViewExample.TimerLabelText.text = "Time: " + currentTime;

            Debug.Log("<color=cyan> TimerCoroutine Start 2 </color>");
            yield return new WaitForSeconds(2.0f);
            Debug.Log("<color=cyan> TimerCoroutine After 2 seconds 3</color>");

            while (currentTime >= 0)
            {
                yield return new WaitForSeconds(1.0f);
                currentTime -= 1;

                if (currentTime < 0)
                {
                    currentTime = 0;
                }
                uiViewExample.TimerLabelText.text = "Time: " + currentTime;
            }

            Debug.Log("<color=cyan> TimerCoroutine End 4</color>");
        }

        private async void TimerForSeconds(int totalTime)
        {
            int currentTime = totalTime;
            while (currentTime >= 0)
            {
                await Task.Yield();

                currentTime -= 1;

                if (currentTime < 0)
                {
                    currentTime = 0;
                }
                uiViewExample.TimerLabelText.text = "Time: " + currentTime;
            }
        }

        private void SortList()
        {
            List<int> listNumb = new List<int>();

            listNumb.Add(4);
            listNumb.Add(7);
            listNumb.Add(3);
            listNumb.Add(6);
            listNumb.Add(2);
            listNumb.Add(1);

            string listNumbStr = "Original List: ";
            for (int index = 0; index < listNumb.Count; index++)
            {
                listNumbStr += listNumb[index] + " ";
            }
            Debug.Log("<color=cyan>" + listNumbStr + "</color>");

            listNumb = MathUtility.BubbleSort(listNumb);

            listNumbStr = "Sorted List: ";
            for (int index = 0; index < listNumb.Count; index++)
            {
                listNumbStr += listNumb[index] + " ";
            }
            Debug.Log("<color=cyan>" + listNumbStr + "</color>");
        }


        private void CloseButton_onClick()
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }

            uiViewExample.Root.style.visibility = Visibility.Hidden;

            //uiViewExample.Hide();
        }


        private void TestAddressablesButton_onClick()
        {
            Debug.Log("<color=cyan>" + "TestAddressablesButton_onClick" + "</color>");
            testAddressable.LoadAddressable();
        }
    }
}
