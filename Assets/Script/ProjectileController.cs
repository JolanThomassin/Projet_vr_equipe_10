using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    private int _playerDamage = 50;
    private int _enemyDamage = 10;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            Destroy(this.gameObject);
        }

        if(collision != null && !collision.gameObject.CompareTag("Ennemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().Damage(_playerDamage);
        }
        if (collision != null && !collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Damage(_enemyDamage);
        }
    }
}
