using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceTrayPanel : MonoBehaviour
{
    public GameObject[] dice;
    
    public void RandomizeDiceTray()
    {
        Transform[] diceTraySlots = GetComponentsInChildren<Transform>();

        foreach(Transform diceTraySlot in diceTraySlots)
        {
            if (diceTraySlot.CompareTag("SlotDiceTray"))
            {
                GameObject die = GetRandomDie();
                GameObject go = Instantiate(die);
                go.transform.SetParent(diceTraySlot);
                go.transform.position = diceTraySlot.position;
            }
        }
    }

    public GameObject GetRandomDie()
    {
        int randomNumber = Random.Range(0, 6);
        return dice[randomNumber];
    }

    public void DestroyAllChildren()
    {
        Transform[] diceTraySlots = GetComponentsInChildren<Transform>();
        foreach (Transform diceTraySlot in diceTraySlots)
        {
            if (diceTraySlot.CompareTag("SlotDiceTray") && HasChild(diceTraySlot))
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
