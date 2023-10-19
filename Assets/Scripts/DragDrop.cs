using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    GameObject objSelected = null;

    public Dictionary<string, GameObject> snapPoints = new Dictionary<string, GameObject>();
    public float snapSensitivity = 2.0f;

    void Start()
    {
        // Fill the snapPoints dictionary with your outlines.
        snapPoints["Barrel"] = GameObject.Find("o_Barrel");
        snapPoints["Trigger"] = GameObject.Find("o_Trigger");
        snapPoints["Spring"] = GameObject.Find("o_Spring");
    }
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

        if (objSelected == null)
        {
            Debug.Log("object selected: " + objSelected);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                objSelected = hit.transform.gameObject;
            }
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

    // void DropObject()
    // {        
    //     //snap code
    //     for(int i = 0; i < snapPoints.Length; i++)
    //     {

    //         if(Vector3.Distance(snapPoints[i].transform.position, objSelected.transform.position) < snapSensitivity)
    //         {
    //             if(snapPoints[i].name == "barrel_outline")
    //             {
    //                 objSelected.transform.position = new Vector3(snapPoints[i].transform.position.x, snapPoints[i].transform.position.y, snapPoints[i].transform.position.z + 0.1f);
    //             }
    //             else
    //             {
    //                 objSelected.transform.position = new Vector3(snapPoints[i].transform.position.x, snapPoints[i].transform.position.y, snapPoints[i].transform.position.z + 0.1f);
    //             }
    //         }
    //     }
    //     objSelected = null;
    // }
    void DropObject()
    {
        string objectName = objSelected.name;
        string outlineName;

        if (objectToOutlineMapping.TryGetValue(objectName, out outlineName))
        {
            for (int i = 0; i < snapPoints.Length; i++)
            {
                if (snapPoints[i].name == outlineName)
                {
                    float distance = Vector3.Distance(snapPoints[i].transform.position, objSelected.transform.position);

                    if (distance < snapSensitivity)
                    {
                        objSelected.transform.position = new Vector3(
                            snapPoints[i].transform.position.x,
                            snapPoints[i].transform.position.y,
                            snapPoints[i].transform.position.z + 0.1f
                        );

                    }
                }
            }
        }

        objSelected = null;
    }

}
