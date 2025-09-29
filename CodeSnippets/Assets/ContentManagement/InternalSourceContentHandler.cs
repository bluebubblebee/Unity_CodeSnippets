using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContentManagement
{
    public class InternalSourceContentHandler<TReference> : SerializableContentHandler<TReference, string>, IDefaultReferencesCreator where TReference : ContentReference
    {             
        public ContentReference CreateReference()
        {
            string id = GetNextId();
            return CreateReference(id);
        }

        public virtual string GetNextId()
        {
            return "";
        }
    }
}
