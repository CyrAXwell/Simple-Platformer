using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideDustEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem dust;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        dust.Play();
    }
}
