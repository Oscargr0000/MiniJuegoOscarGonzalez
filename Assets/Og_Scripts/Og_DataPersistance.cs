using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Og_DataPersistance : MonoBehaviour
{
    private Og_UiManager _uiManager;
    private Og_ItemsLogic _itemLogic;
    private Og_SpawnManager _spawnManager;
    private Og_PlayerController _playerController;
    private Og_MainMenuManager _Mmm;

    public int maxPoints;
    public float maxTime;
    public int puntuacionData;
    public float itemsSpeed;

    private void Awake()
    {
        _uiManager = FindObjectOfType<Og_UiManager>();
        _playerController = FindObjectOfType<Og_PlayerController>();
        _Mmm = FindObjectOfType<Og_MainMenuManager>();
        PlayerPrefs.SetFloat("spawnRate", 2);
    }
    private void Start()
    {
        maxPoints = PlayerPrefs.GetInt("puntuacionInt");
        maxTime = PlayerPrefs.GetFloat("tiempoFloat");
        puntuacionData = PlayerPrefs.GetInt("currentPoints");

        PlayerPrefs.SetFloat("itemsSpeed", 1f);

    }


    public void SaveCurrentPoints()
    {
        puntuacionData += _playerController.puntiacionCouter;
        PlayerPrefs.SetInt("currentPoints", puntuacionData);
    }
    public void SaveDataPoints()
    {
        maxPoints = _playerController.puntiacionCouter;

        PlayerPrefs.SetInt("puntuacionInt", maxPoints);
        PlayerPrefs.Save();   
    }

    public void SaveTime()
    {
        maxTime = _uiManager.currentTime;
        PlayerPrefs.SetFloat("tiempoFloat", maxTime);
        PlayerPrefs.Save();
    }


}
