using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Objects
{
    public abstract class ObjectEvent 
    {
        public const int MAX_POOL_SIZE = 8192;

        public static readonly string id = "";

        private static ObjectEventPool pool = new ObjectEventPool();

        public int uid;

        public abstract string Id { get; }

        public ObjectEvent()
        {
            uid = Random.Range(100000000, 999999999);
        }

        public static T Get<T>() where T : ObjectEvent, new() =>
            pool.Get<T>();

        public virtual bool ReturnToPool()
        {
            Reset();
            return pool.Return(this);
        }

        protected abstract void Reset();
    }
}
