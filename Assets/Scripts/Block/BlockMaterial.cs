using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BlockMaterial : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;

    private MeshRenderer _renderer;

    public int CurrentMaterial { get; private set; }

    private void Awake()
    {
        CurrentMaterial = Random.Range(0, 3);
    }

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        SetMaterial();
    }

    private void SetMaterial() => _renderer.material = _materials[CurrentMaterial];
    private void SwitchMaterial() => CurrentMaterial++;

    public void SetNextMaterial()
    {
        SwitchMaterial();
        SetMaterial();
    }
}
