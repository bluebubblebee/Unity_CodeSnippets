using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CodeSnippets.FileReader
{
    public class FileReaderToolManager : MonoBehaviour
    {
        public void ImportCSVFile()
        {
            string filePath = EditorUtility.OpenFilePanel("CSV To import", "", "csv");

            List<TransactionEntry> data = CSVImporter.GetData(filePath, ",");


        }
    }
}
