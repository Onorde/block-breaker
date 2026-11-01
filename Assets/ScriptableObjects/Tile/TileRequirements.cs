using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TileRequirements", menuName = "Scriptable Objects/TileRequirements")]
public class TileRequirements : ScriptableObject
{
    
    [SerializeField] private GameObject tilePrefab;
    [Space]
    [SerializeField] private bool needsRotation;
    [SerializeField] private Vector3 rotationDegree;
    [Space]
    [SerializeField] private bool needsPeriodicOffset;
    
    private float _offsetX;
    private float _offsetY;

    public bool NeedsRotation()
    {
        return needsRotation;
    }
    public bool NeedsPeriodicOffset()
    {
        return needsPeriodicOffset;
    }

    public float GetOffset_X()
    {
        _offsetX = tilePrefab.transform.lossyScale.x;
        return _offsetX;
    }

    public float GetOffset_Y()
    {
        _offsetY = tilePrefab.transform.lossyScale.y;
        return _offsetY;
    }

    public Vector3 GetRotationDegree()
    {
        return rotationDegree;
    }
}
