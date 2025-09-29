using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContentManagement
{
    internal interface IContentHandlerEditorPanel : IContentEditorPanel
    {
       string Group { get; }
    }
}
