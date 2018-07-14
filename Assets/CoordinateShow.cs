using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Show Unity coordinates
public class CoordinateShow : MonoBehaviour
{
    float cubeRectWidth = 230f;
    float cubeRectHeight = 145f;
    float mouseRectWidth = 240f;
    float mouseRectHeight = 90f;

    Vector2 cubeRectSize;
    Vector2 mouseRectSize;

    public Transform cubeTransform;

    // Use this for initialization
    void Start()
    {
        cubeRectSize = new Vector2(cubeRectWidth, cubeRectHeight);
        mouseRectSize = new Vector2(mouseRectWidth, mouseRectHeight);
    }

    void OnGUI()
    {
        ShowCubeCoord();
        ShowMouseCoord();
    }

    void ShowCubeCoord()
    {
        Vector3 cubeWorldCoord = cubeTransform.position;
        Vector3 cubeLocalCoord = cubeTransform.localPosition;
        Vector3 cubeScreenCoord = Camera.main.WorldToScreenPoint(cubeWorldCoord);
        Vector3 cubeViewportCoord = Camera.main.WorldToViewportPoint(cubeWorldCoord);

        Vector2 rectGUICoord = cubeScreenCoord;
        rectGUICoord.x -= cubeRectWidth / 2;
        rectGUICoord.y += cubeRectHeight;
        rectGUICoord.y = Screen.height - rectGUICoord.y;  // Screen coordinate to GUI coordinate

        GUILayout.BeginArea(new Rect(rectGUICoord, cubeRectSize));
        GUILayout.Label("World Coordinate: " + cubeWorldCoord);
        GUILayout.Label("Local Coordinate: " + cubeLocalCoord);
        GUILayout.Label("Screen Coordinate: " + cubeScreenCoord);
        GUILayout.Label("Viewport Coordinate: " + cubeViewportCoord);
        GUILayout.Label("GUI Coordinate: " + rectGUICoord);
        GUILayout.EndArea();
    }

    void ShowMouseCoord()
    {
        Vector3 mouseScreenCoord = Input.mousePosition;
        Vector3 mouseViewportCoord = Camera.main.ScreenToViewportPoint(mouseScreenCoord);

        // Mouse pointing spot in world space
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenCoord);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 mouseWorldCoord = hit.point;

        Vector2 rectGUICoord = mouseScreenCoord;
        rectGUICoord.y = Screen.height - mouseScreenCoord.y;  // Screen coordinate to GUI coordinate

        // Show the complete GUI area
        if (rectGUICoord.x < 0)
            rectGUICoord.x = 0;
        if (rectGUICoord.x > Screen.width - mouseRectWidth)
            rectGUICoord.x = Screen.width - mouseRectWidth;
        if (rectGUICoord.y < 0)
            rectGUICoord.y = 0;
        if (rectGUICoord.y > Screen.height - mouseRectHeight)
            rectGUICoord.y = Screen.height - mouseRectHeight;

        GUILayout.BeginArea(new Rect(rectGUICoord, mouseRectSize));
        GUILayout.Label("World Coordinate: " + mouseWorldCoord);
        GUILayout.Label("Screen Coordinate: " + mouseScreenCoord);
        GUILayout.Label("Viewport Coordinate: " + mouseViewportCoord);
        GUILayout.Label("GUI Coordinate: " + rectGUICoord);
        GUILayout.EndArea();
    }
}
