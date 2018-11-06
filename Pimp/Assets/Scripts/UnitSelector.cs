using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.Networking;

public static class Utils
{
    static Texture2D _whiteTexture;
    public static Texture2D WhiteTexture
    {
        get
        {
            if (_whiteTexture == null)
            {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }

            return _whiteTexture;
        }
    }

    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, WhiteTexture);
        GUI.color = Color.white;
    }

    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // Top
        Utils.DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // Left
        Utils.DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // Right
        Utils.DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        // Bottom
        Utils.DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
    {
        var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }
}

public class UnitSelector : NetworkBehaviour
{

    bool isSelecting = false;
    Vector3 mousePosition1;

    public List<GameObject> selectedUnits = new List<GameObject>();

    void Update()
    {
        if (!isLocalPlayer)
            return;
        U_SelectUnit();
        // If we press the left mouse button, save mouse location and begin selection
        if (Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;
        }
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
        {
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("clickable"))
            {
                if (IsWithinSelectionBounds(obj) && CheckUnitFaction(obj))
                {
                    selectedUnits.Add(obj);
                    obj.GetComponent<UnitController>().Selected = true;
                }   
            }
            isSelecting = false;
        }
    }

    void OnGUI()
    {
        if (isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect(mousePosition1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds =
            Utils.GetViewportBounds(camera, mousePosition1, Input.mousePosition);

        return viewportBounds.Contains(
            camera.WorldToViewportPoint(gameObject.transform.position));
    }

    Ray ray;
    RaycastHit hit;

    float off;
    Vector3 offset;


    void U_SelectUnit()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 300))
        {
            Debug.DrawLine(Camera.main.transform.position,hit.point,Color.red);
            //Selecting unit
            if (hit.collider.tag == "clickable" && Input.GetMouseButtonUp(0))
            {
                GameObject unit = hit.collider.gameObject;
                selectedUnits.Add(unit);
                unit.GetComponent<UnitController>().Selected = true;
            }
            //moving unit
            if (Input.GetMouseButtonDown(1))
            {
                off = 0f;
                offset = new Vector3(off, off, off);
                foreach (GameObject obj in selectedUnits)
                {
                    //obj.GetComponent<NavMeshAgent>().SetDestination(hit.point + offset);
                    CmdMoveUnit(obj, hit.point);
                    off += 1f;
                    offset = new Vector3(off, off, off);
                    //obj.GetComponentInChildren<Animator>().SetBool("Running", true);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject obj in selectedUnits)
            {
                obj.GetComponent<UnitController>().Selected = false;
            }
            selectedUnits.Clear();
        }
    }
    //NETWORKING
    [Command]
    void CmdMoveUnit(GameObject obj,Vector3 dest)
    {
        obj.GetComponent<NavMeshAgent>().SetDestination(dest);
    }

    bool CheckUnitFaction(GameObject obj)
    {
        return obj.GetComponent<UnitController>().PlayerID == transform.name;
    }
}
