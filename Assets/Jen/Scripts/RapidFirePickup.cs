using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFirePickp : Pickup, IDamagable {

    [SerializeField] private float rapidFireDuration, interval;

    public override void OnPickedUp() {
        base.OnPickedUp();

        var player = GameManager.GetInstance().GetPlayer();

        player.RapidFireOn(rapidFireDuration, interval);
    }

    public void GetDamage(float damage) {
        OnPickedUp();
    }
}
