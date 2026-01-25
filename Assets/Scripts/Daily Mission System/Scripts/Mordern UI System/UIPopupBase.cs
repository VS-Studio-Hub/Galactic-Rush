using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Venthan.ModernUISystem
{
    public abstract class UIPopupBase : MonoBehaviour
    {
        public static Action<UIPopupBase> closed;
    }
}