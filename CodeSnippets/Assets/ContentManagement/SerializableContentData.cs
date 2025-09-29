using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ContentManagement
{
    [Serializable]
    public class SerializableContentData : ContentData
    {
        [SerializeField]
        private string m_id;

        [SerializeField]
        private string m_name;

        public override string Id 
        {   get => m_id; 
            set => m_id = value; 
        }

        public override string Name
        {
            get => m_name;
            set => m_name = value;
        }
    }
}
