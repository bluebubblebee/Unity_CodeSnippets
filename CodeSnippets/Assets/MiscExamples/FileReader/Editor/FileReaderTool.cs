using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CodeSnippets.FileReader
{
#if UNITY_EDITOR
    public class FileReaderTool : EditorWindow
    {
        private int currentToolState = 0;


        [MenuItem("Bsgg/Tools/FileReaderTool")]
        public static void OpenCityBuilderTool()
        {
            GetWindow<FileReaderTool>("File Reader Tool");
        }
        private void OnGUI()
        {
            DisplayTitle(" File Reader Tool");


            if (currentToolState == 1)
            {
                DisplayToolReader();
            }

            GUILayout.Space(10.0f);
            if (GUILayout.Button(" Refresh"))
            {
                currentToolState = 1;
            }

        }

        private void DisplayTitle(string title)
        {
            DrawUILine(Color.cyan, 10, 2, 5);

            GUIStyle titleLabelStyle = new GUIStyle();
            titleLabelStyle.alignment = TextAnchor.MiddleCenter;
            titleLabelStyle.fontStyle = FontStyle.Bold;
            titleLabelStyle.fontSize = 18;
            titleLabelStyle.fixedWidth = 500;
            titleLabelStyle.fixedHeight = 20;
            titleLabelStyle.normal.textColor = Color.white;
            GUILayout.Label(title, titleLabelStyle);

            DrawUILine(Color.cyan, 10, 2, 5);
        }

        private void DrawUILine(Color color, int spacing = 10, int thickness = 2, int padding = 10)
        {
            GUILayout.Space(spacing);

            Rect contentRect = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            contentRect.height = thickness;
            contentRect.y = padding / 2;
            contentRect.x -= 2;
            contentRect.width += 6;
            EditorGUI.DrawRect(contentRect, color);

            GUILayout.Space(spacing);
        }

        private GUIStyle GetLabelTitleStyle(int width = 300)
        {
            GUIStyle labelTitleStyle = new GUIStyle();
            labelTitleStyle.fontStyle = FontStyle.Bold;
            labelTitleStyle.fontSize = 12;
            labelTitleStyle.fixedWidth = width;
            labelTitleStyle.normal.textColor = Color.cyan;

            return labelTitleStyle;
        }

        private GUIStyle GetValueTitleStyle(int width = 300)
        {
            GUIStyle labelTitleStyle = new GUIStyle();
            labelTitleStyle.fontStyle = FontStyle.Normal;
            labelTitleStyle.fontSize = 12;
            labelTitleStyle.fixedWidth = width;
            labelTitleStyle.fixedWidth = 20;
            labelTitleStyle.normal.textColor = Color.white;

            return labelTitleStyle;

        }


        private void DisplayToolReader()
        {
            GUILayout.Space(5.0f);

            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            //GUILayout.Label(" Current Wood: ", GetLabelTitleStyle(150));
            //GUILayout.Label(currentWoodRes.ToString(), GetValueTitleStyle(120));

            GUILayout.EndHorizontal();


            if (GUILayout.Button(" Select file to read"))
            {


            }
            
            GUILayout.EndVertical();


        }
    }

#endif
}
