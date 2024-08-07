using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableObjects : MonoBehaviour, IDamagable
{
    public Health health = new Health();

    public Weapon weapon;

    public abstract void Move(Vector2 direction, Vector2 target);

    public virtual void Move(Vector2 direction) {
        Debug.Log("Moving");
    }

    public virtual void Move(float speed) {
        Debug.Log("Moving");
    }

    public abstract void Shoot();

    public abstract void Attack(float interval);

    public abstract void Die();

    public abstract void GetDamage(float damage);
}
