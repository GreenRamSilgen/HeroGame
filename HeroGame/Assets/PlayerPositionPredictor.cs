using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionPredictor : MonoBehaviour
{
    public PlayerController pc;
    public BoxCollider2D box;

    public float hSpeed;
    public float vSpeed;
    public bool grounded = false;
    public bool dashing = false;

    // Need to standardize
    public float speed = 0.2F;
    public float g = -0.01F;
    public float vSpeedMin = -0.1F;

    // allows for movement similar to DPP
    private bool oneKey;

    void Start()
    {
        hSpeed = 0;
        vSpeed = 0;

        oneKey = false;
    }

    void FixedUpdate() {
        if (!grounded) {
            vSpeed += g;
            if (vSpeed < vSpeedMin)
                vSpeed = vSpeedMin;
        }
        else
            vSpeed = 0;

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) {
            if (oneKey) {
                hSpeed = -hSpeed;
            }

            oneKey = false;
        }
        else if (Input.GetKey(KeyCode.A)) {
            hSpeed = -speed;
            oneKey = true;
        }
        else if (Input.GetKey(KeyCode.D)) {
            hSpeed = speed;
            oneKey = true;
        }
        else
            hSpeed = 0;

        transform.position = new Vector2(transform.position.x + hSpeed, transform.position.y + vSpeed);
    }
    
    private void OnCollisionStay2D(Collision2D collision) {
        pc.moveOK = false;
        if (collision.collider.gameObject.tag == "Floor") {
            int points = collision.contactCount;
            ContactPoint2D[] contacts = new ContactPoint2D[points];
            points = collision.GetContacts(contacts);
            float bottom = box.bounds.center.y - box.bounds.extents.y;

            transform.position = new Vector2(transform.position.x, transform.position.y + (contacts[0].point.y - bottom));

            grounded = true;

        }
        if (collision.collider.gameObject.tag == "Wall") {
            int points = collision.contactCount;
            ContactPoint2D[] contacts = new ContactPoint2D[points];
            points = collision.GetContacts(contacts);
            float displacement = 0;
            if (hSpeed > 0) {
                displacement = box.bounds.center.x + box.bounds.extents.x;
                transform.position = new Vector2(transform.position.x - (displacement - contacts[0].point.x), transform.position.y);
            }
            else {
                displacement = box.bounds.center.x - box.bounds.extents.x;
                transform.position = new Vector2(transform.position.x + (contacts[0].point.x - displacement), transform.position.y);
            }
        }
        if (collision.collider.gameObject.tag == "Door") {
            if (!dashing) {
                int points = collision.contactCount;
                ContactPoint2D[] contacts = new ContactPoint2D[points];
                points = collision.GetContacts(contacts);
                float displacement = 0;
                if (hSpeed > 0) {
                    displacement = box.bounds.center.x + box.bounds.extents.x;
                    transform.position = new Vector2(transform.position.x - (displacement - contacts[0].point.x), transform.position.y);
                }
                else {
                    displacement = box.bounds.center.x - box.bounds.extents.x;
                    transform.position = new Vector2(transform.position.x + (contacts[0].point.x - displacement), transform.position.y);
                }
            }
            else {
                Destroy(collision.collider.gameObject);
            }
        }
    }
}
