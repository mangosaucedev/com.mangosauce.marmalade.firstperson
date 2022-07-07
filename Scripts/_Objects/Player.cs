using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Objects
{
    public static class Player
    {
        private static SceneObject instance;

        public static SceneObject Instance
        {
            get => GetInstance();
            set => SetInstance(value);
        }

        private static SceneObject GetInstance()
        {
            return instance;
        }

        private static void SetInstance(SceneObject instance)
        {
            Player.instance = instance;
        }
    }
}
