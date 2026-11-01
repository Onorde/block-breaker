using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

    private List<GameObject> _tiles = new List<GameObject>();
    private bool _allTilesDestroyed;

    
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToMainMenu()
    {

        SceneManager.LoadScene("MainMenu");

    }

    private void Start()
    {
        _tiles.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void Update()
    {
        _tiles.RemoveAll(go => go == null);
        
        if (_tiles.Contains(GameObject.FindWithTag("Enemy")))
        {
            _allTilesDestroyed = false;
        }
        else
        {
            _allTilesDestroyed = true;
        }
        
        if (_allTilesDestroyed)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        
    }
    
    
}
