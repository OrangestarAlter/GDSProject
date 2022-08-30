using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    private List<Transform> insideObjects = new List<Transform>();
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnSolid()
    {
        gameObject.layer = 7;
        if (insideObjects.Count != 0)
        {
            foreach (Transform obj in insideObjects)
            {
                if (obj.CompareTag("Player"))
                {
                    PlayerController player = obj.GetComponent<PlayerController>();
                    player.OutLiquid();
                    player.InSolid();
                }
            }
        }
        boxCollider.isTrigger = false;
    }

    public void TurnLiquid()
    {
        gameObject.layer = 8;
        if (insideObjects.Count != 0)
        {
            foreach (Transform obj in insideObjects)
            {
                if (obj.CompareTag("Player"))
                {
                    PlayerController player = obj.GetComponent<PlayerController>();
                    player.OutSolid();
                    player.InLiquid();
                }
            }
        }
        boxCollider.isTrigger = true;
    }

    public void TurnGas()
    {
        gameObject.layer = 9;
        if (insideObjects.Count != 0)
        {
            foreach (Transform obj in insideObjects)
            {
                if (obj.CompareTag("Player"))
                {
                    PlayerController player = obj.GetComponent<PlayerController>();
                    player.OutSolid();
                    player.OutLiquid();
                }
            }
        }
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            insideObjects.Add(collision.transform);
            if (gameObject.layer == 8)
                collision.GetComponent<PlayerController>().InLiquid();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.layer != 7)
            if (collision.CompareTag("Player"))
            {
                insideObjects.Remove(collision.transform);
                if (gameObject.layer == 8)
                    collision.GetComponent<PlayerController>().OutLiquid();
            }
    }
}
