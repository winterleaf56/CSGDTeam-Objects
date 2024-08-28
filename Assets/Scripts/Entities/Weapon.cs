using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon {
    private string name;
    private float damage;
    private float bulletSpeed;

    public Weapon(string _name, float _damage, float _bulletSpeed) {
        name = _name;
        damage = _damage;
        bulletSpeed = _bulletSpeed;
    }

    public Weapon() { 
    
    }

    public void Shoot(Bullet _bullet, PlayableObjects _player, string _targetTag, float timeToDie = 5) {
        Debug.Log("Shooting");
        Bullet tempBullet = GameObject.Instantiate(_bullet, _player.transform.position, _player.transform.rotation);
        tempBullet.SetBullet(damage, bulletSpeed, _targetTag);

        GameObject.Destroy(tempBullet.gameObject, timeToDie);
    }

    public void Nuke() {
        GameManager.GetInstance().KillAll(false); // kills everything but player
    }

    public float GetDamage() {
        
        return damage;
    }
}
