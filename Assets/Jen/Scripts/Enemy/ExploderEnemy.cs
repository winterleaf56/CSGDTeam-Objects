using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderEnemy : Enemy
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackTime = 0;

    private float timer = 0;
    private float setSpeed = 0;

    protected override void Start(){
        base.Start();
        health = new Health(1, 0, 1);
        setSpeed = speed;
    }

    public void SetExploderEnemy(float attackRange, float attackTime){
        this.attackRange = attackRange;
        this.attackTime = attackTime;
    }

    protected override void Update(){
        base.Update();

        if (target == null)
            return;
        
        if (Vector2.Distance(transform.position, target.position) < attackRange) { // if target is in range and has not attacked yet
            speed = 0;
            Attack(attackTime);
            return; // set to explode once it reaches the target and not move again
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
            target.GetComponent<IDamagable>().GetDamage(weapon.GetDamage());
            Debug.Log($"Enemy attacking for {weapon.GetDamage()} damage");
            Destroy(gameObject); // destroys this enemy without increasing score or having a chance of dropping a pickup
        }
    }
}
