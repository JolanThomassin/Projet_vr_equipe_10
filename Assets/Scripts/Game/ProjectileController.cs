using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private int _playerDamage = 50;
    private int _enemyDamage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponent<EnemyController>().Damage(_playerDamage);
            Destroy(this.gameObject);
        }
        else if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponent<PlayerController>().Damage(_enemyDamage);
            Destroy(this.gameObject);
        }
        else if(!collision.gameObject.CompareTag("Weapon"))
        {
            Debug.Log(collision.gameObject.name);
            Destroy(this.gameObject) ;
        }
    }
}
