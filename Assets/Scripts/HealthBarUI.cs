using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider slider;
    public float smoothSpeed = 5f;

    private float targetValue;

    private void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        targetValue = slider.value;
    }

    private void Update()
    {
        slider.value = Mathf.Lerp(
            slider.value,
            targetValue,
            smoothSpeed * Time.deltaTime
        );
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        targetValue = maxHealth;
    }

    public void SetHealth(int health)
    {
        targetValue = health;
    }
}
