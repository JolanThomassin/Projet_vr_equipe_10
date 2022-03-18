using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    public float FireRange { get; set; }

    [SerializeField]
    //private Transform _projectileSpawn;

    //[SerializeField]
    //private GameObject _projectile;

    //private float _fireRate = 1.0f;
    //private float _damage = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        FireRange = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
