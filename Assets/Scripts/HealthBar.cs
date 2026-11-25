using UnityEngine;
using UnityEngine.UI;///// <<----

public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance;

    [SerializeField] Character character;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        if (slider != null)
        {
            slider = GetComponent<Slider>();
        }
    }

    private void OnEnable()
    {
        // SI NO EXISTE EL CHARACTER
        if (character == null) return;

        // NOS SUBSCRIBIMOS AL EVENTO
        character.OnHealthChanged += UpdateBar;
    }

    private void Start()
    {
        if (character == null) return;

        UpdateBar(character.CurrentHealth, character.MaxHealth);
    }

    private void OnDisable()
    {
        if (character == null) return;

        character.OnHealthChanged -= UpdateBar;
    }

    private void UpdateBar(float current, float max)
    {
        slider.maxValue = max;
        slider.value = current;
    }
}
