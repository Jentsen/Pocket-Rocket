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

        // if (objSelected == null)
        // {
        //     Debug.Log("object selected: " + objSelected);
        //     RaycastHit hit;
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //     {
        //         objSelected = hit.transform.gameObject;
        //     }
        // }
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
        for (int i = 0; i < snapPoints.Length; i++)
        {
            if (Vector3.Distance(snapPoints[i].transform.position, objSelected.transform.position) < snapSensitivity)
            {
                if (snapPoints[i].name == "o_Barrel" && objSelected.name == "Barrel")
                {
                    objSelected.transform.position = new Vector3(snapPoints[i].transform.position.x, snapPoints[i].transform.position.y, snapPoints[i].transform.position.z + 0.1f);
                    Debug.Log("barrel snapped");
                }
                else if (snapPoints[i].name == "o_Trigger" && objSelected.name == "Trigger")
                {
                    objSelected.transform.position = new Vector3(snapPoints[i].transform.position.x, snapPoints[i].transform.position.y, snapPoints[i].transform.position.z + 0.1f);
                }
                else if (snapPoints[i].name == "o_Spring" && objSelected.name == "Spring")
                {
                    objSelected.transform.position = new Vector3(snapPoints[i].transform.position.x, snapPoints[i].transform.position.y, snapPoints[i].transform.position.z + 0.1f);
                }
            }
        }

        objSelected = null;
    }

}
