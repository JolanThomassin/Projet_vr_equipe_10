using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Quaternion _initRot;
    public HealthBarController _healthBar;

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
        _healthBar = gameObject.GetComponentInChildren<HealthBarController>();
        if (_healthBar == null)
            Debug.Log("Player: No health bar!");
        _initRot = transform.rotation;
        
    }

    
    void LateUpdate()
    {
        transform.rotation = _initRot;

        
    }
    // Update is called once per frame
    void Update()
    {


    }

    

    
  
}
