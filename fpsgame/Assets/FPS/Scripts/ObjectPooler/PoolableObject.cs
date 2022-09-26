using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.ObjectPooler
{
    public class PoolableObject : MonoBehaviour
    {
        ObjectPool parentPool;
        public void SetObjectPool(ObjectPool parentPool)
        {
            this.parentPool = parentPool;
        }

        public virtual void Return()
        {
            parentPool.Return(this);
        }
        public virtual void Return(float time)
        {
            StartCoroutine(ReturnAfterTime(time));
        }

        IEnumerator ReturnAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            parentPool.Return(this);
        }
    }
}