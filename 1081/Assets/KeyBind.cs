using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyBind : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public Sprite selectedSprite;

    private Image image;
    private bool isSelected = false;
    private Vector3 originalScale;
    private static KeyBind currentlySelected;

    private void Start()
    {
        // Try to get the Image component
        image = GetComponent<Image>();

        // Check if the Image component is missing
        if (image == null)
        {
            // Log an error message
            Debug.LogError("HoverAndSelect script requires an Image component on the GameObject.");

            // Attempt to add the Image component if missing
            image = gameObject.AddComponent<Image>();

            // Check if adding the Image component was successful
            if (image == null)
            {
                // Log an additional error message and disable the script
                Debug.LogError("Failed to add Image component. Make sure an Image component is attached to the GameObject.");
                enabled = false;
                return;
            }

            // Set the initial sprite to normalSprite
            image.sprite = normalSprite;
        }
        else
        {
            // Set the initial sprite to normalSprite if the Image component is present
            image.sprite = normalSprite;
        }

        // Store the original scale for later use
        originalScale = transform.localScale;

        // Select the first image by default
        if (currentlySelected == null)
        {
            SelectImage();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            // Change the sprite to hoverSprite when the mouse enters
            image.sprite = hoverSprite;
            transform.localScale = originalScale * 1.05f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            // Change the sprite back to normalSprite when the mouse exits
            image.sprite = normalSprite;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Change the sprite to selectedSprite when the GameObject is clicked
        // image.sprite = selectedSprite;
        if (currentlySelected != null && currentlySelected != this)
        {
            currentlySelected.DeselectImage();
        }
        // Set isSelected to true to indicate that the object has been selected
        // isSelected = true;
        SelectImage();
        // Increase the scale to make the object a bit bigger
        // transform.localScale = originalScale * 1.05f; // Adjust the scale factor as needed
    }

      private void SelectImage()
    {
        // Change the sprite to selectedSprite
        image.sprite = selectedSprite;

        // Set isSelected to true to indicate that the object has been selected
        isSelected = true;

        // Increase the scale to make the object a bit bigger
        transform.localScale = originalScale * 1.05f; // Adjust the scale factor as needed

        // Update the currently selected instance
        currentlySelected = this;
    }

    private void DeselectImage()
    {
        // Change the sprite back to normalSprite
        image.sprite = normalSprite;

        // Set isSelected to false
        isSelected = false;

        // Reset the scale to the original scale
        transform.localScale = originalScale;
    }
}
