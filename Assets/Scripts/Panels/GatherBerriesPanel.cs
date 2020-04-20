using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatherBerriesPanel : MonoBehaviour
{
    public void SetGatherBerriesName(int min, int max)
    {
        GetComponentsInChildren<Text>()[0].text = "Gather Berries (" + min + "-" + max + ")";
    }

    public void SetGatherBerriesLevel(int value)
    {
        GetComponentsInChildren<Text>()[1].text = "Level " + value;
    }

    public bool CheckForDie()
    {
        Transform[] diceTraySlots = GetComponentsInChildren<Transform>();
        foreach (Transform diceTraySlot in diceTraySlots)
        {
            if (diceTraySlot.CompareTag("SlotDice1"))
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
            if (diceTraySlot.CompareTag("SlotDice1") && HasChild(diceTraySlot))
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
