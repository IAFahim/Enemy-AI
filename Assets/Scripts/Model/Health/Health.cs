using System;
using Model.CheckAble;
using UnityEngine;
using UnityEngine.Events;

namespace Model.Health
{
    [Serializable]
    public class Health : IHealth, ICheckAble
    {
        [SerializeField] protected float value;
        public UnityEvent<Health> onHealthChanged;
        public UnityEvent<Health> onDeath;

        public float Value
        {
            get => value;
            set
            {
                this.value = value;
                if (this.value <= 0)
                {
                    Die();
                }

                onHealthChanged?.Invoke(this);
            }
        }

        public void Die(bool invoke = true)
        {
            onDeath?.Invoke(this);
        }


        public void TakeDamage(float damage, bool invoke = true)
        {
            if (invoke) Value -= damage;
            else value -= damage;
        }

        public void SetHealth(float health, bool invoke = true)
        {
            if (invoke) Value = health;
            else this.value = health;
        }

        public void Check()
        {
            Value = value;
        }
    }
}