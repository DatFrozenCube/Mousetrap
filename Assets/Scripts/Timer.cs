using UnityEngine;

public class Timer : MonoBehaviour
{
    void Start()
    {
        TimeController timeController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TimeController>();

        if (timeController.includeTime)
        {
            gameObject.SetActive(true);
        }

        else
        {
            gameObject.SetActive(false);
        }
    }
}
