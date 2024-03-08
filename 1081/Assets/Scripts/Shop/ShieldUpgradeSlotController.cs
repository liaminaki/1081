using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUpgradeSlotController : MonoBehaviour
{
    [SerializeField] private List<ShieldSlots> _filledSlots;
    private bool shieldUpgrade = false;

    public void Start()
    {
        IterateSlots();
    }
    public void AddToList(ShieldSlots slots)
    {

        if (_filledSlots == null){
            _filledSlots = new List<ShieldSlots>();
        }
        _filledSlots.Add(slots);
    }

    public void IterateSlots()
    {
        if (_filledSlots != null)
        {
            int count = 5;
            if (shieldUpgrade)
            {
                Debug.Log("nisud here para mo upgrade");
                for (int i = 0; i < count; i++)
                {
                    if (i < PlayerPrefs.GetInt("ShieldLevel"))
                    {
                        _filledSlots[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        _filledSlots[i].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (i < PlayerPrefs.GetInt("ShieldLevel"))
                    {
                        _filledSlots[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        _filledSlots[i].gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            Debug.Log("List of filled slots is null or empty.");
        }
    }

    public void SetWasUpgradeShield(bool value)
    {
        this.shieldUpgrade = value;
        if (shieldUpgrade)
        {
            Debug.Log("nitrue siya");
        }
        else
        {
            Debug.Log("false siya");
        }
    }
}
