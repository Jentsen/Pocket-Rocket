using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    GameObject objSelected = null;

    public GameObject[] snapPoints;
    public float snapSensitivity = 2.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //check if obj is clicked
            CheckHitObject();
        }

        if (Input.GetMouseButton(0) && objSelected != null)
        {
            //drag obj
            DragObject();
        }

        if (Input.GetMouseButtonUp(0) && objSelected != null)
        {
            //drop obj
            DropObject();
        }

    }

    void CheckHitObject()
    {
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        //check if the obj is hit by the ray
        if (hit2D.collider != null)
        {
            objSelected = hit2D.transform.gameObject;
        }

    }

    void DragObject()
    {
        //get obj position and set to where mouse is
        objSelected.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 10.0f));

        if (Input.GetMouseButtonDown(1))
        {
            objSelected.transform.Rotate(0, 0, 90);
        }
    }

    void DropObject()
    {
        // Temporarily set the object to a layer where it doesn't collide with the snap points.
        int originalLayer = objSelected.layer;
        objSelected.layer = LayerMask.NameToLayer("Ignore Raycast");

        for (int i = 0; i < snapPoints.Length; i++)
        {
            if (IsObjectSnappable(objSelected, snapPoints[i]))
            {
                // Temporarily disable collisions between the object and the snap point.
                Physics2D.IgnoreCollision(objSelected.GetComponent<Collider2D>(), snapPoints[i].GetComponent<Collider2D>(), true);

                // Check for collisions.
                if (objSelected.GetComponent<Collider2D>().IsTouching(snapPoints[i].GetComponent<Collider2D>()))
                {
                    SnapObjectToSnapPoint(objSelected, snapPoints[i]);
                    break; // Stop checking snap points after successful snap.
                }

                // Re-enable collisions.
                Physics2D.IgnoreCollision(objSelected.GetComponent<Collider2D>(), snapPoints[i].GetComponent<Collider2D>(), false);
            }
        }

        // Restore the original layer.
        objSelected.layer = originalLayer;
        objSelected = null;
    }

    bool IsObjectSnappable(GameObject obj, GameObject snapPoint)
    {
        if (obj == null || snapPoint == null)
        {
            return false;
        }

        string snapPointName = snapPoint.name;
        string objectName = obj.name;

        // Define the maximum allowed Z rotation difference (in degrees) for a valid snap.
        float maxZRotationDifference = 5.0f;

        // Check if the object's Z rotation is within the specified range.
        bool isZRotationValid = Mathf.Abs(obj.transform.rotation.eulerAngles.z) <= maxZRotationDifference;

        // Check if the snap point's name matches the correct format and if Z rotation is valid.
        bool isSnappable = snapPointName.StartsWith("o_" + objectName) && isZRotationValid;

        Debug.Log("Object: " + objectName + ", Snap Point: " + snapPointName + ", Is Snappable: " + isSnappable);

        return isSnappable;
    }

    void SnapObjectToSnapPoint(GameObject obj, GameObject snapPoint)
    {
        obj.transform.position = new Vector3(snapPoint.transform.position.x, snapPoint.transform.position.y, snapPoint.transform.position.z - 0.1f);

        Debug.Log(obj.name + " snapped to " + snapPoint.name);
    }
}