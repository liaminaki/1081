using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyGlass : MonoBehaviour
{
    private Camera magnifyCamera;
    private RenderTexture renderTexture;
    public GameObject magnifierSprite; // Reference to the magnifier sprite
    public GameObject circleObject; // Reference to the circle object where the content should be zoomed

    void Start()
    {
        CreateMagnifyGlass();
    }

    void Update()
    {
        UpdateMagnifier();
    }

    private void CreateMagnifyGlass()
    {
        // Create the magnify camera
        GameObject cameraObject = new GameObject("MagnifyCamera");
        magnifyCamera = cameraObject.AddComponent<Camera>();
        magnifyCamera.orthographic = true;

        // Set the orthographic size to a smaller value to achieve the zoom effect
        magnifyCamera.orthographicSize = Camera.main.orthographicSize / 10.0f; // Adjust this factor to get the desired zoom level

        magnifyCamera.clearFlags = CameraClearFlags.SolidColor;
        magnifyCamera.backgroundColor = Color.clear;
        magnifyCamera.cullingMask = LayerMask.GetMask("Book"); // Ensure your book content is in the "Book" layer

        // Set the render texture size (adjust as needed for quality vs performance)
        renderTexture = new RenderTexture(Screen.width / 5, Screen.height / 5, 16);
        magnifyCamera.targetTexture = renderTexture;

        // Set up the magnifier sprite
        if (magnifierSprite != null)
        {
            SpriteRenderer sr = magnifierSprite.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.material = new Material(Shader.Find("Unlit/Transparent")); // Use Unlit/Transparent shader
                sr.material.mainTexture = renderTexture;
            }
        }

        // Set up the circle object as a mask
        if (circleObject != null)
        {
            SpriteMask mask = circleObject.GetComponent<SpriteMask>();
            if (mask == null)
            {
                mask = circleObject.AddComponent<SpriteMask>();
            }
            mask.sprite = circleObject.GetComponent<SpriteRenderer>().sprite;
            circleObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void UpdateMagnifier()
    {
        // Update the camera and sprite positions based on the mouse position
        Vector3 mousePos = GetWorldPosition(Input.mousePosition);
        magnifyCamera.transform.position = new Vector3(mousePos.x, mousePos.y, -10); // Adjust the z-axis value to match the scene setup

        if (magnifierSprite != null)
        {
            magnifierSprite.transform.position = mousePos;
        }

        if (circleObject != null)
        {
            circleObject.transform.position = mousePos;
        }
    }

    // Method to calculate world position from screen position
    private Vector3 GetWorldPosition(Vector3 screenPos)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, -Camera.main.transform.position.z));
    }
}
