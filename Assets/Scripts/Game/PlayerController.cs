using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Quaternion _initRot;
    public HealthBarController _healthBar;
    private int _health = 250;

    private GameObject _movementSys;
    private GameObject _weapon;
    public GameObject gameOver;
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
        _movementSys = GameObject.Find("Locomotion System");
        _weapon = GameObject.Find("WeponPlayer");
        Health = _health;
        MaxHealth = _health;
        
    }

    
    void LateUpdate()
    {
        transform.rotation = _initRot;

        
    }
    // Update is called once per frame
    void Update()
    {

        Die();
    }

    public void Die()
    {
        if (_health <=0)
        {
            _movementSys.SetActive(false);
            gameOver.SetActive(true);
            _weapon.GetComponent<PlayerWeaponController>().enabled = false;
            GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject ennemi in ennemies)
            {
                ennemi.GetComponent<EnemyController>().enabled = false;
            }
        }
    }

    

    
  
}
