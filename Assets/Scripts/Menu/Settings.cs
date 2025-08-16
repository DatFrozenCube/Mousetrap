using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/Settings", order = 1)]
public class Settings : ScriptableObject
{
    public string SettingsName;
    public bool IsSoundEffects;
    public bool IsMusic;
    public bool IsParticles;
}
