using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private EnemyWeaponController _weapon;
    private HealthBarController _healthBar;

    private float _visionRange = 20.0f;

    private bool _isSeeking = true;

    // Player position
    private Transform _playerTransform;

    public Transform weaponTransform;
    public GameObject projectile;

    private float _nextTimeToFire = 1.0f;
    private float _fireRate = 1f;
    public int Health
    {
        // Health is stored in HealthBarController
        get => _healthBar.Health;
        set => _healthBar.Health = value;
    }

    public int MaxHealth
    {
        // Health is stored in HealthBarController
        get => _healthBar.MaxHealth;
        set => _healthBar.MaxHealth = value;
    }

    public int Heal(int amount)
    {
        Health += amount;
        return Health;
    }

    public int Damage(int amount)
    {
        Health -= amount;
        return Health;
    }


    // Start is called before the first frame update
    void Start()
    {
        _weapon = gameObject.GetComponentInChildren<EnemyWeaponController>();
        if (_weapon == null)
            Debug.Log("Enemy: No weapon!");

        _healthBar = gameObject.GetComponentInChildren<HealthBarController>();
        if (_healthBar == null)
            Debug.Log("Enemy: No health bar!");

        // Find player
        _playerTransform = GameObject.FindWithTag("Player")?.transform;
        if (_playerTransform == null)
            Debug.Log("Enemy: No player entity was found!");

        Health = 200;
        MaxHealth = 200;
    }

    // Update is called once per frame
    void Update()
    {
        // Check the current state
        _isSeeking = CheckIsSeeking();
        if (_isSeeking)
        {
            Seek();
        }
        else
        {
            if (Time.time >= _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + 1 / _fireRate;
                Attack();
            }
        }

        //Health = 0;
        //Damage(1);
    }

    private bool CheckIsSeeking()
    {
        if (_playerTransform == null)
        {
            Debug.Log(false + "No player!");
            return true;
        }

        // Positions of the enemy and the player
        Vector3 position = transform.position;
        Vector3 playerPosition = _playerTransform.position;

        // Ray between the enemy and the player
        Ray ray = new Ray(position, playerPosition - position);
        
        // FIXME: I think the raycast does not ignore the enemy itself
        var mask = ~(1 << LayerMask.NameToLayer("Ignore Raycast"));

        if (Physics.Raycast(ray, out var hitInfo, _visionRange, mask))
        {
            // Player is found: if the enemy is too far it still seeks for the player, otherwise it attacks
            float distance = Vector3.Distance(playerPosition, position);
            return !(hitInfo.transform.CompareTag("Player") && distance <= _weapon.FireRange);
        }
        else
        {
            // Player not found, still seeking
            return true;
        }
    }

    void Seek()
    {
        Debug.Log("Enemy: Seek!");
    }

    void Attack()
    {
        GameObject bullet = (GameObject)Instantiate(projectile, weaponTransform.position, weaponTransform.rotation);
        //Add velocity to the projectile
        bullet.GetComponent<Rigidbody>().velocity = (weaponTransform.transform.position - _playerTransform.position) * -5f;
        Debug.Log("Enemy : Fireeeeee " + bullet.name + weaponTransform);
    }

    
}
