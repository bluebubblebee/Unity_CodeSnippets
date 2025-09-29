using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContentManagement
{
    internal interface IContentEditorPanel: IDrawable, IDisposable
    {
        string Label { get; }
        void Reload();
        void RequiresReload();
        void RequiresRepaint();    }
}
