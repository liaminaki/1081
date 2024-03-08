using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ShieldSlots : MonoBehaviour
{
    [SerializeField] private GameObject _slotReference; // Reference to the GameObject representing the slot
    [SerializeField] private ShieldUpgradeSlotController _slots;

    // Start is called before the first frame update
    void Start()
    {
        //_slots.AddToList(this);
    }

    // Constructor
}
