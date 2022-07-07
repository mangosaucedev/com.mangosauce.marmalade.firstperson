using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Objects
{
    public interface IObjectEventHandler 
    {
        bool FireEvent<T>(IObjectEventHandler target, T e) where T : ObjectEvent;
        bool HandleEvent<T>(T e) where T : ObjectEvent;
    }
}
