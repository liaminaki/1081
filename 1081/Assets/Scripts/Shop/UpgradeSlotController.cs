using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSlotController : MonoBehaviour
{
    [SerializeField] private GameObject _upgradeSlot;
    [SerializeField] private GameObject _filledSlot;

    // Start is called before the first frame update

    // Update is called once per frame
    public void Activate()
    {
        if (_upgradeSlot != null)
        {
            _filledSlot.SetActive(true);
            Debug.Log(_filledSlot.name + " Filled Slot Activated");
            _upgradeSlot.SetActive(false);
            Debug.Log(_upgradeSlot.name + " Unfilled Slot Deactivated");
        }
    }
    public void Deactivate()
    {
        if (_upgradeSlot != null)
        {
            _filledSlot.SetActive(false);
            Debug.Log(_filledSlot.name + " Filled Slot Deactivated");
            _upgradeSlot.SetActive(true);
            Debug.Log(_upgradeSlot.name + " Unfilled Slot Activated");
        }
    }
}
