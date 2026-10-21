using System;
using System.Collections.Generic;
using UnityEngine;

public class TileBuilder : MonoBehaviour
{
    
    private readonly float _hexagonOffsetYCoefficient = 0.875F;
    private readonly float _hexagonOffsetXCoefficient = 0.76f;
    private readonly float _rotationOffsetCoefficient = 0.2885f;
    private readonly float _equilateralTriangleHeight = MathF.Sqrt(3) / 2;
    private readonly float _triangleSpacingCoefficient = 0.5f;
    
    [SerializeField] private GameObject rectanglePrefab;
    [SerializeField] private GameObject trianglePrefab;
    [SerializeField] private GameObject hexagonPrefab;
    [SerializeField] private GameObject circlePrefab;
    
    [Header("Grid Variables")]
    
    [SerializeField] [Min(1)] private int gridLength = 5;
    [SerializeField] [Min(1)] private int gridWidth = 5;
    [SerializeField] [Min(0)] private Vector2 gridSpacingValue;
    [SerializeField] private List<GameObject> createdTiles = new();

    private readonly Dictionary<string, bool> _prefabRequirements = new(6);
    private float _offsetX;
    private float _offsetY;
    private Vector3 _rotationDegree;
    private Vector2 _rotationOffset;


    [ContextMenu("Build Rectangle Grid")]
    private void Rectangle()
    {
        AssignVariables(rectanglePrefab);
        BuildGrid(rectanglePrefab);
    }

    [ContextMenu("Build Triangle Grid")]
    private void Triangle()
    {
        AssignVariables(trianglePrefab);
        BuildGrid(trianglePrefab);
    }

    [ContextMenu("Build Hexagon Grid")]
    private void Hexagon()
    {
        AssignVariables(hexagonPrefab);
        BuildGrid(hexagonPrefab);
    }

    [ContextMenu("Build Circle Grid")]
    private void Circle()
    {
        AssignVariables(circlePrefab);
        BuildGrid(circlePrefab);
    }

    [ContextMenu("Delete Grid")]
    private void DeleteGrid()
    {
        foreach (var tile in createdTiles) DestroyImmediate(tile);

        createdTiles.Clear();
    }
    

    private void BuildGrid(GameObject tilePrefab)
    {
        DeleteGrid();

        bool rotationNecessary = false;
        for (var y = 0; y < gridLength; y++)
        {
            bool periodicOffsetNecessary = false;
            for (var x = 0; x < gridWidth; x++)
            {
                CreateTile(x, y, tilePrefab, rotationNecessary, periodicOffsetNecessary);
                rotationNecessary = !rotationNecessary;
                periodicOffsetNecessary = !periodicOffsetNecessary;
            }
        }

        _prefabRequirements.Clear();
    }
    
    private void CreateTile(float xPosition, float yPosition, GameObject tilePrefab, bool rotationNecessary, bool periodicOffsetNecessary)
    {
        var newTile = Instantiate(tilePrefab, transform);
        AdjustTransform(xPosition, yPosition, newTile, rotationNecessary, periodicOffsetNecessary);
        createdTiles.Add(newTile);
    }

    private void AdjustTransform(float xPosition, float yPosition, GameObject newTile, bool rotationNecessary, bool periodicOffsetNecessary)
    {
        var tilePosition = new Vector2(xPosition * _offsetX, yPosition * _offsetY);
        newTile.transform.localPosition = tilePosition + gridSpacingValue;

        if (_prefabRequirements.GetValueOrDefault("needsRotation") && rotationNecessary)
        {
            newTile.transform.localEulerAngles = _rotationDegree;
            newTile.transform.localPosition = (tilePosition + _rotationOffset) + gridSpacingValue;
        }
        if (_prefabRequirements.GetValueOrDefault("needsPeriodicOffset") && periodicOffsetNecessary)
        {
            newTile.transform.localPosition = (tilePosition + new Vector2(0,_hexagonOffsetYCoefficient)) + gridSpacingValue;
        }
    }

    private void AssignVariables(GameObject tilePrefab)
    {
        if (tilePrefab.GetComponent<SpriteRenderer>().sprite.name == "Square")
        {
            _offsetX = tilePrefab.transform.lossyScale.x + gridSpacingValue.x;
            _offsetY = tilePrefab.transform.lossyScale.y + gridSpacingValue.y;
            _prefabRequirements.Add("needsRotation", false);
            _prefabRequirements.Add("needsPeriodicOffset", false);
        }
        else if (tilePrefab.GetComponent<SpriteRenderer>().sprite.name == "Triangle")
        {
            _offsetX = tilePrefab.transform.lossyScale.x * _triangleSpacingCoefficient + gridSpacingValue.x;
            _offsetY = tilePrefab.transform.lossyScale.y * _equilateralTriangleHeight + gridSpacingValue.y;
            _prefabRequirements.Add("needsRotation", true);
            _prefabRequirements.Add("needsPeriodicOffset", false);
            _rotationDegree = new Vector3(0, 0, 180);
            _rotationOffset = new Vector3(0, tilePrefab.transform.lossyScale.x * _rotationOffsetCoefficient, 0);
        }
        else if (tilePrefab.GetComponent<SpriteRenderer>().sprite.name == "Circle")
        {
            _offsetX = tilePrefab.transform.lossyScale.x + gridSpacingValue.x;
            _offsetY = tilePrefab.transform.lossyScale.y + gridSpacingValue.y;
            _prefabRequirements.Add("needsRotation", false);
            _prefabRequirements.Add("needsPeriodicOffset", false);

        }
        else // For hexagons
        {
            _offsetX = tilePrefab.transform.lossyScale.x * _hexagonOffsetXCoefficient + gridSpacingValue.x;
            _offsetY = tilePrefab.transform.lossyScale.y * _hexagonOffsetYCoefficient + gridSpacingValue.y;
            _prefabRequirements.Add("needsRotation", false);
            _prefabRequirements.Add("needsPeriodicOffset", true);
        }

    }
}