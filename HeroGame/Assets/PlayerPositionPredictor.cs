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

    //stairs
    public float slope;
    public float yint;
    public Vector2 LeftBound;
    public Vector2 RightBound;
    public bool stairFollow;
    public bool dropped;

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

        stairFollow = false;
        dropped = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        pc.moveOK = false;

        int points = collision.contactCount;
        ContactPoint2D[] contacts = new ContactPoint2D[points];
        points = collision.GetContacts(contacts);

        if (collision.collider.gameObject.tag == "Floor" && !stairFollow)
        {
            float floorDisplace = box.bounds.center.y - box.bounds.extents.y;

            transform.position = new Vector3(transform.position.x, transform.position.y + contacts[0].point.y - floorDisplace - vSpeed, transform.position.z);

            grounded = true;
        }
        if (collision.collider.gameObject.tag == "Wall")
        {
            float displacement = 0;

            if (hSpeed > 0)
            {
                displacement = box.bounds.center.x + box.bounds.extents.x;
                transform.position = new Vector3(transform.position.x - (displacement - contacts[0].point.x) - hSpeed, transform.position.y, transform.position.z);
            }
            else if (hSpeed < 0)
            {
                displacement = box.bounds.center.x - box.bounds.extents.x;
                transform.position = new Vector3(transform.position.x + (contacts[0].point.x - displacement) - hSpeed, transform.position.y, transform.position.z);
            }
        }
        if (collision.collider.gameObject.tag == "Door")
        {
            if (!dashing)
            {
                float displacement = 0;

                if (hSpeed > 0)
                {
                    displacement = box.bounds.center.x + box.bounds.extents.x;
                    transform.position = new Vector3(transform.position.x - (displacement - contacts[0].point.x) - hSpeed, transform.position.y, transform.position.z);
                }
                else if (hSpeed < 0)
                {
                    displacement = box.bounds.center.x - box.bounds.extents.x;
                    transform.position = new Vector3(transform.position.x + (contacts[0].point.x - displacement) - hSpeed, transform.position.y, transform.position.z);
                }
            }
            else
            {
                Destroy(collision.collider.gameObject);
            }
        }
        if (collision.collider.gameObject.tag == "Stair" && !dropped) {
            StairProperties stair = collision.collider.gameObject.GetComponent<StairProperties>();
            grounded = true;

            yint = stair.yint;
            slope = stair.slope;
            LeftBound = stair.LeftBound;
            RightBound = stair.RightBound;

            if (transform.position.y < slope * transform.position.x + yint) {
                dropped = true;
            }
            else {
                stairFollow = true;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.gameObject.tag == "Stair") {
            stairFollow = false;

            if (dropped)
                dropped = false;
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

        if (Input.GetKey(KeyCode.LeftShift) && grounded && !dashOnCooldown && !dashing) {
            dashTimer = 0;
            dashing = true;
        }

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

        if (Input.GetKey(KeyCode.S)) {
            stairFollow = false;
            grounded = false;
            dropped = true;
        }

        // change position at the end
        if (stairFollow) {
            float calcx = transform.position.x + hSpeed;
            float calcy = slope * calcx + yint;

            // fix magic numbers
            if (calcx < LeftBound.x) {
                transform.position = new Vector3(calcx, LeftBound.y + box.bounds.extents.y, transform.position.z);
            }
            else if (calcx > RightBound.x) {
                transform.position = new Vector3(calcx, RightBound.y + box.bounds.extents.y, transform.position.z);
            }
            else {
                transform.position = new Vector3(calcx, calcy + box.bounds.extents.y, transform.position.z);
            }
        }
        else {
            transform.position = new Vector3(transform.position.x + hSpeed, transform.position.y + vSpeed, transform.position.z);
        }

        
    }
}
