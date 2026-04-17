using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonForceTest : MonoBehaviour, IPointerClickHandler
{
    void Start()
    {
        Debug.Log("ButtonForceTest READY on: " + gameObject.name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(">>> DIRECT POINTER CLICK DETECTED on button!");
    }
}