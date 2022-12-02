using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistance : MonoBehaviour
{
    private UiManager _uiManager;
    private ItemsLogic _itemLogic;
    private SpawnManager _spawnManager;
    private PlayerController _playerController;

    public int maxPoints;
    public float maxTime;
    public int puntuacionData;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UiManager>();
        _playerController = FindObjectOfType<PlayerController>();
    }
    private void Start()
    {
        maxPoints = PlayerPrefs.GetInt("puntuacionInt");
        maxTime = PlayerPrefs.GetFloat("tiempoFloat");
        puntuacionData = PlayerPrefs.GetInt("currentPoints");
    }


    public void SaveCurrentPoints()
    {
        puntuacionData += _playerController.puntiacionCouter;
        PlayerPrefs.SetInt("currentPoints", puntuacionData);

        Debug.Log(PlayerPrefs.GetInt("currentPoints"));
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
