using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GameManager.isGameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
