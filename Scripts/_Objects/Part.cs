using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Objects
{
    public abstract class Part : MonoBehaviour, IObjectEventHandler, IResource
    {
        private const int MAX_LOG_MESSAGES = 24;

        public List<string> log = new List<string>();
        public SceneObject parent;

        protected bool applicationQuitting;

        private Guid guid = Guid.NewGuid();

        public Guid Guid => guid;

        protected virtual void Awake()
        {
            Resources.Register(this);
            if (!parent)
                parent = GetComponentInParent<SceneObject>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        protected virtual void OnDestroy()
        {
            Resources.Unregister(this);
        }

        private void OnApplicationQuit() => applicationQuitting = true;

        public virtual void LogMessage(string message)
        {
            while (log.Count > MAX_LOG_MESSAGES)
                log.RemoveAt(0);
            log.Add(message);
        }

        public bool FireEvent<T>(IObjectEventHandler target, T e) where T : ObjectEvent =>
            target.HandleEvent(e);

        public bool HandleEvent<T>(T e) where T : ObjectEvent
        {
            return true;
        }
    }
}
