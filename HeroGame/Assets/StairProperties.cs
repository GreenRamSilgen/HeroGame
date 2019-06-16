using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairProperties : MonoBehaviour
{
    public float yint;
    public float slope;

    public Vector2 LeftBound;
    public Vector2 RightBound;

    void Start()
    {
        PolygonCollider2D poly = gameObject.GetComponent<PolygonCollider2D>();

        float dx = 0;
        float dy = 0;
        Vector2 slant = new Vector2();

        // works only for triangulars
        // determines where slope is by slope comparison
        for (int i = 0; i < poly.points.Length - 1; i++) {
            dx = poly.points[i + 1].x - poly.points[i].x;
            dy = poly.points[i + 1].y - poly.points[i].y;

            slant = new Vector2(dx, dy);
            slant.Normalize();

            if (!(Mathf.Abs(slant.x) < 0.1 || Mathf.Abs(slant.x) > 0.9)) {
                slope = slant.y / slant.x;
                yint = poly.points[i].y - slope * poly.points[i].x;

                if (poly.points[i].x < poly.points[i + 1].x) {
                    LeftBound.x = poly.points[i].x;
                    LeftBound.y = poly.points[i].y;

                    RightBound.x = poly.points[i + 1].x;
                    RightBound.y = poly.points[i + 1].y;
                }
                else {
                    LeftBound.x = poly.points[i + 1].x;
                    LeftBound.y = poly.points[i + 1].y;

                    RightBound.x = poly.points[i].x;
                    RightBound.y = poly.points[i].y;
                }
            }
        }

        int plen = poly.points.Length - 1;

        // checking last slant
        dx = poly.points[plen].x - poly.points[0].x;
        dy = poly.points[plen].y - poly.points[0].y;

        slant = new Vector2(dx, dy);
        slant.Normalize();

        if (!(Mathf.Abs(slant.x) < 0.1 || Mathf.Abs(slant.x) > 0.9)) {
            slope = slant.y / slant.x;
            yint = poly.points[0].y - slope * poly.points[0].x;

            if (poly.points[plen].x < poly.points[0].x) {
                LeftBound.x = poly.points[plen].x;
                LeftBound.y = poly.points[plen].y;

                RightBound.x = poly.points[0].x;
                RightBound.y = poly.points[0].y;
            }
            else {
                LeftBound.x = poly.points[0].x;
                LeftBound.y = poly.points[0].y;

                RightBound.x = poly.points[plen].x;
                RightBound.y = poly.points[plen].y;
            }
        }
    }
}
