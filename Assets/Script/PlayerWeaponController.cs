using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class PlayerWeaponController : MonoBehaviour
{
    //private bool _canFire;
    //private bool _fireIsPressed;
    
    private float _nextTimeToFire = 1.0f;
    private float _fireRate = 1.5f; // Increase for faster fire rate
    private InputDevice _device;

    public GameObject projectile;
    
    

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDeviceCharacteristics rigthChara = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rigthChara, inputDevices);

        foreach (var inputDevice in inputDevices)
        {
            Debug.Log(inputDevice.name + inputDevice.characteristics);
        }

        if (inputDevices.Count > 0)
        {
            _device = inputDevices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream

        _device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        
        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1/_fireRate;
            Fire();
=======
        //Store all of the device input
        List<InputDevice> inputDevices;
        if (_device != null)
        {
            inputDevices = new List<InputDevice>();
            //Only get the right controller
            InputDeviceCharacteristics rigthChara = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right; 
            //Store it in the inputdevece List
            InputDevices.GetDevicesWithCharacteristics(rigthChara, inputDevices); 
            foreach (var inputDevice in inputDevices)
            {
                Debug.Log(inputDevice.name + inputDevice.characteristics);
                //Debug to see if we get the device
            }

            if (inputDevices.Count > 0)
            {
                //store the wanted device in a var
                _device = inputDevices[0];
            }

            //Get the trigger button, if pressed triggerPressed = true
            _device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);
            
            if (Time.time >= _nextTimeToFire && triggerPressed)
            {
                _nextTimeToFire = Time.time + 1/_fireRate;
                Fire();
            }
>>>>>>> Stashed changes
        }
    }
    private void LateUpdate()
    {
    }
    public void Fire()
    {
<<<<<<< Updated upstream
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, _range))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                GameObject bullet = (GameObject)Instantiate(projectile, this.transform);
                bullet.GetComponent<Rigidbody>().velocity = hit.transform.position * 2.5f;
                Debug.Log("Fireeeeee");
            }
        }
=======
        //Creation of the bullet at the weapon transform
        GameObject bullet = (GameObject)Instantiate(projectile, weaponTransform.position, weaponTransform.rotation);
        //Add velocity to the projectil
        bullet.GetComponent<Rigidbody>().velocity = (weaponTransform.forward) * -5f;
        Debug.Log("Player : Fireeeeee " + bullet.name + weaponTransform);
>>>>>>> Stashed changes
    }

  
}
