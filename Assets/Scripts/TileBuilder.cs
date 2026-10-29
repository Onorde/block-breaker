using System;
using System.Collections.Generic;
using UnityEngine;

public class TileBuilder : MonoBehaviour
{
    
    private readonly float _hexagonOffsetYCoefficient = 0.875F;
    private readonly float _hexagonOffsetXCoefficient = 0.76f;
    private readonly float _triangleRotationOffsetCoefficient = 0.2885f;
    private readonly float _equilateralTriangleHeight = MathF.Sqrt(3) / 2;
    private readonly float _triangleSpacingCoefficient = 0.5f;
    
    [SerializeField] private GameObject rectanglePrefab;
    [SerializeField] private TileRequirements rectangleRequirements;
    [Space]
    [SerializeField] private GameObject trianglePrefab;
    [SerializeField] private TileRequirements triangleRequirements;
    [Space]
    [SerializeField] private GameObject hexagonPrefab;
    [SerializeField] private TileRequirements hexagonRequirements;
    [Space]
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private TileRequirements circleRequirements;

    
    
    [Header("Grid Variables")]
    
    [SerializeField] [Min(1)] private int gridLength = 5;
    [SerializeField] [Min(1)] private int gridWidth = 5;
    [SerializeField] [Min(0)] private Vector2 gridSpacingValue;
    [SerializeField] private List<GameObject> createdTiles = new();

    private float _offsetX;
    private float _offsetY;
    private Vector2 _rotationOffset;


    [ContextMenu("Build Rectangle Grid")]
    private void Rectangle()
    {
        AssignVariables(rectanglePrefab, rectangleRequirements);
        BuildGrid(rectanglePrefab, rectangleRequirements);
    }

    [ContextMenu("Build Triangle Grid")]
    private void Triangle()
    {
        AssignVariables(trianglePrefab, triangleRequirements);
        BuildGrid(trianglePrefab, triangleRequirements);
    }

    [ContextMenu("Build Hexagon Grid")]
    private void Hexagon()
    {
        AssignVariables(hexagonPrefab, hexagonRequirements);
        BuildGrid(hexagonPrefab, hexagonRequirements);
    }

    [ContextMenu("Build Circle Grid")]
    private void Circle()
    {
        AssignVariables(circlePrefab, circleRequirements);
        BuildGrid(circlePrefab, circleRequirements);
    }

    [ContextMenu("Delete Grid")]
    private void DeleteGrid()
    {
        foreach (var tile in createdTiles) DestroyImmediate(tile);

        createdTiles.Clear();
    }
    

    private void BuildGrid(GameObject tilePrefab, TileRequirements tileRequirements)
    {
        DeleteGrid();

        bool rotationNecessary = false;
        for (var y = 0; y < gridLength; y++)
        {
            bool periodicOffsetNecessary = false;
            for (var x = 0; x < gridWidth; x++)
            {
                CreateTile(x, y, tilePrefab, tileRequirements, rotationNecessary, periodicOffsetNecessary);
                rotationNecessary = !rotationNecessary;
                periodicOffsetNecessary = !periodicOffsetNecessary;
            }
        }
    }
    
    private void CreateTile(float xPosition, float yPosition, GameObject tilePrefab, TileRequirements tileRequirements, bool rotationNecessary, bool periodicOffsetNecessary)
    {
        var newTile = Instantiate(tilePrefab, transform);
        AdjustTransform(xPosition, yPosition, newTile, tileRequirements, rotationNecessary, periodicOffsetNecessary);
        createdTiles.Add(newTile);
    }

    private void AdjustTransform(float xPosition, float yPosition, GameObject newTile, TileRequirements tileRequirements, bool rotationNecessary, bool periodicOffsetNecessary)
    {
        var tilePosition = new Vector2(xPosition * _offsetX, yPosition * _offsetY);
        newTile.transform.localPosition = tilePosition + gridSpacingValue;

        if (tileRequirements.NeedsRotation() && rotationNecessary)
        {
            newTile.transform.localEulerAngles = tileRequirements.GetRotationDegree();
            newTile.transform.localPosition = (tilePosition + _rotationOffset) + gridSpacingValue;
        }
        if (tileRequirements.NeedsPeriodicOffset() && periodicOffsetNecessary)
        {
            newTile.transform.localPosition = (tilePosition + new Vector2(0,_hexagonOffsetYCoefficient)) + gridSpacingValue;
        }
    }

    private void AssignVariables(GameObject tilePrefab, TileRequirements tileRequirements)
    {
        
        _offsetX = tileRequirements.GetOffset_X() + gridSpacingValue.x;
        _offsetY = tileRequirements.GetOffset_Y() + gridSpacingValue.y;

        if (tilePrefab.GetComponent<SpriteRenderer>().sprite.name == "Triangle")
        {
            
            _offsetX = tileRequirements.GetOffset_X() * _triangleSpacingCoefficient + gridSpacingValue.x;
            _offsetY = tileRequirements.GetOffset_Y() * _equilateralTriangleHeight + gridSpacingValue.y;
            _rotationOffset = new Vector3(0, tilePrefab.transform.lossyScale.x * _triangleRotationOffsetCoefficient, 0);
        }
        else if (tilePrefab.GetComponent<SpriteRenderer>().sprite.name == "Hexagon Flat Top")
        {
            
            _offsetX = tileRequirements.GetOffset_X() * _hexagonOffsetXCoefficient + gridSpacingValue.x;
            _offsetY = tileRequirements.GetOffset_Y() * _hexagonOffsetYCoefficient + gridSpacingValue.y;
        }


    }
}