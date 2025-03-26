using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Factory.Settings.UI
{

    public class SettingsSliderField : MonoBehaviour
    {

        [SerializeField] private Slider m_slider;
        [SerializeField] private string m_settingParameterName; // Строковое название изменяемой настройки
        [Space]
        [SerializeField] private UnityEvent<float> m_onValueChanged;
        [Space]
        [SerializeField] private string m_valueStringFormat = "F1"; // Формат строки в событии ниже
        [SerializeField] private UnityEvent<string> m_valueString;

        public Slider slider => m_slider;

        private void Start()
        {
            m_slider.minValue = Settings.Properties[m_settingParameterName].minValue;
            m_slider.maxValue = Settings.Properties[m_settingParameterName].maxValue;
            OnValueChanged(Settings.Properties[m_settingParameterName].value);
        }

        public void SaveSettings()
        {
            Settings.Save();
        }

        private void OnEnable()
        {
            m_slider.onValueChanged.AddListener(OnValueChanged);
            OnValueChanged(Settings.Properties[m_settingParameterName].value);
        }

        private void OnDisable()
        {
            m_slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            m_slider.SetValueWithoutNotify(value);
            Settings.Properties[m_settingParameterName].SetValue(value);
            m_onValueChanged?.Invoke(value);
            m_valueString?.Invoke(value.ToString(m_valueStringFormat));
        }

    }

}