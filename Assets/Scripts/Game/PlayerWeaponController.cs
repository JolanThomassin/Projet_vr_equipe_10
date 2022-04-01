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
    public Transform weaponTransform;
    public GameObject projectile;
    public GameObject parameter;
    
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
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

            _device.TryGetFeatureValue(CommonUsages.menuButton, out bool parameterPressed);
            if(parameterPressed)
            {
                Parameter();
            }
        }

    }
    private void LateUpdate()
    {
    }
    public void Fire()
    {
        //Creation of the bullet at the weapon transform
        GameObject bullet = (GameObject)Instantiate(projectile, weaponTransform.position, weaponTransform.rotation);
        //Add velocity to the projectil
        bullet.GetComponent<Rigidbody>().velocity = (weaponTransform.forward) * 100f;
        Debug.Log("Player : Fireeeeee " + bullet.name + weaponTransform);
    }

    public void Parameter()
    {
        if(!parameter.activeSelf)
        {
            parameter.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            parameter.SetActive(false); 
            Time.timeScale = 1;
        }
    }
  
}
