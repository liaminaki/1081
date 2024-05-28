using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Magnify : MonoBehaviour, IPointerClickHandler
{
    private bool isFollowingCursor = false;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

         // Access the Image component of the parent GameObject
        Image image = GetComponent<Image>();

        // Set alphaHitTestMinimumThreshold to a lower value
        image.alphaHitTestMinimumThreshold = 0.001f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isFollowingCursor = false;
            return;
        }

        // If the magnifier should follow the cursor, update its position
        if (isFollowingCursor)
        {
            Vector3 worldPos = GetWorldPosition(Input.mousePosition);
            transform.position = worldPos;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Toggle following state
        isFollowingCursor = !isFollowingCursor;
    }

    private Vector3 GetWorldPosition(Vector3 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0; // Set Z to 0 to ensure the object stays in the 2D plane
        return worldPosition;
    }
}
