using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [Header("Gradients")]
    [SerializeField] private Gradient fogGradient;
    [SerializeField] private Gradient ambientGradient;
    [SerializeField] private Gradient directionLightGradient;
    [SerializeField] private Gradient skyboxTintGradient;

    [Header("Enviromental Assets")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private Material skyboxMaterial;

    [Header("Variables")]
    [SerializeField] private float dayDurationInSeconds = 24f;
    [SerializeField] private float rotationSpeed = 1.0f;

    [SerializeField, Range(0,1)] private float currentTime = 0;
    [SerializeField] private int currentHour;

    private void Update()
    {
        UpdateTime();
        UpdateDayNightCycle();
        RotateSkybox();
    }

    private void UpdateTime()
    {
        currentTime += Time.deltaTime / dayDurationInSeconds;
        currentTime = Mathf.Repeat(currentTime, 1f);

        currentHour = Mathf.FloorToInt(currentTime * 24 );
    }

    private void UpdateDayNightCycle()
    {
        float sunPosition = Mathf.Repeat(currentTime+0.25f, 1f);
        directionalLight.transform.rotation = Quaternion.Euler(sunPosition * 360f, 0f, 0f);

        RenderSettings.fogColor = directionLightGradient.Evaluate(currentTime);

        skyboxMaterial.SetColor("_Tint", skyboxTintGradient.Evaluate(currentTime));

    }

    private void RotateSkybox()
    {
        float currentRotation = skyboxMaterial.GetFloat("_Rotation");
        float newRotation = currentRotation + rotationSpeed * Time.deltaTime;
        newRotation = Mathf.Repeat(newRotation, 360f);
        skyboxMaterial.SetFloat("_Rotation", newRotation);
    }

    private void OnApplicationQuit()
    {
        skyboxMaterial.SetColor("_Tint", new Color(0.5f,0.5f,0.5f));
    }
}
