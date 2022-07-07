using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Marmalade.Objects;

namespace Marmalade.Editors
{
    public static class ObjectMenuItems
    {
        private static SceneObject sceneObject;
        private static Rigidbody rigidbody;
        private static Transform partsParent;

        [MenuItem("Marmalade/FpCore/Objects/Create Scene Object")]
        public static void CreateSceneObject()
        {
            GameObject root = CreateObject("Scene Object", Selection.activeTransform, typeof(SceneObject), typeof(Rigidbody));
            sceneObject = root.GetComponent<SceneObject>();
            rigidbody = root.GetComponent<Rigidbody>();
            
            InjectSceneObjectDependencies();

            Transform parent = root.transform;

            partsParent = CreateObject("Parts", parent).transform;
            GameObject meshObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            meshObject.transform.SetParent(parent);
            meshObject.transform.position = new Vector3(0f, 1f, 0f);
            CreateObject("Bottom", parent);

            InjectDependencies();

            Undo.RegisterCreatedObjectUndo(root, "new Scene Object");
        }

        private static GameObject CreateObject(string name, Transform parent, params Type[] components)
        {
            GameObject gameObject = new GameObject(name, components);
            gameObject.transform.SetParent(parent);
            return gameObject;
        }

        private static void InjectDependencies()
        {
            InjectSceneObjectDependencies();
        }

        private static void InjectSceneObjectDependencies()
        {
            sceneObject.Rigidbody = rigidbody;
            sceneObject.PartsParent = partsParent;
        }
    }
}
