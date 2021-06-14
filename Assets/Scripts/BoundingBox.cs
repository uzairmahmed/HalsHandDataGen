using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoundingBox : MonoBehaviour
{
    public Camera camera;

    // In full version turn this into an array 
    // of every board piece and iterate through in code.

    public List<GameObject> pieces = new List<GameObject>();
    public float padding = 10f;
    void Start()
    {
        camera = Camera.main;
    }

    public void getBBCoords(){
        StartCoroutine(GetBoundingBoxCoords());
    }

    public IEnumerator GetBoundingBoxCoords(){
        foreach (GameObject piece in pieces)
        {
            // WORLD SPACE BOUNDS
            Bounds bounds = piece.GetComponentInChildren<Renderer>().bounds;

            // Map all 8 viewable corners into SCREEN SPACE BOUNDS
            Vector3[] SSCorners = new Vector3[8];
            SSCorners[0] = camera.WorldToScreenPoint(new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z + bounds.extents.z));
            SSCorners[1] = camera.WorldToScreenPoint(new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z - bounds.extents.z));
            SSCorners[2] = camera.WorldToScreenPoint(new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z + bounds.extents.z));
            SSCorners[3] = camera.WorldToScreenPoint(new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z - bounds.extents.z));
            SSCorners[4] = camera.WorldToScreenPoint(new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z + bounds.extents.z));
            SSCorners[5] = camera.WorldToScreenPoint(new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z - bounds.extents.z));
            SSCorners[6] = camera.WorldToScreenPoint(new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z + bounds.extents.z));
            SSCorners[7] = camera.WorldToScreenPoint(new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z - bounds.extents.z));

            // Save 4 min/max X and Y for screen space bounds (there is no Z axis in 2D)
            float min_x = SSCorners[0].x;
            float min_y = SSCorners[0].y;
            float max_x = SSCorners[0].x;
            float max_y = SSCorners[0].y;

            for (int i = 1; i < 8; i++)
            {
                if (SSCorners[i].x < min_x) min_x = SSCorners[i].x;
                if (SSCorners[i].y < min_y) min_y = SSCorners[i].y;
                if (SSCorners[i].x > max_x) max_x = SSCorners[i].x;
                if (SSCorners[i].y > max_y) max_y = SSCorners[i].y;
            }

            // Move and Size the Bounding Box to outline the object, Disable the renderer for this or 
            // dont look at UI elements in the image capture

            RectTransform rt = GetComponent<RectTransform>();
            rt.position = new Vector2(min_x - padding, min_y - padding);
            rt.sizeDelta = new Vector2(max_x - min_x + (padding * 2), max_y - min_y + (padding * 2));

            // Prints Name, Bottom Left Corner, and Size Vector.
            Debug.Log(piece.name + ": Bottom Left Point: " + rt.position + ", Bounding Box Size: " + rt.sizeDelta);
            
            // Slow it down a bit for debug (?)
            yield return new WaitForSeconds(0.1f);
        }
    }
}