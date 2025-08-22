using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/Settings", order = 1)]
public class Settings : ScriptableObject
{
    public string SettingsName;
    public bool IsParticles;
}
