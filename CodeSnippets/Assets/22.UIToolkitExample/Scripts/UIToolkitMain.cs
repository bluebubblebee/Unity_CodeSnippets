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

    void Start()
    {
        Debug.Log($"<color=cyan> Testing UIToolkitMain {GetType().Name} </color>");

        uiViewExample.RestartButton.clicked += RestartButton_onClick;
        uiViewExample.StartGameButton.clicked += StartGameButton_onClick; 
    }

    private void OnDisable()
    {
        uiViewExample.RestartButton.clicked -= RestartButton_onClick;
        uiViewExample.StartGameButton.clicked -= StartGameButton_onClick;
    }

    private void RestartButton_onClick()
    {
        points += 1;
        //Debug.Log("<color=cyan> RestartButton_onClick called! Points:" + points + "</color>");

       // uiViewExample.PointsLabel.text = "Points: " + points;

        uiViewExample.NameTextField.value = "Bea!";
        uiViewExample.NameTextField.label = "Your name:";


        float dotCubes = Vector3.Dot(cube1.transform.forward, cube2.transform.forward);
        uiViewExample.PointsLabel.text = "Dot(cube1, cube2): " + dotCubes;
    }

    private void StartGameButton_onClick()
    {
        Debug.Log("<color=cyan> StartGameButton_onClick called!</color>");

        Debug.Log("<color=cyan> Calling TestCoroutine  1</color>");
        StartCoroutine(TestCoroutine());

        Debug.Log("<color=cyan> Calling After TestCoroutine 4 </color>");

        TestSortList();
    }

    private IEnumerator TestCoroutine()
    {
        Debug.Log("<color=cyan> TestCoroutine Start 2 </color>");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("<color=cyan> TestCoroutine End  3</color>");
        uiViewExample.Hide();

    }

    List<int> listNumb;
    private void TestSortList()
    {
        listNumb = new List<int>();

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

        int i = 1;

        for (int current = 1; current < listNumb.Count; current++)
        {
            i = current;
            while ((i > 1) && (listNumb[i - 1] > listNumb[i]))
            {
                if (listNumb[i - 1] > listNumb[i])
                {
                    // Swap
                    int temp = listNumb[i - 1];
                    listNumb[i - 1] = listNumb[i];
                    listNumb[i] = temp;

                    i = i - 1;
                }
            }
        }

        listNumbStr = "Sort List: ";
        for (int index = 0; index < listNumb.Count; index++)
        {
            listNumbStr += listNumb[index] + " ";
        }

        Debug.Log("<color=cyan>" + listNumbStr + "</color>");

    }

}
