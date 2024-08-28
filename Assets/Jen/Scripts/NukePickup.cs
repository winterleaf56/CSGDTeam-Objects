using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePickup : Pickup, IDamagable {
    public override void OnPickedUp() {
        base.OnPickedUp();

        var player = GameManager.GetInstance().GetPlayer();

        player.AddNuke();
    }

    public void GetDamage(float damage) {
        OnPickedUp();
    }
}
