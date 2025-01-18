using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISound : UIBase
{
    [SerializeField] private Slider globalVolumeSlider;
    [SerializeField] private TextMeshProUGUI globalVolumeText;

    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private TextMeshProUGUI bgmVolumeText;

    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private TextMeshProUGUI sfxVolumeText;

    [SerializeField] private Slider uiVolumeSlider;
    [SerializeField] private TextMeshProUGUI uiVolumeText;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = AudioManager.Instance;
    }

    void Start()
    {
        globalVolumeSlider.value = audioManager.globalVolume * 0.01f;
        bgmVolumeSlider.value = audioManager.bgmVolume * 0.01f;
        sfxVolumeSlider.value = audioManager.sfxVolume * 0.01f;
        uiVolumeSlider.value = audioManager.uiVolume * 0.01f;

        globalVolumeText.text = audioManager.globalVolume.ToString();
        bgmVolumeText.text = audioManager.bgmVolume.ToString();
        sfxVolumeText.text = audioManager.sfxVolume.ToString();
        uiVolumeText.text = audioManager.uiVolume.ToString();

        globalVolumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(globalVolumeSlider, globalVolumeText, "global"); });
        bgmVolumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(bgmVolumeSlider, bgmVolumeText, "bgm"); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(sfxVolumeSlider, sfxVolumeText, "sfx"); });
        uiVolumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(uiVolumeSlider, uiVolumeText, "ui"); });
    }

    private void UpdateVolume(Slider slider, TextMeshProUGUI text, string type)
    {
        text.text = (slider.value * 100).ToString("F0") + "%";

        switch (type)
        {
            case "global":
                audioManager.globalVolume = slider.value * 100;
                audioManager.SetBGMVolume();
                break;
            case "bgm":
                audioManager.bgmVolume = slider.value * 100;
                audioManager.SetBGMVolume();
                break;
            case "sfx":
                audioManager.sfxVolume = slider.value * 100;
                break;
            case "ui":
                audioManager.uiVolume = slider.value * 100;
                break;
        }
    }
}
