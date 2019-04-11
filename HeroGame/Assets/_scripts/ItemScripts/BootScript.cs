using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootScript : MonoBehaviour
{
    public Collider2D item;
    public Collider2D player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      if(item.IsTouching(player) && Input.GetKeyDown(KeyCode.G))
        {
            ResourceTracker.resourceTracker.IncreaseCurrency(500);
            ResourceTracker.resourceTracker.IncreaseFans(500);
        }
    }
    
}
