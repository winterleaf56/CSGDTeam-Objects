using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup, IDamagable {

    [SerializeField] private float healthMin, healthMax;

    SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = FindAnyObjectByType<SoundManager>();
    }

    public override void OnPickedUp() {
        base.OnPickedUp();
        
        float health = Random.Range(healthMin, healthMax);

        var player = GameManager.GetInstance().GetPlayer();

        player.health.AddHealth(health);

        _soundManager.PlaySound("HealthUp");
    }

    public void GetDamage(float damage) {
        OnPickedUp();
    }
}
