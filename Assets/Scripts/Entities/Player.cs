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

    [SerializeField] public int nukeCap;

    //public Action<float> OnHealthUpdate;

    private Rigidbody2D playerRB;
    private bool speedIncreased, isInvulnerable;

    public int nukeCount;
    public bool canRapidFire;

    public Action onDeath;
    public Action<float,float,int> OnTimerUpdate;
    public Action<int,bool> OnNukeUpdate;

    private void Awake() {
        health = new Health(100, 0.5f, 50);
        playerRB = GetComponent<Rigidbody2D>();

        // Give the player a weapon
        weapon = new Weapon("Player Weapon", weaponDamage, bulletSpeed);

        cam = Camera.main;

        speedIncreased = false;
        isInvulnerable = false;
        canRapidFire = false;
        nukeCount = 0;

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
        playerHealth = health.GetHealth();
    }

    /// <summary>
    /// Responisble for making the player shoot
    /// </summary>

    public override void Shoot() {
        weapon.Shoot(bulletPrefab, this, "Enemy");
    }

    public void AddNuke() {
        nukeCount = Mathf.Min(nukeCount+1, nukeCap);
        OnNukeUpdate?.Invoke(nukeCount-1,true);
    }

    public void Nuke() {
        nukeCount--;
        OnNukeUpdate?.Invoke(nukeCount,false);
        weapon.Nuke();
    }

    public override void Die() {
        Debug.Log("Player is dead");
        onDeath?.Invoke();

        Destroy(gameObject);
    }

    public override void Attack(float interval) {
        
    }

    public override void GetDamage(float damage) {
        if (isInvulnerable){
            return;
        }

        health.DeductHealth(damage);

        // Update health value from the C# Action
        //OnHealthUpdate?.Invoke(health.GetHealth());

        if (health.GetHealth() <= 0) {
            Die();
        }
        
    }

    public void IncreaseSpeed(float speedIncrease, float duration) {
        StartCoroutine(IncreaseSpeedCoroutine(speedIncrease, duration));
    }

    IEnumerator IncreaseSpeedCoroutine(float speedIncrease, float duration) {
        speed = speedIncreased ? speed : speed + speedIncrease; // only increases speed once (so it does not stack)
        speedIncreased = true;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            OnTimerUpdate?.Invoke(counter,duration,0);
            yield return null; //Don't freeze Unity
        }

        //OnTimerUpdate?.Invoke(duration);
        //yield return new WaitForSeconds(duration);

        speed = speedIncreased ? speed - speedIncrease : speed;
        speedIncreased = false;
    }

    public void ShieldOn(float duration) {
        StartCoroutine(ShieldOnCoroutine(duration));
    }

    IEnumerator ShieldOnCoroutine(float duration) {
        isInvulnerable = true;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            OnTimerUpdate?.Invoke(counter,duration,1);
            yield return null; //Don't freeze Unity
        }

        isInvulnerable = false;
    }

    public void RapidFireOn(float duration, float interval) {
        StartCoroutine(RapidFireOnCoroutine(duration, interval));
    }

    IEnumerator RapidFireOnCoroutine(float duration, float interval) {
        canRapidFire = true;
        float counter = 0f;
        float counter2 = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            
            if (counter2 <= interval) {
                canRapidFire = false;
                counter2 += Time.deltaTime;
            }
            else {
                counter2 = 0;
                canRapidFire = true;
            }
            
            OnTimerUpdate?.Invoke(counter,duration,2);
            yield return null; //Don't freeze Unity
        }

        canRapidFire = false;
    }
}
