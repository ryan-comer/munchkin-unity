using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCardDetails : MonoBehaviour
{

    public Card cardToShow;

    public void ShowDetails()
    {
        UIController.instance.ShowCardDetails(cardToShow);
    }

    public void HideDetails()
    {
        UIController.instance.HideCardDetails();
    }

}
