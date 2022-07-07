using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Objects
{
    public class ObjectEventPool 
    {
        private Dictionary<Type, List<ObjectEvent>> pools =
            new Dictionary<Type, List<ObjectEvent>>();

        public T Get<T>() where T : ObjectEvent, new()
        {
            Type type = typeof(T);
            List<ObjectEvent> pool = GetPool(type);
            if (pool.Count > 0)
            {
                T e = (T)pool[0];
                if (e != null)
                {
                    pool.Remove(e);
                    return e;
                }
            }
            return new T();
        }

        private List<ObjectEvent> GetPool(Type type)
        {
            if (!pools.TryGetValue(type, out List<ObjectEvent> pool))
            {
                pool = new List<ObjectEvent>();
                pools[type] = pool;
            }
            return pool;
        }

        public bool Return(ObjectEvent e)
        {
            Type type = e.GetType();
            List<ObjectEvent> pool = GetPool(type);
            if (pool.Contains(e))
                return false;
            pool.Add(e);
            return true;
        }
    }
}
