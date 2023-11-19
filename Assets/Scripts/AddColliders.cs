using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddColliders : MonoBehaviour
{
    private EdgeCollider2D edge;
    private Vector2[] edgePoints;

    void Awake()
    {
        Camera camera = Camera.main;
        edge = GetComponent<EdgeCollider2D>();
        edgePoints = new Vector2[5];
        edgePoints[0] = camera.ScreenToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        edgePoints[2] = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth,camera.pixelHeight,camera.nearClipPlane));
        edgePoints[1] = new Vector2(edgePoints[0].x,edgePoints[2].y);
        edgePoints[3] = new Vector2(edgePoints[2].x, edgePoints[0].y);
        edgePoints[4] = edgePoints[0];
        edge.points = edgePoints;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" || other.tag == "Enemy Bullet")
        {
            Destroy(other.gameObject);
        }
    }
}
