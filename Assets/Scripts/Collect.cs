using UnityEngine;

public class Collect : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
