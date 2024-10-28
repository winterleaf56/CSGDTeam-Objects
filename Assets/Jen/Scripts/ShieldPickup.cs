using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : Pickup, IDamagable {

    [SerializeField] private float shieldDuration;

    public override void OnPickedUp() {
        base.OnPickedUp();

        var player = GameManager.GetInstance().GetPlayer();

        player.ShieldOn(shieldDuration);
    }

    public void GetDamage(float damage) {
        OnPickedUp();
    }
}
