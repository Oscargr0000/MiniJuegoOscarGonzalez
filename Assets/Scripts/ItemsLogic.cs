using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsLogic : MonoBehaviour
{
    private float speed = 1.5f;

    public int recompensa;

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("GameOver");
        }
    }

}
