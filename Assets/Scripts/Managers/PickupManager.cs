using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {
    [SerializeField] private PickupSpawn[] pickups;

    [Range(0, 1)]
    [SerializeField] private float spawnChance;

    List<Pickup> pickupPool = new List<Pickup>();
    Pickup chosenPickup;

    // Start is called before the first frame update
    void Start()
    {
        //Populate the pickup pool with the pickups
        foreach (PickupSpawn spawn in pickups) {
            for (int i = 0; i < spawn.spawnWeight; i++) {
                pickupPool.Add(spawn.pickup);
            }
        }
    }

    public void SpawnPickup(Vector2 position) {
        if (pickupPool.Count <= 0) {
            return;
        }

        if (Random.Range(0.0f, 1.0f) < spawnChance) {
            chosenPickup = pickupPool[Random.Range(0, pickupPool.Count)];
            Instantiate(chosenPickup, position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public struct PickupSpawn {
    public Pickup pickup;
    public int spawnWeight;
}
