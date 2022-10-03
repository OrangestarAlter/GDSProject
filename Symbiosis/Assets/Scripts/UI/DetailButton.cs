using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailButton : MonoBehaviour
{
    [SerializeField] private Collectible collectible;

    public void ShowDetail()
    {
        GameUI.instance.ShowDetail(collectible.sprite, collectible.title, collectible.description);
    }
}
