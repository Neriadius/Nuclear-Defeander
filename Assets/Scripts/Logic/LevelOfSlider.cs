using UnityEngine;
using TMPro;
using UnityEngine.XR.Content.Interaction;

public class LevelOfSlider : MonoBehaviour
{
    public TMP_Text textLevel;
    public TMP_Text compareToValue;
    public int compareToValueInt;

    private float level;
    private XRSlider boxSlide;
    private int intLevel;

    void Start()
    {
        boxSlide = GetComponent<XRSlider>();
        compareToValue.text = compareToValueInt.ToString();
    }

    void Update()
    {
        ShowLevel();
    }

    public void ShowLevel()
    {
        level = boxSlide.value * 100;
        intLevel = RoundTo5((int)level);
        textLevel.text = intLevel.ToString();
        CompareValues(intLevel, compareToValueInt);
    }

    private int RoundTo5(int value)
    {
        return (int)(Mathf.Round(value / 5.0f) * 5);
    }

    public void CompareValues(float value1, float value2)
    {
        if (value1 > value2)
        {
            Debug.Log("Value 1 is greater than Value 2: " + value1 + " > " + value2);
        }
        else if (value1 < value2)
        {
            Debug.Log("Value 1 is less than Value 2: " + value1 + " < " + value2);
        }
        else
        {
            Debug.Log("Value 1 is equal to Value 2: " + value1 + " = " + value2);
        }
    }
}

