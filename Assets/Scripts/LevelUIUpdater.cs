using UnityEngine;
using TMPro;

public class LevelUIUpdater : MonoBehaviour
{
    private void Start()
    {
        LevelController.LevelActions += UpdateUI;
    }

    public void UpdateUI()
    {
        GetComponent<TMP_Text>().text = LevelController.LevelNumber.ToString();
    }
}
