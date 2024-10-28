using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player player;
    private float horizontal, vertical;
    private Vector2 lookTarget;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        if (!GameManager.GetInstance().IsPlaying()) {
            return;
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        lookTarget = Input.mousePosition;

        if (Input.GetMouseButtonDown(0)) {
            player.Shoot();
        }

        if (Input.GetMouseButton(0) && player.canRapidFire) {
            player.Shoot();
        }
        
        if (Input.GetMouseButtonDown(1) && player.nukeCount > 0) {
            player.Nuke();
        }
    }

    private void FixedUpdate() {
        player.Move(new Vector2(horizontal, vertical), lookTarget);
        
    }
}
