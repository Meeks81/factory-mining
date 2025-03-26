using Factory.Settings;
using UnityEngine;
using UnityEngine.Audio;

public class StartGame : MonoBehaviour
{

    [SerializeField] private ControlPanel m_controlPanel;
    [SerializeField] private EntityController m_playerController;
    [SerializeField] private MoveCamera m_moveCamera;
    [SerializeField] private AudioMixer m_audioMixer;

    private void Awake()
    {
        m_controlPanel.OnMoveTo.AddListener(m_playerController.GoTo);
        m_controlPanel.OnInteractiveWith.AddListener(m_playerController.InteractiveWithObject);
        m_controlPanel.OnDragDelta.AddListener((delta) => m_moveCamera.Move(-delta * 0.02f));
    }

    private void Start()
    {
        // Создание свойста в настройках
        Settings.Property audioVolumeProperty = new Settings.Property(1f, 0f, 1f);
        audioVolumeProperty.OnValueChanged.AddListener((value) => m_audioMixer.SetFloat("globalVolume", Mathf.Lerp(-80f, 0f, value))); // globalVolume - переменная микшера общего звука
        Settings.Properties.Add(SettingsGlobalNames.GlobalVolume, audioVolumeProperty);
        // Загрузка настроек из предыдущей сессии
        Settings.Load();
    }

}
