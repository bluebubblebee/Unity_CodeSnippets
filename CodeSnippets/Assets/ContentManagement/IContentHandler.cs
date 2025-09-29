using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContentManagement
{
    public interface IContentHandler
    {
        string ContentLabel { get;  }
        string ContentGroup { get; }
        Type ReferenceType { get;  }

        int ReferenceCount { get; }
        IReadOnlyList<ContentReference> WeakReferences { get; }

        bool ReloadReferences();
        bool ContainsReference(string id);
        bool TryGetReference(string id, out ContentReference reference);

        void ReloadCache();
        void PrepareCache();
        void DisposeCache();

    }

    public interface IContentHandler<TReference> : IContentHandler where TReference: ContentReference
    {
        event Action<string> OnReferenceCreated;
        event Action<string> OnReferenceDeleted;

        IReadOnlyList<TReference> References { get; }

        bool TryGetReference(string id, out TReference reference);

        bool ValidateReferences();
        bool ValidateReferences(out IDictionary<string, TReference> cacheReferences);
    }
}
