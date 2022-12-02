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
    private float multiplySpeed;

    private void Start()
    {
        spawnedCouter++;


        PlayerPrefs.SetFloat("itemsSpeed", PlayerPrefs.GetFloat("itemsSpeed") + spawnedCouter * 0.03f);
    }

    private void Update()
    {
        transform.Translate(Vector2.down * PlayerPrefs.GetFloat("itemsSpeed") * Time.deltaTime);

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
        Pc = FindObjectOfType<PlayerController>();
        UM = FindObjectOfType<UiManager>();
        Am = FindObjectOfType<AudioManager>();

        UM.GameOverUI();

        Time.timeScale = 0f;

        Am._musicSource.Stop();
        
        Pc.enabled = false;
        Sm.spawnON = false;

        
    }

}
