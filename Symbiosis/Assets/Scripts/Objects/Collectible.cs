using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float collectible;

    private bool added = false;
    private float sign = 1;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("C" + collectible))
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0;
            sign *= -1;
        }
        transform.position += new Vector3(0, sign * 0.25f * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !added)
        {
            added = true;
            Inventory.instance.GetCollectible(collectible);
            PlayerPrefs.SetFloat("C" + collectible, collectible);
            Destroy(gameObject);
        }
    }
}
