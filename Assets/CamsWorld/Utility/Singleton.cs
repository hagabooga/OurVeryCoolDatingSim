using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameronsWorld.Utility
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance { get; set; }

        public static T Instance => instance;


        protected virtual void Start()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
                Debug.LogError($"{name} has more than 1 Singleton<{typeof(T).ToString()}>! Deleted the component...");
            }
            else
            {
                instance = this as T;
                // DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}