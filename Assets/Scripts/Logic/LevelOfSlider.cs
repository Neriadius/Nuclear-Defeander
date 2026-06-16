using UnityEngine;
using TMPro;
using UnityEngine.XR.Content.Interaction;
using System.Collections;
using UnityEngine.SceneManagement;  

public class LevelOfSlider : MonoBehaviour
{
    public TMP_Text textLevel;
    public TMP_Text compareToValue;
    public int compareToValueInt;
    public Slider_logic_LED LED;
    public AudioSource click;
    public AudioSource bell;
    public GameObject[] lamps;

    private float level;
    private XRSlider boxSlide;
    private int intLevel;
    private int SliderValueStored;
    private bool color;
    void Start()
    {
        boxSlide = GetComponent<XRSlider>();
        compareToValue.text = compareToValueInt.ToString();
        textLevel.text = intLevel.ToString();
        StartCoroutine(Countdown());
    }

    void Update()
    {
        ShowLevel();
    }

    public void ShowLevel()
    {

        level = boxSlide.value * 100;
        intLevel = RoundTo5((int)level);
        if (intLevel != SliderValueStored)
        {
            CompareValues(intLevel, compareToValueInt);
            LampUpdate(intLevel);
            click.Play();
            textLevel.text = intLevel.ToString();
        }
        SliderValueStored = intLevel;
    }

    private int RoundTo5(int value)
    {
        return (int)(Mathf.Round(value / 5.0f) * 5);
    }

    public int CompareValues(float value1, float value2)
    {
        if (value1 > value2)
        {
            Debug.Log("Value 1 is greater than Value 2: " + value1 + " > " + value2);
            return 1;
        }
        else if (value1 < value2)
        {
            Debug.Log("Value 1 is less than Value 2: " + value1 + " < " + value2);
            return -1;
        }
        else
        {
            Debug.Log("Value 1 is equal to Value 2: " + value1 + " = " + value2);
            return 0;
        }
    }
    public void LampUpdate(int SliderValue)
    {
        if (SliderValue != SliderValueStored)
        {
            for (int i = 0; i < lamps.Length; i++)
            {
                if (i <= (SliderValue / 5))
                {
                    Light l = lamps[i].GetComponent<Light>();
                    l.LightOn(color);
                }
                else
                {
                    Light l = lamps[i].GetComponent<Light>();
                    l.LightOff();
                }

            }
        }
        SliderValueStored = SliderValue;
    }
    IEnumerator Countdown()
    {
        bell.Play();
        compareToValueInt = RoundTo5((int)Random.Range(0, 100));
        compareToValue.text = compareToValueInt.ToString();
        yield return new WaitForSeconds(Random.Range(20,30));
        if (CompareValues(intLevel, compareToValueInt) == 0)
        {
            StartCoroutine(Countdown());
        }
        else
        {
            color = true;
            yield return new WaitForSeconds(10);
            if (CompareValues(intLevel, compareToValueInt) == 0)
            {
                StartCoroutine(Countdown());
            }
            else
            {
                SceneManager.LoadScene(1);
            }
                
        }
    }
}

