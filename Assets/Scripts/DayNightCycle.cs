using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle instance { get; set; }

    [Header("Gradients")]
    [SerializeField] private Gradient skyColor;
    [SerializeField] private Gradient equatorColor;
    [SerializeField] private Gradient sunColor;

    [Header("Enviromental Assets")]
    [SerializeField] private Light Sun;

    [Header("Variables")]
    [SerializeField,Range(0,24)] private float timeOfDay;
    [SerializeField] private float sunRotationSpeed;

    public Text TimeUI;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        timeOfDay += Time.deltaTime * sunRotationSpeed;

        if (timeOfDay > 24)
        {
            timeOfDay = 0;
            TimeManager.instance.TriggerNextDay();
        }

        TimeUI.text = $"{((int)timeOfDay)}";



        UpdateSunRotation();
        UpdateLighting();

    }

    private void OnValidate()
    {
        UpdateSunRotation();
        UpdateLighting();

    }
    private void UpdateSunRotation()
    {
        float sunRotation = Mathf.Lerp(-90,270, timeOfDay / 24);
        Sun.transform.rotation = Quaternion.Euler(sunRotation, Sun.transform.rotation.y,Sun.transform.rotation.z);

    }

    private void UpdateLighting()
    {
        float timeFraction = timeOfDay / 24;
        RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor = skyColor.Evaluate(timeFraction);
        Sun.color = sunColor.Evaluate(timeFraction);
    }

    public void SkipDay()
    {
        timeOfDay = 6.00f;
        TimeManager.instance.TriggerNextDay();
    }



}
