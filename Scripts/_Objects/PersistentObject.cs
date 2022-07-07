using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Objects
{
    public class PersistentObject : MonoBehaviour, IResource
    {
        public LocalVarCollection localVars = new LocalVarCollection();
    
        private Guid guid = Guid.NewGuid();

        public Guid Guid => guid;
    }
}
