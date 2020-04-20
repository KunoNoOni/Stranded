using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSignalFirePanel : MonoBehaviour
{
    public Sprite[] diceImages;

    public void SetWoodRequiredActiveStatus(bool value)
    {
        GetComponentsInChildren<Text>()[2].gameObject.SetActive(value);
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

        return numberOfDice == 4;
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

    public void GenerateRandomDiceImages()
    {
        Image[] currentDiceImages = GetComponentsInChildren<Image>();

        foreach (Image currentDiceImage in currentDiceImages)
        {
            switch(currentDiceImage.name)
            {
                case "RandomDice1":
                    GenerateNewDieImage(currentDiceImage);
                    break;
                case "RandomDice2":
                    GenerateNewDieImage(currentDiceImage);
                    break;
                case "RandomDice3":
                    GenerateNewDieImage(currentDiceImage);
                    break;
                case "RandomDice4":
                    GenerateNewDieImage(currentDiceImage);
                    break;
            }
        }
    }

    private void GenerateNewDieImage(Image currentDiceImage)
    {
        Sprite newDieImage = GetRandomDie();

        currentDiceImage.sprite = newDieImage;
        switch (currentDiceImage.name)
        {
            case "RandomDice1":
                SetTagForMatchingSlot(currentDiceImage.name, newDieImage.name);
                break;
            case "RandomDice2":
                SetTagForMatchingSlot(currentDiceImage.name, newDieImage.name);
                break;
            case "RandomDice3":
                SetTagForMatchingSlot(currentDiceImage.name, newDieImage.name);
                break;
            case "RandomDice4":
                SetTagForMatchingSlot(currentDiceImage.name, newDieImage.name);
                break;
        }
    }

    private void SetTagForMatchingSlot(string currentDiceImageName, string newDieImageName)
    {
        string diceTageName = newDieImageName.Replace("_", "").Substring(0, 5);
        Image[] images = GetComponentsInChildren<Image>();
        foreach(Image image in images)
        {
            if (image.name.EndsWith("1") && currentDiceImageName.EndsWith("1"))
            {
                image.tag = "Slot" + diceTageName;
            }

            if (image.name.EndsWith("2") && currentDiceImageName.EndsWith("2"))
            {
                image.tag = "Slot" + diceTageName;
            }

            if (image.name.EndsWith("3") && currentDiceImageName.EndsWith("3"))
            {
                image.tag = "Slot" + diceTageName;
            }

            if (image.name.EndsWith("4") && currentDiceImageName.EndsWith("4"))
            {
                image.tag = "Slot" + diceTageName;
            }
        }
    }

    public Sprite GetRandomDie()
    {
        int randomNumber = Random.Range(0, 5);
        return diceImages[randomNumber];
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
