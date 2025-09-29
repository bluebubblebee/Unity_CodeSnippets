using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContentManagement
{
    public abstract class ContentData
    {
        /// <summary>
        /// Unique GUID for the data
        /// </summary>
        public abstract string Id { get; set; }

        public abstract string Name { get; set; }

        public override bool Equals (object obj)
        {
            if (obj is ContentData data)
            {
                return Id == data.Id;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}
