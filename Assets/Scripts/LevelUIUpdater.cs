using UnityEngine;
using TMPro;

public class LevelUIUpdater : MonoBehaviour
{
    public void UpdateUI()
    {
        GetComponent<TMP_Text>().text = LevelController.LevelNumber.ToString();
    }
}
