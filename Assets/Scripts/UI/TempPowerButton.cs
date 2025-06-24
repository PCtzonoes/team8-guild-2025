using System;
using DefaultNamespace.Events;
using DefaultNamespace.Powers;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TempPowerButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private MonoBehaviour reaperPower; // temp until we have SystemManger to assign power in play.
    [SerializeField] private MonoBehaviour impPower; // temp until we have SystemManger to assign power in play.
    [SerializeField] private MonoBehaviour devilPower; // temp until we have SystemManger to assign power in play.
    [SerializeField] private Button ReaperButton;
    [SerializeField] private Button ImpButton;
    [SerializeField] private Button DevilButton;

    private IPower power1;
    private IPower power2;
    private IPower power3;

    private void Start()
    {
        power1 = reaperPower as IPower;
        power2 = impPower as IPower;
        power3 = devilPower as IPower;
        if (power1 == null)
            Debug.LogError("Assigned component does not implement IPower.");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    
    public void UsePower(int thisPower)
    {
        switch (thisPower) 
        {
            case 1:
                GameEvents.UsePower(power1);
                break;
            case 2:
                GameEvents.UsePower(power2);
                break;
            case 3:
                GameEvents.UsePower(power3);
                break;

        }
        //confirm.interactable = false;
    }
}
