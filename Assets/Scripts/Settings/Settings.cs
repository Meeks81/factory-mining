using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Factory.Settings
{

    public static class Settings
    {

        private const string SAVE_KEY = "settings"; // Ключ сохранения в PlayerPrefs

        public static Dictionary<string, Property> Properties { get; private set; } = new Dictionary<string, Property>();

        public static void Load()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY) == false)
                return;

            string json = PlayerPrefs.GetString(SAVE_KEY);
            Dictionary<string, Property> saveProperties = JsonConvert.DeserializeObject<Dictionary<string, Property>>(json);
            foreach (var item in saveProperties)
            {
                if (Properties.ContainsKey(item.Key))
                    Properties[item.Key].SetValue(item.Value.value);
            }
        }

        public static void Save()
        {
            PlayerPrefs.SetString(SAVE_KEY, JsonConvert.SerializeObject(Properties));
        }

        [Serializable]
        public class Property
        {
            public float value { get; private set; }
            public float minValue { get; private set; }
            public float maxValue { get; private set; }

            public UnityEvent<float> OnValueChanged { get; private set; } = new UnityEvent<float>();

            public Property(float value, float minValue, float maxValue)
            {
                this.minValue = minValue;
                this.maxValue = maxValue;
                SetValue(value);
            }

            public void SetValue(float value)
            {
                this.value = Mathf.Clamp(value, minValue, maxValue);
                OnValueChanged?.Invoke(this.value);
            }
        }

    }

}