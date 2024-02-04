using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIToolkitMain : MonoBehaviour
{

    [Header("UI Scene")]
    [SerializeField] private UIToolkitViewExample uiViewExample;    

    [SerializeField] private GameObject cube1;
    [SerializeField] private GameObject cube2;

    private int points = 0;

    private void OnEnable()
    {
        Debug.Log($"<color=cyan> Testing UIToolkitMain {GetType().Name} </color>");

        uiViewExample.RestartButton.clicked += RestartButton_onClick;
        uiViewExample.StartTimerButton.clicked += StartTimerButton_onClick;
        uiViewExample.CloseButton.clicked += CloseButton_onClick;
    }

    private void OnDisable()
    {
        uiViewExample.RestartButton.clicked -= RestartButton_onClick;
        uiViewExample.StartTimerButton.clicked -= StartTimerButton_onClick;
        uiViewExample.CloseButton.clicked -= CloseButton_onClick;
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
        timerCoroutine =  StartCoroutine(TimerCoroutine());
        Debug.Log("<color=cyan> Calling After TimerCoroutine 5 </color>");
        SortList();
    }

    private Coroutine timerCoroutine;

    private IEnumerator TimerCoroutine()
    {
        int currentTime = 10;
        uiViewExample.TimerLabelText.text = "Time: " + currentTime;

        Debug.Log("<color=cyan> TimerCoroutine Start 2 </color>");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("<color=cyan> TimerCoroutine After 2 seconds 3</color>");
        //uiViewExample.Hide();
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

        for (int i = 0; i < listNumb.Count; i++)
        {
            for (int j = i + 1; j < listNumb.Count; j++)
            {
                if (listNumb[i] > listNumb[j])
                {
                    // Swap
                    int temp = listNumb[i];
                    listNumb[i] = listNumb[j];
                    listNumb[j] = temp;
                }
            }
        }

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

        uiViewExample.Hide();
    }
}
