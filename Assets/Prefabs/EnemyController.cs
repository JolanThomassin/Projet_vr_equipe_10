using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private WeaponController _weapon;
    private HealthBarController _healthBar;

    private float _visionRange = 20.0f;

    private bool _isSeeking = true;

    // Player position
    private Transform _playerTransform;

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
        _weapon = gameObject.GetComponentInChildren<WeaponController>();
        if (_weapon == null)
            Debug.LogWarning("Enemy: No weapon!");

        _healthBar = gameObject.GetComponentInChildren<HealthBarController>();
        if (_healthBar == null)
            Debug.LogWarning("Enemy: No health bar!");

        // Find player
        _playerTransform = GameObject.FindWithTag("Player")?.transform;
        if (_playerTransform == null)
            Debug.LogWarning("Enemy: No player entity was found!");

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
            Attack();
        }

        Health = 0;
        //Damage(1);
    }

    private bool CheckIsSeeking()
    {
        if (_playerTransform == null)
        {
            Debug.Assert(false, "No player!");
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
        Debug.Log("Enemy: Shoot!");
    }
}
