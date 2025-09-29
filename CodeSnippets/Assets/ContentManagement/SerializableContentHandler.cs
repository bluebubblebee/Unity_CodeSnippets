using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ContentManagement
{
    public abstract class SerializableContentHandler : ScriptableObject, IContentHandler
    {
        
        [SerializeField]
        private string m_contentLabel;

        [SerializeField]
        private string m_contentGroup;

        

        public string ContentLabel
        {
            get => m_contentLabel;
            internal set => m_contentLabel = value;
        }

        public string ContentGroup        
        {
             get => m_contentGroup;
            internal set => m_contentGroup = value;
        }
    

        public abstract Type ReferenceType { get;  }

        public abstract int ReferenceCount { get; }

        public abstract IReadOnlyList<ContentReference> WeakReferences { get; }

        public abstract bool ContainsReference(string id);

        public abstract bool TryGetReference(string id, out ContentReference reference);

        public abstract bool ReloadReferences();

        public abstract void ReloadCache();
        public abstract void PrepareCache();
        public abstract void DisposeCache();

    }

    public abstract class SerializableContentHandler<TReference, TInput> : SerializableContentHandler, IContentHandler<TReference> where TReference : ContentReference
    {
        public event Action<string> OnReferenceCreated;
        public event Action<string> OnReferenceDeleted;
        public event Action<bool> OnReferenceLoaded;

        [SerializeField]
        private string m_contentPath;

        [SerializeField]
        private TReference m_template;

        [SerializeField]
        private List<TReference> m_references = new List<TReference>();

        private ContentReferencesCache<TReference> m_cache;

        public override Type ReferenceType => typeof(TReference);        

        public override int ReferenceCount => References.Count;

        public override IReadOnlyList<ContentReference> WeakReferences => References;

        public IReadOnlyList<TReference> References => m_references;

        public string ContentPath
        {
            get => m_contentPath;
            internal set => m_contentPath = value;
        }

        public TReference Template
        {
            get => m_template;
            internal set => m_template = value;
        }

        private ContentReferencesCache<TReference> Cache
        {
            get
            {
                m_cache ??= new ContentReferencesCache<TReference>(ContentLabel);
                return m_cache;
            }
        }

        public override bool ReloadReferences()
        {
            ReloadCache();
            bool anythingChanged = ValidateReferences();
            if (anythingChanged)
            {
                ContentAssetUtility.MakeDirty(this);
            }

            OnReferenceLoaded?.Invoke(anythingChanged);
            return anythingChanged;
        }

        public override bool ContainsReference(string id)
        {
            return TryGetReference(id, out _);
        }

        public override bool TryGetReference(string id, out ContentReference reference)
        {
            return TryGetReference(id, out reference);
        }



        

        public override void ReloadCache()
        {
            DisposeCache();
            PrepareCache();
        }

        public override void PrepareCache()
        {
            if (Cache.IsPrepared)
            {
                return;
            }

            Cache.Prepare(References, GetType().Name);
        }

        public override void DisposeCache()
        {
            Cache.Dispose();
        }

        public virtual bool TryGetReference(string id, out TReference reference)
        {
            PrepareCache();
            return Cache.TryGet(id, out reference);
        }

        public virtual bool ValidateReferences()
        {
            return ValidateReferences(out _);
        }

        public virtual bool ValidateReferences(out IDictionary<string, TReference> cacheReferences)
        {
            bool anyChanges = false;
            var referencesByGuids = new Dictionary<string, TReference>();
            for (int i = References.Count - 1; i >= 0; i--)
            {
                TReference reference = References[i];
                if (!IsReferenceValid(reference))
                {
                    continue;
                }

                string id = reference.Id;
                if (referencesByGuids.ContainsKey(id))
                {
                    anyChanges = true;

                }

                if (ValidateReference(reference))
                {
                    anyChanges = true;
                }

                referencesByGuids.Add(id, reference);
            }

            cacheReferences = referencesByGuids;
            return anyChanges;
        }

        internal string GetReferencePath(ContentReference reference)
        {
            string contentPath = GetContentAssetPath();
            return GetReferencePath(reference, contentPath);
        }

        internal string GetReferencePath(ContentReference reference, string contentPath)
        {
            string filename = ContentAssetUtility.GetAssetFileName(reference.name);
            return Path.Combine(contentPath, filename);
        }

        internal string GetContentAssetPath()
        {
            return string.IsNullOrEmpty(ContentPath) ? null : ContentAssetUtility.GetAssetDirectory(ContentPath);
        }

        protected virtual bool IsReferenceValid(TReference reference)
        {
            return reference != null && !string.IsNullOrEmpty(reference.Id);
        }

        protected virtual bool ValidateReference(TReference reference)
        {
            return false;
        }

        protected virtual TReference CreateReference(TInput input)
        {
            TReference reference = CreateReferenceInstance();
            if (reference ==  null)
            {
                return null;
            }


            if (!IsReferenceValid(reference))
            {
                return null;
            }

            if (!TryCreateReferenceAsset(reference))
            {
                return null;
            }

            AppendReference(reference);
            string id = reference.Id;

            OnReferenceCreated?.Invoke(id);
            return reference;
        }

        protected virtual TReference CreateReferenceInstance()
        {
            TReference reference = m_template == null ? ScriptableObject.CreateInstance<TReference>() : ScriptableObject.Instantiate(m_template);

            if (reference != null)
            {
                reference.name = typeof(TReference).Name;                
            }

            return reference;
        }

        protected virtual bool TryCreateReferenceAsset(TReference reference)
        {
#if UNITY_EDITOR
            string assetPath = GetReferencePath(reference);
            if (string.IsNullOrEmpty(assetPath))
            {
                return false;
            }

            assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);
            AssetDatabase.CreateAsset(reference, assetPath);

#endif
            return false;
        }

        protected virtual void AppendReference(TReference reference)
        {
            Cache.Append(reference);
            m_references.Add(reference);
            ContentAssetUtility.MakeDirty(this);
        }
    }
}
