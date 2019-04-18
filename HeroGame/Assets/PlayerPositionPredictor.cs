using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionPredictor : MonoBehaviour
{
    public PlayerController pc;

    public float hSpeed;
    public float vSpeed;
    public bool grounded = false;

    // Need to standardize
    public float speed = 0.1F;
    public float g = -0.01F;
    public float vSpeedMax = 1;

    private bool oneKey;

    // Start is called before the first frame update
    void Start()
    {
        hSpeed = 0;
        vSpeed = 0;

        oneKey = false;
    }

    void FixedUpdate() {
        if (!grounded) {
            vSpeed += g;
            if (vSpeed > vSpeedMax)
                vSpeed = vSpeedMax;
        }
        else
            vSpeed = 0;

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) {
            if (oneKey) {
                hSpeed = -hSpeed;
            }
            else {
                hSpeed = 0;
            }

            oneKey = false;
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            hSpeed = -speed;
            oneKey = true;
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            hSpeed = speed;
            oneKey = true;
        }
        else
            hSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + hSpeed, transform.position.y + vSpeed);
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        pc.moveOK = false;
        if (collision.collider.gameObject.tag == "Floor") {
            int points = collision.contactCount;
            ContactPoint2D[] contacts = new ContactPoint2D[points];
            Debug.Log(collision.GetContacts(contacts));
            Debug.Log(contacts[0].separation);

            //Fix position
            transform.position = new Vector2(transform.position.x, transform.position.y - (contacts[0].separation * 2));

            grounded = true;


        }
    }
}
