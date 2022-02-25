using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _meshRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        AlignCamera();
        UpdateParams();
    }

    private void AlignCamera()
    {
        if (_mainCamera != null)
        {
            var camXform = _mainCamera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }

    private void UpdateParams()
    {
        _meshRenderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetFloat("_Fill", _damageable.Health / (float)_damageable.MaxHealth);
        _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
    }


    private Camera _mainCamera;
    private Damageable _damageable;
    private MeshRenderer _meshRenderer;
    private MaterialPropertyBlock _materialPropertyBlock;
}
