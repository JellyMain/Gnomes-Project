using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public static event Action OnBuildingEntered;

    private const string MAIN_HOUSE = "MainHouse";
    private const string OCEAN = "Ocean";
    private const string MAP = "Map";

    public static SceneLoader Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            LoadMapScene();
        }
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            LoadMainHouseScene();
        }
    }


    public void LoadMainHouseScene()
    {
        SceneManager.LoadScene(MAIN_HOUSE);
        OnBuildingEntered?.Invoke();
    }


    public void LoadMapScene()
    {
        SceneManager.LoadScene(MAP);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}
