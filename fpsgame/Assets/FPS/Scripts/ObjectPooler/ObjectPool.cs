using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.ObjectPooler
{
    // At the object pool level, we work with PoolableObject instead of <T>,
    // since we're only instantiating PoolableObjects or components.
    public class ObjectPool : MonoBehaviour
    {
        // Settings:
        [SerializeField]
        public PoolableObject prefab;
        [SerializeField]
        int pregenAmount;
        // TODO: Add max object count

        List<PoolableObject> poolObjects;
        Queue<PoolableObject> inactiveObjects;
        List<PoolableObject> activeObjects;

        void Start()
        {
            poolObjects = new List<PoolableObject>();
            inactiveObjects = new Queue<PoolableObject>();
            activeObjects = new List<PoolableObject>();

            // TODO: Do this at a better time (prevents lag spikes with lots of pools).
            Init();
        }

        void GenerateNewObject()
        {
            // TODO: Add some generic onCreate function

            // We set the pool object as the parent to keep things neat.  Can be changed if this causes problems in the future.
            PoolableObject newObj = Instantiate(prefab, gameObject.transform); 
            newObj.parentPool = this;
            newObj.gameObject.SetActive(false);
            poolObjects.Add(newObj);
            inactiveObjects.Enqueue(newObj);
        }

        // Currently only called from Start.
        // Call this at a different time if we change this to need some other values passed in at runtime.
        public void Init()
        {
            if (poolObjects.Count > 0)
                return; // Already init

            for (int i = 0; i < pregenAmount; i++)
                GenerateNewObject();
        }

        public PoolableObject Get()
        {
            if (inactiveObjects.Count == 0)
                GenerateNewObject();

            PoolableObject returnObject = inactiveObjects.Dequeue();
            activeObjects.Add(returnObject);
            returnObject.gameObject.SetActive(true);

            return returnObject;
        }

        public void Return(PoolableObject poolObj)
        {
            if (!poolObjects.Contains(poolObj))
            {
                Debug.LogError("Attempted to return object that is not in object pool");
                return;
            }
            if(!activeObjects.Contains(poolObj))
            {
                return; // Do not give warning here. We might set a poolable object to return on a timer, then return earlier.
            }

            // TODO: Add onReturn function (not needed for current task. but could be useful in the future)
            poolObj.gameObject.SetActive(false);
            activeObjects.Remove(poolObj);
            inactiveObjects.Enqueue(poolObj);
        }
    }
}

