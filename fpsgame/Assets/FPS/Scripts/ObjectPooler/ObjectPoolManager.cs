using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.ObjectPoolManager
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance { get => _instance; }
        static ObjectPoolManager _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }
        }
    }
}

