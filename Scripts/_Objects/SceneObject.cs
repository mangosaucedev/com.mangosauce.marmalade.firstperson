using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Objects
{
    [AddComponentMenu("Dream2/Objects/SceneObject")]
    [RequireComponent(typeof(Rigidbody))]
    public class SceneObject : PersistentObject, IObjectEventHandler
    {
        [SerializeField] private Collider col;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform partsParent;

        private PartCollection parts;

        public PartCollection Parts
        {
            get
            {
                if (parts == null)
                    parts = new PartCollection(this);
                return parts;
            }
        }

        public MeshRenderer MeshRenderer
        {
            get
            {
                if (!meshRenderer)
                    meshRenderer = GetComponentInChildren<MeshRenderer>();
                return meshRenderer;
            }
            set => meshRenderer = value;
        }

        public Collider Collider
        {
            get
            {
                if (!col)
                {
                    Transform meshParent = transform.Find("Mesh");
                    col = meshParent.GetComponent<Collider>();
                }
                return col;
            }
            set => col = value;
        }

        public Rigidbody Rigidbody
        {
            get
            {
                if (!rb)
                    rb = GetComponent<Rigidbody>();
                return rb;
            }
            set => rb = value;
        }

        public Transform PartsParent
        {
            get
            {
                if (!partsParent)
                    partsParent = transform.Find("Parts");
                return partsParent;
            }
            set => partsParent = value;
        }

        private void Awake()
        {
            Resources.Register(this);
            if (CompareTag("Player"))
                Player.Instance = this;
        }

        private void OnDestroy()
        {
            Resources.Unregister(this);
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }

        public bool FireEvent<T>(IObjectEventHandler target, T e) where T : ObjectEvent =>
            target.HandleEvent(e);

        public bool HandleEvent<T>(T e) where T : ObjectEvent
        {
            bool successful = true;
            foreach (Part part in Parts.parts)
            {
                if (!FireEvent(part, e))
                    successful = false;
            }
            return successful;
        }
    }
}