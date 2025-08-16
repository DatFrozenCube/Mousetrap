using UnityEngine;

[CreateAssetMenu(fileName = "Menu", menuName = "ScriptableObjects/Menu")]
public class Menu : ScriptableObject
{
    public string menuName;
    public GameObject[] orderedPrefabs;
}
