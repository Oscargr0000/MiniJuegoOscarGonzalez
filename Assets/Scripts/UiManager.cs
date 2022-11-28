using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI puntosText;
    public GameObject gameOverCanvas;   

    private PlayerController Pc;




    void Start()
    {
        Pc = FindObjectOfType<PlayerController>();

        gameOverCanvas.SetActive(false);
    }


    void Update()
    {
        puntosText.text = Pc.puntiacionCouter.ToString();
        timeText.text = Time.time.ToString("f0");
    }
}
