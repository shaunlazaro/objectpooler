using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.ObjectPooler
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance { get => _instance; }
        static ObjectPoolManager _instance;

        // Implement this as one pool per prefab.
        Dictionary<PoolableObject, ObjectPool> pools;

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

        private void Start()
        {
            pools = new Dictionary<PoolableObject, ObjectPool>();

            // Register every pool (that is a child of this):
            ObjectPool[] childPools = GetComponentsInChildren<ObjectPool>();
            foreach(ObjectPool childPool in childPools)
            {
                pools.Add(childPool.prefab, childPool);
            }
        }

        public T GetObject<T>(T prefab)
        {
            ObjectPool pool = pools[prefab as PoolableObject];
            GameObject returnObj = pool.Get().gameObject;
            return returnObj.GetComponent<T>();
        }

        // For now, we only implement only this overload (to match GameObject.Instantiate).
        public T GetObject<T>(T prefab, Vector3 position, Quaternion rotation)
        {
            T obj = GetObject<T>(prefab);
            (obj as Component).gameObject.transform.position = position;
            (obj as Component).gameObject.transform.rotation = rotation;
            return obj;
        }
    }
}

