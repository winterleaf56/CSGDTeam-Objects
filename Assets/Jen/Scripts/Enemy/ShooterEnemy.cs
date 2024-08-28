using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackTime = 0;
    
    [SerializeField] private Bullet bulletPrefab;

    private float timer = 0;
    private float setSpeed = 0;

    LineRenderer lineRenderer; 

    protected override void Start(){
        base.Start();
        health = new Health(1, 0, 1);
        setSpeed = speed;

        // line renderer
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.05f;
    }

    public void SetShooterEnemy(float attackRange, float attackTime){
        this.attackRange = attackRange;
        this.attackTime = attackTime;
    }

    protected override void Update(){
        base.Update();

        if (target == null)
            return;
        
        if (Vector2.Distance(transform.position, target.position) < attackRange) { // if target is in range
            speed = 0;
            Attack(attackTime);
        }
        else {
            speed = setSpeed;
        }
        DrawPath();
    }

    public override void Attack (float interval) {
        if (timer <= interval) {
            timer+= Time.deltaTime;
        }
        else {
            timer = 0;
            Shoot();
        }
    }

    public override void Shoot() {
        Debug.Log("Shooter enemy is shooting");
        weapon.Shoot(bulletPrefab, this, "Player");
    }

    // draws path between enemy and target (player)
    private void DrawPath(){
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);

        if(Vector2.Distance(transform.position, target.position) >= attackRange) {
            lineRenderer.positionCount = 0;
            return;
        }
        lineRenderer.SetPosition(1, target.position);
    }
}
