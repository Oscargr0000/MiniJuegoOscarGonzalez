using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsLogic : MonoBehaviour
{
    

    public int recompensa;

    private SpawnManager Sm;
    private PlayerController Pc;
    private UiManager UM;
    private AudioManager Am;

    public int spawnedCouter;

    public bool isActivated;

    private void Start()
    {
        isActivated = true;
        spawnedCouter++;

        Pc = FindObjectOfType<PlayerController>();
        PlayerPrefs.SetFloat("itemsSpeed", PlayerPrefs.GetFloat("itemsSpeed") + spawnedCouter * 0.03f);
    }

    private void Update()
    {
        if (Pc.desactivateTime.Equals(false))
        {
            transform.Translate(Vector2.down * PlayerPrefs.GetFloat("itemsSpeed") * Time.deltaTime);
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameOver();
        }
    }


    void GameOver()
    {
        Sm = FindObjectOfType<SpawnManager>();
        
        UM = FindObjectOfType<UiManager>();
        Am = FindObjectOfType<AudioManager>();
        UM.GameOverUI();

        Time.timeScale = 0f;

        Am._musicSource.Stop();
        Am.PLayMusic(1);
        
        Pc.enabled = false;
        Sm.spawnON = false;

        
    }

}
