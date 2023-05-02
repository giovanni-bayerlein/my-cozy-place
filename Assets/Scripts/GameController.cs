using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    public int Money;

    public LevelData[] Levels;

    public Transform ViewportContent;
    public GameObject ViewportItemPrefab;
    public BuildingController BuildingController;

    int currentLevelIndex = -1;

    private void Start()
    {
        if (BuildingController == null) FindObjectOfType<BuildingController>();

        LoadLevel();
    }

    public void Add(GameObject furniture)
    {

    }

    public void Use(GameObject furniture)
    {
        
    }

    public void SpendMoney(int amount)
    {
        Money -= amount;
    }

    public void GainMoney(int amount)
    {
        Money += amount;
    }

    public void LoadLevel()
    {
        currentLevelIndex++;
        Debug.Log(Levels[currentLevelIndex].InitialDialogue.sentences);

        //GetComponent<DialogueTrigger>().TriggerDialogue(Levels[currentLevelIndex].InitialDialogue);

        LoadFurnituresToMenu();
    }

    void LoadFurnituresToMenu()
    {
        var level = Levels[currentLevelIndex];

        foreach (var furniture in level.LevelFurnitures)
        {
            InstaniateData(furniture);
        }

        foreach (var furniture in level.ExtraLevelFurnitures)
        {
            var data = InstaniateData(furniture);

            data.ShouldPay = true;
        }
    }

    FurnitureData InstaniateData(GameObject furniture)
    {
        var button = Instantiate(ViewportItemPrefab, ViewportContent);

        var furnitureData = furniture.GetComponent<Furniture>().Data;

        button.GetComponent<Image>().sprite = furnitureData.Icon;

        button.GetComponent<Button>().onClick.AddListener(() => BuildingController.Select(furniture));

        return furnitureData;
    }
}
