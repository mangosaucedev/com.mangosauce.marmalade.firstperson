using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Marmalade.Objects
{
    public class PartCollection
    {
        public List<Part> parts = new List<Part>();

        private SceneObject parent;

        public PartCollection(SceneObject parent)
        {
            this.parent = parent;
            foreach (Part part in parent.GetComponentsInChildren<Part>())
                parts.Add(part);
        }

        public Part Add(Type type) => (Part) parent.PartsParent.gameObject.AddComponent(type);
        

        public T Get<T>() where T : Part
        {
            TryGet(out T part);
            return part;
        }

        public bool TryGet<T>(out T part) where T : Part
        {
            part = null;
            foreach (Part p in parts)
            {
                if (p is T)
                    part = (T)p;
            }
            return part;
        }

        public bool TryGet(Type type, out Part part)
        {
            part = null;
            foreach (Part p in parts)
            {
                if (p.GetType() == type)
                    part = p;
            }
            return part;
        }

        public bool Remove<T>() where T : Part
        {
            if (!TryGet(out T part))
                return false;
            return Remove(part);
        }

        public bool Remove(Part part)
        {
            if (!parts.Contains(part))
                return false;
            parts.Remove(part);
            Object.Destroy(part);
            return true;
        }
    }
}
