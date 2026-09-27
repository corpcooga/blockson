using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    const string gameplayVolumeKey = "gameplayVolume";
    const string sensitivityKey = "sensitivity";
    const string fieldOfViewKey = "FOV";
    const string postFXKey = "postFX";

    public Slider gameplayVolumeSlider;
    public Slider sensitivitySlider;
    public Slider fieldOfViewSlider;
    public Toggle postFXToggle;

    void Start()
    {
        float gameplayVolume = PlayerPrefs.GetFloat(gameplayVolumeKey, gameplayVolumeSlider.value);
        float sensitivity = PlayerPrefs.GetFloat(sensitivityKey, sensitivitySlider.value);
        float FOV = PlayerPrefs.GetFloat(fieldOfViewKey, fieldOfViewSlider.value);
        bool postFX = PlayerPrefs.GetInt(postFXKey, Convert.ToInt32(postFXToggle.isOn)) == 1;
        gameplayVolumeSlider.value = gameplayVolume;
        sensitivitySlider.value = sensitivity;
        fieldOfViewSlider.value = FOV;
        postFXToggle.isOn = postFX;
        SetGameplayVolume(gameplayVolume);
        SetSensitivity(sensitivity);
        SetFOV(FOV);
        TogglePostFX(postFX);
    }

    public void SetGameplayVolume(float volume)
    {
        AudioManager.instance.SetGameplayVolume(volume);
        PlayerPrefs.SetFloat(gameplayVolumeKey, volume);
    }

    public void SetSensitivity(float sensitivity)
    {
        PlayerControls.sensitivity = sensitivity;
        PlayerPrefs.SetFloat(sensitivityKey, sensitivity);
    }

    public void SetFOV(float FOV)
    {
        CameraSwap.FOV = FOV;
        PlayerPrefs.SetFloat(fieldOfViewKey, FOV);
    }

    public void TogglePostFX(bool postFX)
    {
        CameraSwap.postFX = postFX;
        PlayerPrefs.SetInt(postFXKey, Convert.ToInt32(postFX));
    }
}
