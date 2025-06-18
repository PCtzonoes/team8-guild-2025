using System;
using DefaultNamespace.Events;
using DefaultNamespace.Powers;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TempPowerButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private MonoBehaviour powerUser; // temp until we have SystemManger to assign power in play.
    [SerializeField] private Button confirm;

    private IPower power;

    private void Start()
    {
        power = powerUser as IPower;
        if (power == null)
            Debug.LogError("Assigned component does not implement IPower.");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    
    public void UsePower()
    {
        GameEvents.UsePower(power);
        confirm.interactable = false;
    }
}
