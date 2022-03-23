
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {
       
        GameManager.isGameOver = true;
    }
}
