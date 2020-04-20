using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuntPanel : MonoBehaviour
{
    public void SetHuntName(int min, int max)
    {
        GetComponentsInChildren<Text>()[0].text = "Hunt (" + min + "-" + max + ")";
    }

    public void SetHuntLevel(int value)
    {
        GetComponentsInChildren<Text>()[1].text = "Level " + value;
    }


    public bool CheckForDie()
    {
        Transform[] diceTraySlots = GetComponentsInChildren<Transform>();
        int numberOfDice = 0;

        foreach (Transform diceTraySlot in diceTraySlots)
        {
            if (diceTraySlot.tag.Contains("SlotDice") && HasChild(diceTraySlot))
            {
                numberOfDice++;
            }
        }

        return numberOfDice == 3;
    }

    public void DestroyAllChildren()
    {
        Transform[] diceTraySlots = GetComponentsInChildren<Transform>();
        foreach (Transform diceTraySlot in diceTraySlots)
        {
            if (diceTraySlot.tag.Contains("SlotDice") && HasChild(diceTraySlot))
            {
                DestroyChild(diceTraySlot);
            }
        }
    }

    private void DestroyChild(Transform diceTraySlot)
    {
        GameObject badChild = diceTraySlot.GetChild(0).gameObject;
        Destroy(badChild);
    }

    private bool HasChild(Transform diceTraySlot)
    {
        return diceTraySlot.childCount > 0;
    }
}
