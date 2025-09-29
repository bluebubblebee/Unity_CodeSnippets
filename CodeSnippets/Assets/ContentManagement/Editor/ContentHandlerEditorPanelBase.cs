using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ContentManagement
{
    public abstract class ContentHandlerEditorPanelBase : IContentEditorPanel
    {
        private static readonly GUIContent m_referencesCountLabel = new GUIContent("References Count");
        private static readonly GUIContent s_createButtonLabel = new GUIContent("Create New", "Create new, default referene.");
        private static readonly GUIContent s_reloadButtonLabel = new GUIContent("Reload", "Reloads available content");
        private static readonly GUIContent m_deleteButtonLabel = new GUIContent("Delete", "Destroy selected references");

        public string Label => throw new System.NotImplementedException();

        protected ContentEditorHandler Handler { get; }

        public ContentHandlerEditorPanelBase(ContentEditorHandler handler)
        {
            Handler = handler;
        }


        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void OnGui()
        {
            if (Handler ==  null)
            {
                EditorGUILayout.HelpBox("Content Handler not available", MessageType.Warning);
                return;
            }

            OnGuiSafe();
        }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void RequiresReload()
        {
            throw new System.NotImplementedException();
        }

        public void RequiresRepaint()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void OnGuiSafe()
        {
            
        }
    }
}
