using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextureGeneratorControlUI : MonoBehaviour
{
    [SerializeField]
    Button refreshButton;

    [SerializeField]
    TextureGenerator textureGenerator;

    [SerializeField]
    SurfaceCreator surfaceCreator;

    [SerializeField]
    Slider resolutionSlider;

    [SerializeField]
    TMPro.TMP_Text resolutionValueText;


    [SerializeField]
    Slider frequencySlider;

    [SerializeField]
    TMPro.TMP_Text frequencyValueText;


    [SerializeField]
    Slider strengthSlider;

    [SerializeField]
    TMPro.TMP_Text strengthValueText;

    [SerializeField]
    private int resolution = 256;

    [SerializeField]
    private int frequency = 8;

    [SerializeField]
    private int strength = 1;

    [SerializeField]
    MeshRenderer noiseMapTextureRenderer;

    private void Awake()
    {
        // Znam che cqlata taz arhitektura e gnusna ama me murzi da gledam.
        resolutionSlider.onValueChanged.AddListener(OnResolitionValueChanged);
        frequencySlider.onValueChanged.AddListener(OnFrequencyValueChanged);
        strengthSlider.onValueChanged.AddListener(OnStrengthValueChanged);
        refreshButton.onClick.AddListener(Generate);

        int resolutionSqrt = (int) Mathf.Sqrt(resolution);
        resolutionSlider.SetValueWithoutNotify(resolutionSqrt);
        resolutionValueText.SetText(resolution + "");

        frequencySlider.SetValueWithoutNotify((int) frequency);
        frequencyValueText.SetText(frequency + "");

        strengthSlider.SetValueWithoutNotify((int)strength);
        strengthValueText.SetText(strength + "");
    }

    private void Generate()
    {
        Texture2D texture = textureGenerator.GenerateTexture(resolution, frequency);
        if(noiseMapTextureRenderer)
        {
            noiseMapTextureRenderer.material.mainTexture = texture;
        }
        surfaceCreator.GenerateTerrain(texture, strength);
    }


    void OnResolitionValueChanged(float value)
    {
        resolution = (int) Mathf.Pow(2f, value);
        resolutionValueText.SetText(resolution + "");
    }

    void OnFrequencyValueChanged(float value)
    {
        frequency = (int) value;
        frequencyValueText.SetText(frequency + "");
    }

    
    void OnStrengthValueChanged(float value)
    {
        strength = (int)value;
        strengthValueText.SetText(value + "");
    }
}
