using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsLogic : MonoBehaviour
{
    private float speed = 1.5f;

    public int recompensa;

    private SpawnManager Sm;
    private PlayerController Pc;
    private UiManager UM;
    private AudioManager Am;

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Sm = FindObjectOfType<SpawnManager>();
            Pc = FindObjectOfType<PlayerController>();
            UM = FindObjectOfType<UiManager>();
            Am = FindObjectOfType<AudioManager>();

            Time.timeScale = 0f;

            Am._musicSource.Stop();
            UM.gameOverCanvas.SetActive(true);
            Pc.enabled = false;
            Sm.spawnON = false;
            speed = 0f;
        }
    }

}
