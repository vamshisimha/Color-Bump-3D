using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BallController ball = other.GetComponent<BallController>();

        if(!ball || GameManager.singleton.GameEnded)
        {
            return;
        }

        Debug.Log("Çizgi bitti.");

        GameManager.singleton.EndGame(true);
    }
}