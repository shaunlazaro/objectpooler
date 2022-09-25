using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.ObjectPooler
{
    public class PoolableObject : MonoBehaviour
    {
        public ObjectPool parentPool;

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