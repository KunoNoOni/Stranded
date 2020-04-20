using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RerollButton : MonoBehaviour
{
    public GameObject rerollButton;

    public bool RerollDiceInDiceTray(GameObject diceTray)
    {
        Transform[] diceTraySlots = diceTray.GetComponentsInChildren<Transform>();
        bool rerolledADie = false;

        foreach (Transform diceTraySlot in diceTraySlots)
        {
            if (diceTraySlot.CompareTag("SlotDiceTray") && HasChild(diceTraySlot))
            {
                DestroyChild(diceTraySlot);
                GameObject die = diceTray.GetComponent<DiceTrayPanel>().GetRandomDie();
                GameObject go = Instantiate(die);
                go.transform.SetParent(diceTraySlot);
                go.transform.position = diceTraySlot.position;
                rerolledADie = true;
            }
        }

        return rerolledADie;
    }

    private bool HasChild(Transform diceTraySlot)
    {
        return diceTraySlot.childCount > 0;
    }

    private void DestroyChild(Transform diceTraySlot)
    {
        GameObject badChild = diceTraySlot.GetChild(0).gameObject;
        Destroy(badChild);
    }

    public void SetButtonDisableStatus(bool value)
    {
        rerollButton.GetComponentInChildren<Button>().interactable = value;
    }

    
}
