using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicSound : MonoBehaviour
{
    [SerializeField] private bool startPlay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (startPlay)
            RelicPickup.instance.PlaySound();
        else
            RelicPickup.instance.StopSound();
    }
}
