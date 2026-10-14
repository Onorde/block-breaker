using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TileBuilder : MonoBehaviour
{
    [SerializeField] private GameObject rectanglePrefab;
    [SerializeField] private GameObject trianglePrefab;
    [SerializeField] private GameObject hexagonPrefab;
    [Space]
    [SerializeField] private int gridLength = 10;
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private List<GameObject> createdTiles = new List<GameObject>();
    
    private Dictionary<string, float> _prefabVariables = new Dictionary<string, float>(6);
    
    [ContextMenu("Build Rectangle Grid")]
    private void Rectangle()
    {
        BuildGrid(rectanglePrefab, AssignVariables(rectanglePrefab));
    }
    [ContextMenu("Build Triangle Grid")]
    private void Triangle()
    {
        BuildGrid(trianglePrefab, AssignVariables(trianglePrefab));
    }
    [ContextMenu("Build Hexagon Grid")]
    private void Hexagon()
    {
        BuildGrid(hexagonPrefab, AssignVariables(hexagonPrefab));
    }
    [ContextMenu("Delete Grid")]
    private void DeleteGrid()
    {
        foreach (var tile in createdTiles)
        {
            DestroyImmediate(tile);
        }
        
        createdTiles.Clear();
    }
    
    private void CreateTile(float xPosition, float yPosition, GameObject tilePrefab, Dictionary<string, float> variables, int offsetTracker, int rotationTracker)
    {
        
        GameObject newTile = Instantiate(tilePrefab, this.transform);
        if (variables.GetValueOrDefault("needsPeriodicOffset") == 1f && (offsetTracker % 2) == 1)
        {
            Vector2 tilePosition = new Vector2(xPosition * variables.GetValueOrDefault("xScale"), yPosition * variables.GetValueOrDefault("yScale") - variables.GetValueOrDefault("offsetValue"));
            newTile.transform.localPosition = tilePosition;
        }
        else
        {
            Vector2 tilePosition = new Vector2(xPosition * variables.GetValueOrDefault("xScale"), yPosition * variables.GetValueOrDefault("yScale"));
            newTile.transform.localPosition = tilePosition;
        }

        if (variables.GetValueOrDefault("needsRotation") == 1 && (rotationTracker % 2) == 1)
        {
            newTile.transform.localEulerAngles = new Vector3(0,0,variables.GetValueOrDefault("rotationDegree"));
        }
        
        createdTiles.Add(newTile);
    }
    
    private void BuildGrid(GameObject tilePrefab, Dictionary<string, float> variables)
    {
        DeleteGrid();
        int offsetTracker = 0;
        int rotationTracker = 0;
        for (int z = 0; z < gridLength; z++)
        {
            offsetTracker = 0;
            for (int x = 0; x < gridWidth; x++)
            {
                CreateTile(x, z, tilePrefab, variables, offsetTracker, rotationTracker);
                offsetTracker = offsetTracker + 1;
                rotationTracker = rotationTracker + 1;
            }
        }
        _prefabVariables.Clear();
    }

    private Dictionary<string, float> AssignVariables(GameObject tilePrefab)
    {
        if (tilePrefab.GetComponent<SpriteRenderer>().sprite.name == "Square")
        {
            _prefabVariables.Add("xScale", tilePrefab.transform.lossyScale.x);
            _prefabVariables.Add("yScale", tilePrefab.transform.lossyScale.y);
            _prefabVariables.Add("needsRotation", 0f);
            _prefabVariables.Add("needsPeriodicOffset", 0f);

            return _prefabVariables;
        }
        else if (tilePrefab.GetComponent<SpriteRenderer>().sprite.name == "Triangle")
        {
            _prefabVariables.Add("xScale", tilePrefab.transform.lossyScale.x);
            _prefabVariables.Add("yScale", tilePrefab.transform.lossyScale.y);
            _prefabVariables.Add("needsRotation", 1f);
            _prefabVariables.Add("rotationDegree", 60f);
            _prefabVariables.Add("needsPeriodicOffset", 0f);

            return _prefabVariables;
        }
        else // For hexagons
        {
            _prefabVariables.Add("xScale", tilePrefab.transform.lossyScale.x * 1.5f);
            _prefabVariables.Add("yScale", tilePrefab.transform.lossyScale.y * Mathf.Sqrt(3));
            _prefabVariables.Add("needsRotation", 0f);
            _prefabVariables.Add("needsPeriodicOffset", 1f);
            _prefabVariables.Add("offsetValue", tilePrefab.transform.lossyScale.y * Mathf.Sqrt(3));
            return _prefabVariables;
        }
        
    }
}
