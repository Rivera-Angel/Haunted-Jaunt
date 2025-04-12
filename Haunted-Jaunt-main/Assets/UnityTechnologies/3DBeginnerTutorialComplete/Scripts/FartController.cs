using System.Diagnostics;
using UnityEngine;

public class FartController : MonoBehaviour
{
    public ParticleSystem fartEffect;
    public Transform fartOrigin;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (fartEffect != null && fartOrigin != null)
            {
                // Move the particle system to the fart origin
                fartEffect.transform.position = fartOrigin.position;
                fartEffect.transform.rotation = fartOrigin.rotation;

                fartEffect.Play();
            }
            else
            {
                UnityEngine.Debug.LogWarning("Fart particle system or origin not assigned!");
            }
        }
    }
}


