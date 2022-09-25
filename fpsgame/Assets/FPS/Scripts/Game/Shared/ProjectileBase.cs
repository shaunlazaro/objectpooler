﻿using UnityEngine;
using UnityEngine.Events;
using Unity.FPS.ObjectPooler;

namespace Unity.FPS.Game
{
    public abstract class ProjectileBase : PoolableObject
    {
        public GameObject Owner { get; private set; }
        public Vector3 InitialPosition { get; private set; }
        public Vector3 InitialDirection { get; private set; }
        public Vector3 InheritedMuzzleVelocity { get; private set; }
        public float InitialCharge { get; private set; }

        public UnityAction OnShoot;

        public void Shoot(WeaponController controller)
        {
            Owner = controller.Owner;
            InitialPosition = transform.position;
            InitialDirection = transform.forward;
            InheritedMuzzleVelocity = controller.MuzzleWorldVelocity;
            InitialCharge = controller.CurrentCharge;

            OnShoot?.Invoke();
        }
    }
}