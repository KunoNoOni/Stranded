using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private MusicManager mm;
    private SoundManager sm;

    public GameObject taskGatherWood;
    public GameObject taskHunt;
    public GameObject taskGatherBerries;
    public GameObject taskCollectWater;
    public GameObject taskBuildSignalFire;
    public GameObject taskSignalShip;
    public GameObject taskScavage;
    public GameObject taskScavagePopupWood;
    public GameObject taskScavagePopupHunt;
    public GameObject taskScavagePopupBerries;
    public GameObject taskScavagePopupWater;
    public GameObject taskScavagePopupScavage;
    public GameObject diceTray;
    public GameObject resourcesAvailable;
    public GameObject requiredPerDay;
    public GameObject daysWithout;
    public GameObject endOfDayButton;
    public GameObject rerollButton;
    public GameObject signalFire;
    public GameObject signalFireLit;
    public Text dayTextField;
    public Text rerollsLeft;


    private int resourceWood = 5;
    private int resourceFood = 5;
    private int resourceWater = 5;
    private int requiredFood = 1;
    private int requiredWater = 1;
    private int numberOfDaysOnIsland = 1;
    private int scavageChance = 40;
    private int rescueShipChance = 40;
    private int taskGatherWoodLevel = 1;
    private int taskHuntLevel = 1;
    private int taskGatherBerriesLevel = 1;
    private int taskCollectWaterLevel = 1;
    private int numberOfRerollsLeft = 3;
    private int daysWithoutFood = 0;
    private int daysWithoutWater = 0;
    private int signalFireWoodCost = 25;

    private bool isSignalFireBuilt = false;

    private void Awake()
    {
        mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Start()
    {
        if (mm.music.Length > 0)
        {
            mm.PlaySound(mm.music[1]);
        }

        taskGatherWood.GetComponent<GatherWoodPanel>().SetGatherWoodLevel(taskGatherWoodLevel);
       
        taskHunt.GetComponent<HuntPanel>().SetHuntLevel(taskHuntLevel);
        taskGatherBerries.GetComponent<GatherBerriesPanel>().SetGatherBerriesLevel(taskGatherBerriesLevel);
        taskCollectWater.GetComponent<CollectWaterPanel>().SetCollectWaterLevel(taskCollectWaterLevel);
        SetResourcesAvailableWood(GetResourcesAvailableWood());
        SetResourcesAvailableWoodText(GetResourcesAvailableWood());
        SetResourcesAvailableFood(GetResourcesAvailableFood());
        SetResourcesAvailableFoodText(GetResourcesAvailableFood());
        SetResourcesAvailableWater(GetResourcesAvailableWater());
        SetResourcesAvailableWaterText(GetResourcesAvailableWater());
        SetDaysWithoutFood(GetDaysWithoutFood());
        SetDaysWithoutFoodText(GetDaysWithoutFood());
        SetDaysWithoutWater(GetDaysWithoutWater());
        SetDaysWithoutWaterText(GetDaysWithoutWater());
        SetNumberOfDaysOnIsland(numberOfDaysOnIsland);
        SetDayText(numberOfDaysOnIsland);
        SetRequiredPerDayFood(GetRequiredPerDayFood());
        SetRequiredPerDayFoodText(GetRequiredPerDayFood());
        SetRequiredPerDayWater(GetRequiredPerDayWater());
        SetRequiredPerDayWaterText(GetRequiredPerDayWater());
        SpawnNewDiceInDiceTray();
        SetRerollsLeftText(numberOfRerollsLeft);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mm.MuteMusic(true);
        }
    }

    public void ProcessRerollButton()
    {
        sm.PlaySound(sm.sounds[1]);
        bool decreaseRerollsLeft = GetComponent<RerollButton>().RerollDiceInDiceTray(diceTray);

        if (decreaseRerollsLeft && numberOfRerollsLeft > 0)
        {
            numberOfRerollsLeft--;
            SetRerollsLeftText(numberOfRerollsLeft);
        }

        if (numberOfRerollsLeft == 0)
        {
            GetComponent<RerollButton>().SetButtonDisableStatus(false);
        }
    }

    public void ProcessEndOfDayButton()
    {
        Debug.Log("Processing End of Day button...");
        sm.PlaySound(sm.sounds[1]);
        ResetRerollButton();
        DestroyAllDiceInDiceTray();
        CheckResourcePanels();
        CheckScavagePanel();
        CheckBuildSignalFire();
        CheckResources();
        SpawnNewDiceInDiceTray();
    }

    private void SetResourcesAvailableWood(int value)
    {
        resourceWood = value;
    }

    private void SetResourcesAvailableWoodText(int value)
    {
        resourcesAvailable.GetComponentsInChildren<Text>()[1].text = "Wood: " + value;
    }

    private int GetResourcesAvailableWood()
    {
        return resourceWood;
    }

    private void SetResourcesAvailableFood(int value)
    {
        resourceFood = value;
    }

    private void SetResourcesAvailableFoodText(int value)
    {
        resourcesAvailable.GetComponentsInChildren<Text>()[2].text = "Food: " + value;
    }

    private int GetResourcesAvailableFood()
    {
        return resourceFood;
    }

    private void SetResourcesAvailableWater(int value)
    {
        resourceWater = value;
    }

    private void SetResourcesAvailableWaterText(int value)
    {
        resourcesAvailable.GetComponentsInChildren<Text>()[3].text = "Water: " + value;
    }

    private int GetResourcesAvailableWater()
    {
        return resourceWater;
    }

    private void SetNumberOfDaysOnIsland(int value)
    {
        numberOfDaysOnIsland = value;
    }

    private int GetNumberOfDaysOnIsland()
    {
        return numberOfDaysOnIsland;
    }

    private void SetDayText(int value)
    {
        dayTextField.text = "Days On The Island: " + GetNumberOfDaysOnIsland();
    }

    private void SetRequiredPerDayFood(int value)
    {
        requiredFood = value;
    }

    private void SetRequiredPerDayFoodText(int value)
    {
        requiredPerDay.GetComponentsInChildren<Text>()[1].text = "Food: " + value;
    }

    private int GetRequiredPerDayFood()
    {
        return requiredFood;
    }

    private void SetRequiredPerDayWater(int value)
    {
        requiredWater = value;
    }

    private void SetRequiredPerDayWaterText(int value)
    {
        requiredPerDay.GetComponentsInChildren<Text>()[2].text = "Water: " + value;
    }

    private int GetRequiredPerDayWater()
    {
        return requiredWater;
    }

    private void SetDaysWithoutFood(int value)
    {
        daysWithoutFood = value;
    }

    private void SetDaysWithoutFoodText(int value)
    {
        daysWithout.GetComponentsInChildren<Text>()[1].text = "Food: " + value;   
    }

    private int GetDaysWithoutFood()
    {
        return daysWithoutFood;
    }

    private void SetDaysWithoutWater(int value)
    {
        daysWithoutWater = value;
    }

    private void SetDaysWithoutWaterText(int value)
    {
        daysWithout.GetComponentsInChildren<Text>()[2].text = "Water: " + value;
    }

    private int GetDaysWithoutWater()
    {
        return daysWithoutWater;
    }

    private void SetRerollsLeftText(int value)
    {
        rerollsLeft.GetComponentInChildren<Text>().text = "Rerolls Left: " + value;
    }

    private void SetRerollsLeft(int value)
    {
        numberOfRerollsLeft = value;
    }

    private int GetRandomNumber(int min, int max)
    {
        int randomNumber = Random.Range(min, max);
        return randomNumber;
    }

    private bool CheckIfScavageSuccessful()
    {
        int randomNumber = GetRandomNumber(0, 100);
        Debug.Log("Scavage random number is -> "+randomNumber);
        if (randomNumber <= scavageChance)
        {
            return true;
        }

        return false;
    }

    private void GiveScavageRewards()
    {
        Debug.Log("GiveScavageRewards() running...");
        int randomNumber = GetRandomNumber(1, 6);
        Debug.Log("Rewards should be for #" + randomNumber);
        switch (randomNumber)
        {
            case 1:
                Debug.Log("Rewards are being given for #1 -> Gather Wood");
                taskScavagePopupWood.SetActive(true);
                taskGatherWood.GetComponent<GatherWoodPanel>().SetGatherWoodLevel(taskGatherWoodLevel + 1);
                taskGatherWoodLevel += 1;
                taskGatherWood.GetComponent<GatherWoodPanel>().SetGatherWoodName(taskGatherWoodLevel, taskGatherWoodLevel + 1);
                //taskGatherWood.GetComponent<GatherWoodPanel>().DestroyAllChildren();
                break;
            case 2:
                Debug.Log("Rewards are being given for #2 -> Hunt");
                taskScavagePopupHunt.SetActive(true);
                taskHunt.GetComponent<HuntPanel>().SetHuntLevel(taskHuntLevel + 1);
                taskHuntLevel += 1;
                taskHunt.GetComponent<HuntPanel>().SetHuntName(taskHuntLevel + 2, taskHuntLevel + 3);
                //taskHunt.GetComponent<HuntPanel>().DestroyAllChildren();
                break;
            case 3:
                Debug.Log("Rewards are being given for #3 -> Gather Berries");
                taskScavagePopupBerries.SetActive(true);
                taskGatherBerries.GetComponent<GatherBerriesPanel>().SetGatherBerriesLevel(taskGatherBerriesLevel + 1);
                taskGatherBerriesLevel += 1;
                taskGatherBerries.GetComponent<GatherBerriesPanel>().SetGatherBerriesName(taskGatherBerriesLevel, taskGatherBerriesLevel + 1);
                //taskGatherBerries.GetComponent<GatherBerriesPanel>().DestroyAllChildren();
                break;
            case 4:
                Debug.Log("Rewards are being given for #4 -> Collect Water");
                taskScavagePopupWater.SetActive(true);
                taskCollectWater.GetComponent<CollectWaterPanel>().SetCollectWaterLevel(taskCollectWaterLevel + 1);
                taskCollectWaterLevel += 1;
                taskCollectWater.GetComponent<CollectWaterPanel>().SetCollectWaterName(taskCollectWaterLevel, taskCollectWaterLevel + 1);
                //taskCollectWater.GetComponent<CollectWaterPanel>().DestroyAllChildren();
                break;
            case 5:
                Debug.Log("Rewards are being given for #5 -> Scavage");
                if (scavageChance < 50)
                {
                    taskScavagePopupScavage.SetActive(true);
                    scavageChance += 1;
                    taskScavage.GetComponent<ScavagePanel>().SetScavageName(scavageChance);
                    
                }
                //taskScavage.GetComponent<ScavagePanel>().DestroyAllChildren();
                break;
        }
    }

    private void ResetRerollButton()
    {
        Debug.Log("ResetRerollButton() running...");
        GetComponent<RerollButton>().SetButtonDisableStatus(true);
        SetRerollsLeft(3);
        SetRerollsLeftText(3);
    }

    private void DestroyAllDiceInDiceTray()
    {
        Debug.Log("DestroyAllDiceInDiceTray() running...");
        diceTray.GetComponentInChildren<DiceTrayPanel>().DestroyAllChildren();
    }

    private void CheckResourcePanels()
    {
        Debug.Log("CheckResourcePanels() running...");
        ProcessGatherWood();
        ProcessHunt();
        ProcessGatherBerries();
        ProcessCollectWater();
    }

    private void ProcessGatherWood()
    {
        Debug.Log("ProcessGatherWood() running...");
        bool hasDie = taskGatherWood.GetComponent<GatherWoodPanel>().CheckForDie();

        if (hasDie)
        {
            Debug.Log("Die found!!!! -> ProcessGatherWood()");
            Debug.Log("Wood value is at: "+GetResourcesAvailableWood());
            Debug.Log("Wood value should be " + (GetResourcesAvailableWood()+1) + " or " + (GetResourcesAvailableWood()+2));
            SetResourcesAvailableWood(GetResourcesAvailableWood()+GetRandomNumber(taskGatherWoodLevel, taskGatherWoodLevel + 2));
            SetResourcesAvailableWoodText(GetResourcesAvailableWood());
            taskGatherWood.GetComponent<GatherWoodPanel>().DestroyAllChildren();
        }
    }

    private void ProcessHunt()
    {
        Debug.Log("ProcessHunt() running...");
        bool hasDice = taskHunt.GetComponent<HuntPanel>().CheckForDie();

        if (hasDice)
        {
            Debug.Log("Die found!!!! -> ProcessHunt()");
            Debug.Log("Food value is at: " + GetResourcesAvailableFood());
            Debug.Log("Food value should be " + (GetResourcesAvailableFood()+2) + " or " + (GetResourcesAvailableFood()+3));
            daysWithoutFood = 0;
            SetResourcesAvailableFood(GetResourcesAvailableFood()+GetRandomNumber(taskHuntLevel + 2, taskHuntLevel + 4));
            SetResourcesAvailableFoodText(GetResourcesAvailableFood());
        }
        taskHunt.GetComponent<HuntPanel>().DestroyAllChildren();
    }

    private void ProcessGatherBerries()
    {
        Debug.Log("ProcessGatherBerries() running...");
        bool hasDie = taskGatherBerries.GetComponent<GatherBerriesPanel>().CheckForDie();
        Debug.Log("hasDie is "+hasDie);
        if (hasDie)
        {
            Debug.Log("Die found!!!! -> ProcessGatherBerries()");
            Debug.Log("Food value is at: " + GetResourcesAvailableFood());
            Debug.Log("Food value should be " + (GetResourcesAvailableFood()+1) + " or " + (GetResourcesAvailableFood()+2));
            daysWithoutFood = 0;
            SetResourcesAvailableFood(GetResourcesAvailableFood()+GetRandomNumber(taskGatherBerriesLevel, taskGatherBerriesLevel + 2));
            SetResourcesAvailableFoodText(GetResourcesAvailableFood());
            taskGatherBerries.GetComponent<GatherBerriesPanel>().DestroyAllChildren();
        }
    }

    private void ProcessCollectWater()
    {
        Debug.Log("ProcessCollectWater() running...");
        bool hasDice = taskCollectWater.GetComponent<CollectWaterPanel>().CheckForDie();

        if (hasDice)
        {
            Debug.Log("Die found!!!! -> ProcessCollectWater()");
            Debug.Log("Water value is at: " + GetResourcesAvailableWater());
            Debug.Log("Water value should be " + (GetResourcesAvailableWater()+1) + " or " + (GetResourcesAvailableWater()+2));
            daysWithoutWater = 0;
            SetResourcesAvailableWater(GetResourcesAvailableWater()+GetRandomNumber(taskCollectWaterLevel, taskCollectWaterLevel + 2));
            SetResourcesAvailableWaterText(GetResourcesAvailableWater());
        }
        taskCollectWater.GetComponent<CollectWaterPanel>().DestroyAllChildren();
    }

    private void CheckScavagePanel()
    {
        Debug.Log("CheckScavagePanel() running...");
        bool hasDice = taskScavage.GetComponent<ScavagePanel>().CheckForDie();

        if (hasDice)
        {
            Debug.Log("Die found!!!! -> CheckScavagePanel()");
            bool scavageSuccessful = CheckIfScavageSuccessful();
            Debug.Log("scavageSuccessful -> "+ scavageSuccessful);
            if (scavageSuccessful)
            {
                Debug.Log("Found an upgrade!!");
                GiveScavageRewards();
            }
        }
        taskScavage.GetComponent<ScavagePanel>().DestroyAllChildren();
    }

    private void CheckBuildSignalFire()
    {
        Debug.Log("CheckBuildSignalFire() running...");
        Debug.Log("isSignalFireBuilt -> " + isSignalFireBuilt);
        Debug.Log("taskBuildSignalFire.activeInHierarchy -> "+ taskBuildSignalFire.activeInHierarchy);
        Debug.Log("taskSignalShip.activeInHierarchy -> "+ taskSignalShip.activeInHierarchy);
        if (taskSignalShip.activeInHierarchy && isSignalFireBuilt)
        {
            Debug.Log("Looks like you built the signal fire and can get the attention of a ship!!");
            bool hasDie = taskSignalShip.GetComponent<SignalShipPanel>().CheckForDie();

            if (hasDie)
            {
                Debug.Log("You Win!");
                taskSignalShip.GetComponent<SignalShipPanel>().DestroyAllChildren();
                SceneManager.LoadScene(3);
            }
            else
            {
                Debug.Log("But you didn't...");
                taskSignalShip.GetComponent<SignalShipPanel>().DestroyAllChildren();
                taskSignalShip.gameObject.SetActive(false);
                isSignalFireBuilt = false;
                signalFireLit.SetActive(false);
            }
        }

        if (!taskBuildSignalFire.activeInHierarchy && !isSignalFireBuilt)
        {
            Debug.Log("No signal fire? Hmm... Can you build one?");
            if (GetResourcesAvailableWood() >= signalFireWoodCost)
            {
                Debug.Log("Yes you can!");
                taskBuildSignalFire.gameObject.SetActive(true);
                taskBuildSignalFire.GetComponent<BuildSignalFirePanel>().GenerateRandomDiceImages();
                return;
            }
            Debug.Log("Oh... No you can't");
        }

        if (taskBuildSignalFire.activeInHierarchy && !isSignalFireBuilt)
        {
            Debug.Log("Did you build the signal fire?");
            bool hasDie = taskBuildSignalFire.GetComponent<BuildSignalFirePanel>().CheckForDie();

            if (hasDie)
            {
                Debug.Log("Yes you did!");
                isSignalFireBuilt = true;
                taskBuildSignalFire.GetComponent<BuildSignalFirePanel>().DestroyAllChildren();
                taskBuildSignalFire.gameObject.SetActive(false);
                SetResourcesAvailableWood(GetResourcesAvailableWood() - signalFireWoodCost);
                SetResourcesAvailableWoodText(GetResourcesAvailableWood());
                signalFire.SetActive(true);
                return;
            }
            Debug.Log("No you didn't!");
            taskBuildSignalFire.GetComponent<BuildSignalFirePanel>().DestroyAllChildren();
        }

        if (!taskBuildSignalFire.activeInHierarchy && isSignalFireBuilt)
        {
            Debug.Log("Looks like you build the signal fire. Any ships nearby?");
            int randomNumber = GetRandomNumber(0, 100);
            Debug.Log("Signal Ship random number is -> "+randomNumber);
            if (randomNumber <= rescueShipChance)
            {
                Debug.Log("Yes there is!");
                taskSignalShip.gameObject.SetActive(true);
                taskSignalShip.GetComponent<SignalShipPanel>().GenerateRandomDiceImages();
                signalFire.SetActive(false);
                signalFireLit.SetActive(true);
                return;
            }
            Debug.Log("No there isn't");
        }
    }

    private void CheckResources()
    {
        Debug.Log("CheckResources() running...");
        if (GetResourcesAvailableFood() == 0)
        {
            Debug.Log("NO FOOD!!");
            Debug.Log("daysWithoutFood -> " + daysWithoutFood);
            daysWithoutFood++;
            SetDaysWithoutFood(daysWithoutFood);
            SetDaysWithoutFoodText(daysWithoutFood);
        }

        if (GetResourcesAvailableWater() == 0)
        {
            Debug.Log("NO WATER!!");
            Debug.Log("daysWithoutWater -> " + daysWithoutWater);
            daysWithoutWater++;
            SetDaysWithoutWater(daysWithoutWater);
            SetDaysWithoutWaterText(daysWithoutWater);
        }

        if (GetResourcesAvailableFood() > 0)
        {
            if (GetRequiredPerDayFood() > GetResourcesAvailableFood())
            {
                Debug.Log("SETTING FOOD TO 0!!!!");
                SetResourcesAvailableFood(0);
                SetResourcesAvailableFoodText(0);
            }
            else
            {
                Debug.Log("Food Stock is currently: "+ GetResourcesAvailableFood());
                Debug.Log("Food Required is currently: " + GetRequiredPerDayFood());
                SetResourcesAvailableFood(GetResourcesAvailableFood() - GetRequiredPerDayFood());
                SetResourcesAvailableFoodText(GetResourcesAvailableFood());
            }
        }
        
        if (GetResourcesAvailableWater() > 0)
        {
            if (GetRequiredPerDayWater() > GetResourcesAvailableWater())
            {
                Debug.Log("SETTING WATER TO 0!!!!");
                SetResourcesAvailableWater(0);
                SetResourcesAvailableWaterText(0);
            }
            else
            {
                Debug.Log("Water Stock is currently: " + GetResourcesAvailableWater());
                Debug.Log("Water Required is currently: " + GetRequiredPerDayWater());
                SetResourcesAvailableWater(GetResourcesAvailableWater() - GetRequiredPerDayWater());
                SetResourcesAvailableWaterText(GetResourcesAvailableWater());
            }
        }

        if (daysWithoutFood == 3 || daysWithoutWater == 3)
        {
            SceneManager.LoadScene(4);
        }

        if (GetNumberOfDaysOnIsland() % 3 == 0)
        {
            SetRequiredPerDayFood(GetRequiredPerDayFood() + 1);
            SetRequiredPerDayFoodText(GetRequiredPerDayFood());
            SetRequiredPerDayWater(GetRequiredPerDayWater() + 1);
            SetRequiredPerDayWaterText(GetRequiredPerDayWater());
        }

        SetNumberOfDaysOnIsland(GetNumberOfDaysOnIsland() + 1);
        SetDayText(GetNumberOfDaysOnIsland());
    }

    private void SpawnNewDiceInDiceTray()
    {
        Debug.Log("SpawnNewDiceInDiceTray() running...");
        diceTray.GetComponent<DiceTrayPanel>().RandomizeDiceTray();
    }
}
