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

    private void Awake()
    {
        _uiManager = FindObjectOfType<UiManager>();
        _playerController = FindObjectOfType<PlayerController>();
    }


    public void SaveData()
    {
        if (_playerController.puntiacionCouter >= maxPoints)
        {
            maxPoints = _playerController.puntiacionCouter;
            Debug.Log(maxPoints);
        }

        if (_uiManager.currentTime >= maxTime)
        {
            maxTime = _uiManager.currentTime;
        }

        PlayerPrefs.SetInt("puntuacionInt", maxPoints);
        PlayerPrefs.SetFloat("tiempoFloat", maxTime);
        PlayerPrefs.Save();

        
    }

    public void LoadData()
    {
        _uiManager.recordPpoint.text = PlayerPrefs.GetInt("puntuacionInt").ToString();
        _uiManager.recordTpoint.text = PlayerPrefs.GetFloat("tiempoFloat").ToString("f2");
    }
}
