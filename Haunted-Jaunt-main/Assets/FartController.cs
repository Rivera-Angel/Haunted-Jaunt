using UnityEngine;

public class FartController : MonoBehaviour
{
    public ParticleSystem fartEffect;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (fartEffect != null)
            {
                fartEffect.Play();
            }
            else
            {
                Debug.LogWarning("No fart particle system assigned!");
            }
        }
    }
}

