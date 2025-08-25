using UnityEngine;
using TMPro;

public class LevelUIUpdater : MonoBehaviour
{
    public void UpdateUI(bool includeText)
    {
        if (includeText)
        {
            TMP_Text text = GetComponent<TMP_Text>();
            text.text = $"Experiment #{LevelController.LevelNumber.ToString()}";
        }

        else
        {
            TMP_Text text = GetComponent<TMP_Text>();
            text.text = "";
        }
    }
}
