using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup, IDamagable {

    [SerializeField] private float speedIncrease, speedIncreaseDuration;

    public override void OnPickedUp() {
        base.OnPickedUp();

        var player = GameManager.GetInstance().GetPlayer();

        player.IncreaseSpeed(speedIncrease, speedIncreaseDuration);
    }

    public void GetDamage(float damage) {
        OnPickedUp();
    }
}