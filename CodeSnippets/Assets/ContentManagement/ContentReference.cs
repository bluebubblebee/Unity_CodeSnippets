using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContentManagement
{
    public abstract class ContentReference : ScriptableObject, IEquatable<ContentReference>
    {
        public abstract string Id { get; set; }

        public abstract string Name { get; set; }

        public override string ToString()
        {
            return Id;
        }

        public override int GetHashCode()
        {
            return Id != null ? Id.GetHashCode() : 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ContentReference);
        }

        public virtual bool Equals(ContentReference other)
        {
            if (other == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(Id) && string.IsNullOrEmpty(other.Id))
            {
                return true;
            }

            return Id != null && Id.Equals(other.Id);
        }

        // This lets you implicitly convert a ContentReference object into a string.
        // ContentReference myRef = new ContentReference { Id = "Sword_001" };
        // string id = myRef; // Automatically calls operator string(ContentReference)
        // Debug.Log(id);
        public static implicit operator string (ContentReference reference) => reference.Id;
    }

    public abstract class ContentReference<T> : ContentReference where T : ContentData, new()
    {
        [SerializeField]
        private T m_data = new T();

        public override string Id 
        {   get => Data.Id;
            set => Data.Id = value; 
        }

        public override string Name
        {
            get => Data.Name;
            set => Data.Name = value;
        }

        public T Data
        {
            get
            {
                m_data ??= new T();
                return m_data;
            }
            protected set => m_data = value;
        }

        public override bool Equals(ContentReference other)
        {
            if (base.Equals(other))
            {
                return false;
            }

            return other is ContentReference<T>;
        }
    }

}
