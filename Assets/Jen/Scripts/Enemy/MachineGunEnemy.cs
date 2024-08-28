using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunEnemy : Enemy
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackTime = 0;
    
    [SerializeField] private Bullet bulletPrefab;

    private float timer = 0;
    private float setSpeed = 0;

    protected override void Start(){
        base.Start();
        health = new Health(1, 0, 1);
        setSpeed = speed;
    }

    public void SetMachineGunEnemy(float attackRange, float attackTime){
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
        Debug.Log("Machine Gun enemy is shooting");
        weapon.Shoot(bulletPrefab, this, "Player");
    }
}
