using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private bool isDefault;

    private void Awake()
    {
        if (isDefault && RespawnPosition.instance)
                RespawnPosition.instance.SetRespawnPosition(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && RespawnPosition.instance.transform.position != transform.position)
            RespawnPosition.instance.SetRespawnPosition(transform.position);
    }
}
