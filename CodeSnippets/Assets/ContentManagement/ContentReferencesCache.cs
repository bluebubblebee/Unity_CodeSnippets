using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContentManagement
{
    public class ContentReferencesCache<TReference> : IDisposable where TReference: ContentReference
    {
        private readonly IDictionary<string, TReference> m_referencesByIds = new Dictionary<string, TReference>();
        private readonly string m_cacheLabel;

        public bool IsPrepared { get; private set; }

        public ContentReferencesCache(string cacheLabel)
        {
            m_cacheLabel = cacheLabel;
        }

        public void Prepare(IReadOnlyList<TReference> references, string label)
        {
            int referenceCount = references?.Count ?? 0;
            for (int i=0; i< referenceCount; i++)
            {
                TReference reference = references[i];
                if (reference == null)
                {
                    continue;
                }

                string id = reference.Id;
                if (string.IsNullOrEmpty(id)|| Contains(id))
                {
                    continue;
                }

                Append(reference);

                IsPrepared = true;
            }
        }

        public void Dispose()
        {
            m_referencesByIds.Clear();
            IsPrepared = false;
        }

        public bool Contains(string id)
        {
            return m_referencesByIds.ContainsKey(id);
        }

        public void Append(TReference reference)
        {
            if (reference == null || string.IsNullOrEmpty(reference.Id))
            {
                return;
            }

            m_referencesByIds.Add(reference.Id, reference);
        }

        public bool Remove(TReference reference)
        {
            return Remove(reference?.Id);
        }

        public bool Remove(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            return m_referencesByIds.Remove(id);
        }

        public bool TryGet(string id, out TReference reference)
        {
            if (string.IsNullOrEmpty(id))
            {
                reference = null;
                return false;
            }

            return m_referencesByIds.TryGetValue(id, out reference);

        }

    }
}
