using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    private Camera _mainCamera;
    private MeshRenderer _meshRenderer;
    private MaterialPropertyBlock _materialPropertyBlock;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

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

    public int Health { get; set; }
    public int MaxHealth { get; set; }


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
        _materialPropertyBlock.SetFloat("_Fill", Health / (float)MaxHealth);
        _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
