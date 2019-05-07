using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionPredictor : MonoBehaviour
{
    public PlayerController pc;
    public BoxCollider2D box;

    public float hSpeed;
    public float vSpeed;
    public bool grounded;
    
    public bool facingRight;

    // dashing variables
    public bool dashing;
    public float dashTimer;
    public float dashMaxTime;
    public bool dashOnCooldown;
    public float dashCooldownTime;

    // Need to standardize
    public float speed = 0.2F;
    public float g = -0.01F;
    public float vSpeedMin = -0.1F;

    // allows for movement similar to DPP
    private bool oneKey;

    // collision variables
    public float collisionFix = 0.1F;

    void Start()
    {
        hSpeed = 0;
        vSpeed = 0;
        grounded = false;

        facingRight = true;

        dashing = false;
        dashMaxTime = 0.25F;
        dashTimer = 0;
        dashCooldownTime = 3;

        oneKey = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        pc.moveOK = false;
        if (collision.collider.gameObject.tag == "Floor")
        {
            int points = collision.contactCount;
            ContactPoint2D[] contacts = new ContactPoint2D[points];
            points = collision.GetContacts(contacts);
            float bottom = box.bounds.center.y - box.bounds.extents.y;

            transform.position = new Vector2(transform.position.x, transform.position.y + (contacts[0].point.y - bottom) - vSpeed);

            grounded = true;

        }
        if (collision.collider.gameObject.tag == "Wall")
        {
            int points = collision.contactCount;
            ContactPoint2D[] contacts = new ContactPoint2D[points];
            points = collision.GetContacts(contacts);
            float displacement = 0;
            if (hSpeed > 0)
            {
                displacement = box.bounds.center.x + box.bounds.extents.x;
                transform.position = new Vector2(transform.position.x - (displacement - contacts[0].point.x) - hSpeed, transform.position.y);
            }
            else if (hSpeed < 0)
            {
                displacement = box.bounds.center.x - box.bounds.extents.x;
                transform.position = new Vector2(transform.position.x + (contacts[0].point.x - displacement) - hSpeed, transform.position.y);
            }
        }
        if (collision.collider.gameObject.tag == "Door")
        {
            if (!dashing)
            {
                int points = collision.contactCount;
                ContactPoint2D[] contacts = new ContactPoint2D[points];
                points = collision.GetContacts(contacts);
                float displacement = 0;
                if (hSpeed > 0)
                {
                    displacement = box.bounds.center.x + box.bounds.extents.x;
                    transform.position = new Vector2(transform.position.x - (displacement - contacts[0].point.x) - hSpeed, transform.position.y);
                }
                else if (hSpeed < 0)
                {
                    displacement = box.bounds.center.x - box.bounds.extents.x;
                    transform.position = new Vector2(transform.position.x + (contacts[0].point.x - displacement) - hSpeed, transform.position.y);
                }
            }
            else
            {
                Destroy(collision.collider.gameObject);
            }
        }
    }

    void FixedUpdate() {
        // determine gravity
        if (!grounded) {
            vSpeed += g;
            if (vSpeed < vSpeedMin)
                vSpeed = vSpeedMin;
        }
        else
            vSpeed = 0;

        if (Input.GetKey(KeyCode.LeftShift) && grounded && !dashOnCooldown)
            dashing = true;

        dashTimer += Time.fixedDeltaTime;

        // horizontal movement
        if (dashing)
        {
            // dash mechanic
            if (facingRight)
                hSpeed = speed * 2;
            else
                hSpeed = -speed * 2;
            
            if (dashTimer > dashMaxTime)
            {
                dashTimer = 0;
                dashing = false;
                dashOnCooldown = true;
            }
        }
        else
        {
            if (dashTimer > dashCooldownTime)
            {
                dashTimer = 0;
                dashOnCooldown = false;
            }

            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
            {
                if (oneKey)
                {
                    hSpeed = -hSpeed;
                    facingRight = !facingRight;
                }

                oneKey = false;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                hSpeed = -speed;
                facingRight = false;
                oneKey = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                hSpeed = speed;
                facingRight = true;
                oneKey = true;
            }
            else
                hSpeed = 0;
        }

        // change position at the end
        transform.position = new Vector2(transform.position.x + hSpeed, transform.position.y + vSpeed);
    }
    
    
}
