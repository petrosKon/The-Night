using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //References
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;

    //Variables
    [SerializeField, Range(0, 24)] public float timeOfDay;

    private void Start()
    {
        timeOfDay = Random.Range(10, 18);
    }

    private void Update()
    {
        if(preset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * 0.2f;
            timeOfDay %= 24;
            UpdateLighting(timeOfDay/24f);
        }
        else
        {
            UpdateLighting(timeOfDay/24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        if(directionalLight != null)
        {
            directionalLight.color = preset.directionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f,170f,0f));
        }
    }

    private void OnValidate()
    {
        if(directionalLight != null)
        {
            return;
        }

        if(RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}
