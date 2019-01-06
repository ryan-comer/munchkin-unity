using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContextMenu : MonoBehaviour, IPointerExitHandler
{
    public GameObject target;

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(gameObject);
    }

    public void SendMessageToTarget(string message)
    {
        target.SendMessage(message);
    }

}
