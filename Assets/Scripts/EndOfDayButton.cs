using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfDayButton : MonoBehaviour
{
    public GameObject endOfDayButton;

    public void SetButtonDisableStatus(bool value)
    {
        endOfDayButton.GetComponent<Button>().interactable = value;
    }
}
