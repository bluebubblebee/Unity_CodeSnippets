using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContentManagement
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =false)]
    public class ContentHandlerPanelAttribute : Attribute
    {
        public Type ContentHandlerType { get; }


        public ContentHandlerPanelAttribute(Type contentHandlerType)
        {
            ContentHandlerType = contentHandlerType;
        }
    }
}
