using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class BasicSettings : MonoBehaviour
{
    public static BasicSettings Instance;
    public bool IsParticlesOn;
    [SerializeField] private GameObject particlesIndicator;
    [SerializeField] private Settings settings;
    private TMP_Text particlesText;

    private void Start()
    {
        Instance = this;
    }

    private void Awake()
    {
        particlesText = particlesIndicator.GetComponent<TMP_Text>();
        IsParticlesOn = settings.IsParticles;
        CheckParticles();
    }

    public void ToggleParticles()
    {
        IsParticlesOn = !IsParticlesOn;
        settings.IsParticles = IsParticlesOn;
        CheckParticles();
    }

    private void CheckParticles()
    {
        if (!IsParticlesOn)
        {
            particlesText.color = new Color32(244, 25, 0, 255);
            particlesText.text = "Off";
        }

        else
        {
            particlesText.color = new Color32(88, 255, 17, 255);
            particlesText.text = "On";
        }
    }
}
