using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersManager : MonoBehaviour
{
    private PlayerController Pc;
    private DataPersistance DP;

    

    private void Awake()
    {
        Pc = FindObjectOfType<PlayerController>();
        DP = FindObjectOfType<DataPersistance>();
        
    }

    private void Update()
    {
        
    }
    private void SpeedUp()
    {
        Pc.speed = Pc.speed + 0.5f;
    }

    private void DobleJump()
    {

    }

    private void Dash()
    {

    }

    private void TimeStop()
    {

    }

}
