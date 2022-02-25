using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int Health { get; set; }

    public int MaxHealth { get; private set; }

    public int Heal(int amount)
    {
        Health = System.Math.Min(Health + amount, MaxHealth);
        return Health;
    }

    public int Damage(int amount)
    {
        Health -= amount;
        return Health;
    }

}
