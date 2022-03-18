using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class PlayerWeaponController : MonoBehaviour
{
    //private bool _canFire;
    //private bool _fireIsPressed;
    
    private float _range = 50.0f;
    //private float _damage = 10.0f;
    private float _nextTimeToFire = 1.0f;
    private float _fireRate = 1.5f;
    private InputDevice _device;
    public Transform weaponTransform;
    public GameObject projectile;
    
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        List<InputDevice> inputDevices;
        if (_device != null)
        {
            inputDevices = new List<InputDevice>();
            InputDeviceCharacteristics rigthChara = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right;
            InputDevices.GetDevicesWithCharacteristics(rigthChara, inputDevices);
            foreach (var inputDevice in inputDevices)
            {
                Debug.Log(inputDevice.name + inputDevice.characteristics);
            }

            if (inputDevices.Count > 0)
            {
                _device = inputDevices[0];
            }

            _device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);
            
            if (Time.time >= _nextTimeToFire && triggerPressed)
            {
                _nextTimeToFire = Time.time + 1/_fireRate;
                Fire();
            }
        }

    }
    private void LateUpdate()
    {
    }
    public void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, _range))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                GameObject bullet = (GameObject)Instantiate(projectile, weaponTransform.position, weaponTransform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = (weaponTransform.transform.position - hit.transform.position) * -5f;
                Debug.Log("Player : Fireeeeee " + bullet.name + weaponTransform);
            }
        }
    }

  
}
