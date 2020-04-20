using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatherWoodPanel : MonoBehaviour
{
    public void SetGatherWoodName(int min, int max)
    {
        GetComponentsInChildren<Text>()[0].text = "Gather Wood (" + min + "-" + max + ")";
    }

    public void SetGatherWoodLevel(int value)
    {
        GetComponentsInChildren<Text>()[1].text = "Level " + value;
    }

    public bool CheckForDie()
    {
        Transform[] diceTraySlots = GetComponentsInChildren<Transform>();
        foreach (Transform diceTraySlot in diceTraySlots)
        {
            if (diceTraySlot.CompareTag("SlotDice2"))
            {
                return HasChild(diceTraySlot);
            }
        }

        return false;
    }

    public void DestroyAllChildren()
    {
        Transform[] diceTraySlots = GetComponentsInChildren<Transform>();
        foreach (Transform diceTraySlot in diceTraySlots)
        {
            if (diceTraySlot.CompareTag("SlotDice2") && HasChild(diceTraySlot))
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
