using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class CameraSwap : MonoBehaviour
{
    bool isFirstPerson;
    public static float FOV = 60f;
    public static bool postFX;
    [Header("Cameras")]
    public Camera firstPerson;
    public Camera thirdPerson;
    public Camera weaponCamera;
    [Header("Audio Listener")]
    public AudioListener firstPersonListener;
    public AudioListener thirdPersonListener;
    [Header("Bullet")]
    public ShootScript Weapon;
    [Header("Other")]
    public MeshRenderer playerMesh;

    public PostProcessVolume postProcessing;

    void Start()
    {
        isFirstPerson = PlayerPrefs.GetInt("isfirstperson", 1) == 1;
        Update();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !PauseMenu.GameIsPaused) 
        {
            isFirstPerson = !isFirstPerson;
            PlayerPrefs.SetInt("isfirstperson", isFirstPerson? 1 : 0);
        }
        playerMesh.shadowCastingMode = isFirstPerson? ShadowCastingMode.ShadowsOnly : ShadowCastingMode.On; 
        firstPerson.enabled = firstPersonListener.enabled = weaponCamera.enabled = isFirstPerson;
        thirdPerson.enabled = thirdPersonListener.enabled = !isFirstPerson;
        Weapon.camera = isFirstPerson? firstPerson : thirdPerson;
        firstPerson.fieldOfView = thirdPerson.fieldOfView = weaponCamera.fieldOfView = FOV;
        postProcessing.isGlobal = postFX;
    }
}
