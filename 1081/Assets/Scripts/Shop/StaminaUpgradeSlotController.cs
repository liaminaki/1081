using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUpgradeSlotController : MonoBehaviour
{
    [SerializeField] private List<StaminaSlots> _filledSlots;
    private bool staminaUpgrade = false;

    public void Start()
    {
        IterateSlots();
    }

    public void Update()
    {
        IterateSlots();
    }
    public void AddToList(StaminaSlots slots)
    {

        if (_filledSlots == null)
        {
            _filledSlots = new List<StaminaSlots>();
        }
        _filledSlots.Add(slots);
    }

    public void IterateSlots()
    {
        if (_filledSlots != null)
        {
            if (PlayerPrefs.GetInt("StaminaLevel") >= 5)
            {
                Debug.Log("Maximum Level Reached");
            }
            else
            {
                int count = 5;
                if (staminaUpgrade)
                {
                    Debug.Log("nisud siya para mo upgrade");
                    for (int i = 0; i < count; i++)
                    {
                        if (i < PlayerPrefs.GetInt("StaminaLevel"))
                        {
                            _filledSlots[i].gameObject.SetActive(false);
                        }
                        else
                        {
                            _filledSlots[i].gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i < PlayerPrefs.GetInt("StaminaLevel"))
                        {
                            _filledSlots[i].gameObject.SetActive(false);
                        }
                        else
                        {
                            _filledSlots[i].gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("List of filled slots is null or empty.");
        }
    }
    public void SetWasUpgradeStamina(bool value)
    {
        this.staminaUpgrade = value;
    }
}
