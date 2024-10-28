using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayableObjects
{
    [SerializeField] protected float speed;
    [SerializeField] protected int minCurrencyDrop = 1, maxCurrencyDrop = 3;

    protected Transform target;

    private EnemyType enemyType;

    protected virtual void Start() { 
        try {
            target = GameObject.FindWithTag("Player").transform;
        } catch (NullReferenceException e) {
            Debug.Log("Player not found, " + e);
            //Destroy(gameObject);
            GameManager.GetInstance().PlayerDied();
        }
    }

    protected virtual void Update() {
        if (target != null) {
            Move(target.position);
        } else {
            Move(speed);
        }
    }

    /*
    public Enemy(string _enemyName, float _speed, EnemyType _enemyType, Health _enemyHealth, Weapon _enemyWeapon) {
        enemyName = _enemyName;
        speed = _speed;
        enemyType = _enemyType;
        health = _enemyHealth;
        enemyWeapon = _enemyWeapon;
    }

    public Enemy() {
        
    }

    */

    public override void Move(Vector2 direction, Vector2 target) {
        
    }

    public override void Move(Vector2 direction) {
        direction.x -= transform.position.x;
        direction.y -= transform.position.y;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public override void Move(float speed) {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public override void Shoot() {
        Debug.Log("Enemy shooting");
    }

    public override void Attack(float interval) {
        Debug.Log($"Attacking every {interval} seconds");
    }

    public override void Die() {
        Debug.Log("Enemy died");
        GameManager.GetInstance().NotifyDeath(this);
        Destroy(gameObject);
    }

    public override void GetDamage(float damage) {
        Debug.Log("Enemy Damaged for " + damage);
        health.DeductHealth(damage);
        GameManager.GetInstance().scoreManager.IncrementScore();
        GameManager.GetInstance().currencyManager.IncreaseCurrency(UnityEngine.Random.Range(minCurrencyDrop, maxCurrencyDrop) * GameManager.GetInstance().GetDifficultyCoefficient()); // drops currency within the range multiplied by the diff. Coeff.
        if (health.GetHealth() <= 0) {
            Die();
        }
    }
}
