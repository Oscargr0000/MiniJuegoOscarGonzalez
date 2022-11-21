using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI puntosText;

    private PlayerController Pc;

    // Start is called before the first frame update
    void Start()
    {
        Pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        puntosText.text = Pc.puntiacionCouter.ToString();
        timeText.text = Time.time.ToString("f2");
    }
}
