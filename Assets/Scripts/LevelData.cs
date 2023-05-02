using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/Level")]
public class LevelData : ScriptableObject
{
    public GameObject[] LevelFurnitures;
    public GameObject[] ExtraLevelFurnitures;
    public Dialogue InitialDialogue;
    public Dialogue FinishDialogue;
}
