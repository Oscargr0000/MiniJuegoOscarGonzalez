using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistance : MonoBehaviour
{
    private UiManager _uiManager;
    private ItemsLogic _itemLogic;
    private SpawnManager _spawnManager;
    private PlayerController _playerController;
    private MainMenuManager _Mmm;

    public int maxPoints;
    public float maxTime;
    public int puntuacionData;
    public float itemsSpeed;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UiManager>();
        _playerController = FindObjectOfType<PlayerController>();
        _Mmm = FindObjectOfType<MainMenuManager>();
        PlayerPrefs.SetFloat("spawnRate", 2);
    }
    private void Start()
    {
        maxPoints = PlayerPrefs.GetInt("puntuacionInt");
        maxTime = PlayerPrefs.GetFloat("tiempoFloat");
        puntuacionData = PlayerPrefs.GetInt("currentPoints");

        PlayerPrefs.SetFloat("itemsSpeed", 1.5f);

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

    public void LoadData()
    {
        _uiManager.totalPoints.text = PlayerPrefs.GetInt("currentPoints").ToString();
        _uiManager.recordPpoint.text = PlayerPrefs.GetInt("puntuacionInt").ToString();
        _uiManager.recordTpoint.text = PlayerPrefs.GetFloat("tiempoFloat").ToString("f2");
    }


}
