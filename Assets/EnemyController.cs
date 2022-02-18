using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Health = 20;
        _weapon = gameObject.GetComponentInChildren<WeaponController>();
        if (_weapon == null)
            Debug.LogWarning("Enemy: No weapon!");
        else Debug.Log("Enemy fire range: " + _weapon.FireRange);

        // Find player
        _playerTransform = GameObject.FindWithTag("Player")?.transform;
        if (_playerTransform == null)
            Debug.LogWarning("Enemy: No player entity was found!");
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
    }

    private bool CheckIsSeeking()
    {
        // Positions of the enemy and the player
        Vector3 position = transform.position;
        Vector3 playerPosition = _playerTransform.position;

        // Ray between the enemy and the player
        Ray ray = new Ray(position, playerPosition - position);
        
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
            return false;
        }
    }

    void Seek()
    {

    }

    void Attack()
    {
        Debug.Log("Enemy: Shoot!");
    }


    private WeaponController _weapon;
    
    public int Health { get; set; }
    private float _visionRange = 20.0f;

    private bool _isSeeking;

    // Player position
    private Transform _playerTransform;
}
