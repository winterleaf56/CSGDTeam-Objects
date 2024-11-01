using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Bullet : MonoBehaviour {
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    private string targetTag;

    public void SetBullet(float _damage, float _speed, string _targetTag) {
        damage = _damage;
        speed = _speed;
        targetTag = _targetTag;
    }

    private void Update() {
        Move();
    }

    void Move() { 
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void Damage(IDamagable damageable) {
        if (damageable != null) {
            damageable.GetDamage(damage);
            Debug.Log($"Damaged Something");

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.name);

        if (!other.gameObject.CompareTag(targetTag)) {
            return;
        }

        IDamagable damageable = other.GetComponent<IDamagable>();
        Damage(damageable);
    }

}
