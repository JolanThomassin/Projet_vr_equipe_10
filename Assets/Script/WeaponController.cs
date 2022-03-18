using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class WeaponController : MonoBehaviour
{
    private bool _canFire;
    private bool _fireIsPressed;
    
    private float _range = 50.0f;
    private float _damage = 10.0f;
    private float _nextTimeToFire = 1.0f;
    private float _fireRate = 1.5f;
    private InputDevice _device;

    public GameObject projectile;
    public LayerMask enemyLayer;
    public Camera cam;

    

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

        _device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        while (triggerValue > 0.1f)
        {
            _fireIsPressed = true;
            Fire();
        }
        if(Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1/_fireRate; 
            Fire();
        }
    }
    private void LateUpdate()
    {
    }
    public void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, _range))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                GameObject bullet = (GameObject)Instantiate(projectile, this.transform);
                _fireIsPressed = false;
                bullet.GetComponent<Rigidbody>().velocity = hit.transform.position * 2.5f;
                Debug.Log("Fireeeeee");
            }
        }
    }

  
}
