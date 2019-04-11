using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetector : MonoBehaviour
{
    public GameObject itemUI;
    // Start is called before the first frame update
    void Start()
    {
        itemUI.SetActive(false);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") //other.CompareTag("Player"))
        {
            itemUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        itemUI.SetActive(false);
    }

}
