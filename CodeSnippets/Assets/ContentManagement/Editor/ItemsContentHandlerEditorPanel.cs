using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContentManagement
{
    [ContentHandlerPanel(typeof(ItemsContentHandler))]
    public class ItemsContentHandlerEditorPanel : ContentHandlerEditorPanelBase
    {
        public ItemsContentHandlerEditorPanel( ContentEditorHandler handler) : base (handler)
        {

        }
    }
}
