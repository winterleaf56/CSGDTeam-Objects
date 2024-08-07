using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Player Class for controlling the player
/// </summary>
public class Player : PlayableObjects
{
    [SerializeField] private float speed;
    [SerializeField] private Camera cam;

    [SerializeField] private float weaponDamage = 1;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private Bullet bulletPrefab;

    //public Action<float> OnHealthUpdate;

    private Rigidbody2D playerRB;

    public Action onDeath;

    private void Awake() {
        health = new Health(100, 0.5f, 50);
        playerRB = GetComponent<Rigidbody2D>();

        // Give the player a weapon
        weapon = new Weapon("Player Weapon", weaponDamage, bulletSpeed);

        cam = Camera.main;

        //OnHealthUpdate?.Invoke(health.GetHealth());
    }

    /// <summary>
    /// Moves the player in the direction of a target
    /// </summary>
    /// <param name="direction"> the direction of movement </param>
    /// <param name="target"> Target is the enemy </param>
    public override void Move(Vector2 direction, Vector2 target) {
        playerRB.velocity = direction * speed * Time.deltaTime;

        var playerScreenPos = cam.WorldToScreenPoint(transform.position);
        target.x -= playerScreenPos.x;
        target.y -= playerScreenPos.y;

        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update() {
        health.RegenHealth();
    }

    /// <summary>
    /// Responisble for making the player shoot
    /// </summary>

    public override void Shoot() {
        weapon.Shoot(bulletPrefab, this, "Enemy");
    }

    public override void Die() {
        Debug.Log("Player is dead");
        onDeath?.Invoke();

        Destroy(gameObject);
    }

    public override void Attack(float interval) {
        
    }

    public override void GetDamage(float damage) {
        health.DeductHealth(damage);

        // Update health value from the C# Action
        //OnHealthUpdate?.Invoke(health.GetHealth());

        if (health.GetHealth() <= 0) {
            Die();
        }
        
    }
}
