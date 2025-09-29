using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace CodeSnippets.FileReader
{
    public class TransactionEntry
    {
        public string key;
        public string transaction;

    }

    public class CSVImporter : MonoBehaviour
    {
        //public List<TransactionEntry> transactionList { get; private set; }

        public static List<TransactionEntry> GetData(string path, string splitStr = ", ")
        {
            List<TransactionEntry> data = new List<TransactionEntry>();
            if (path == "")
            {
                return data;
            }

            string csvFile = File.ReadAllText(path, Encoding.UTF8);

            StringReader reader = new StringReader(csvFile);
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] items = line.Split(splitStr.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
                TransactionEntry entry = new TransactionEntry();
                if (items.Length >= 2)
                {                    
                    entry.key = items[0];
                    entry.transaction = items[1];
                    data.Add(entry);
                }                
            }

            return data;
        }





        private Encoding ReturnEncoding(string encoding)
        {
            Encoding returnValue;
            switch (encoding)
            {
                case "ASCII":
                case "ascii":
                    returnValue = System.Text.Encoding.ASCII;
                    break;
                case "Unicode":
                case "UTF16":
                case "UTF-16":
                    returnValue = System.Text.Encoding.Unicode;
                    break;
                case "UTF32":
                case "UTF-32":
                    returnValue = System.Text.Encoding.UTF32;
                    break;
                case "UTF8":
                case "UTF-8":
                    returnValue = System.Text.Encoding.UTF8;
                    break;
                case "UTF7":
                case "UTF-7":
                    returnValue = System.Text.Encoding.UTF7;
                    break;
                case "BigEndianUnicode":
                    returnValue = System.Text.Encoding.BigEndianUnicode;
                    break;
                default:
                    returnValue = System.Text.Encoding.UTF8;
                    break;
            }
            return returnValue;
        }
    }
}
