using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CodeSnippets.FileReader
{
#if UNITY_EDITOR
    [CustomEditor(typeof(FileReaderToolManager))]
    public class FileReaderToolEditorManager : Editor
    {
        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            FileReaderToolManager manager = (FileReaderToolManager)target;

            GUILayout.Space(20.0f);

            GUILayout.BeginVertical();

            if (GUILayout.Button("Import CSV File"))
            {
                manager.ImportCSVFile();
            }

            GUILayout.EndVertical();
        }


    }
#endif
}
