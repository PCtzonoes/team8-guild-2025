using UnityEngine;
using UnityEngine.UIElements;

public class UIRoot : MonoBehaviour
{
    protected UIDocument _uiDoc;

    protected VisualElement _root;

    protected void Start()
    {
        _uiDoc = GetComponent<UIDocument>();
        _root = _uiDoc.rootVisualElement.Q("root");
    }

    public void ToggleActivate(bool isActive)
    {
        if (isActive == true)
        {
            _root.RemoveFromClassList("root-inactive");
        }
        else
        {
            _root.AddToClassList("root-inactive");
        }
    }
}
