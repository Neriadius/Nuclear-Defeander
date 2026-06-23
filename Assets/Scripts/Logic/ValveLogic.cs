using System.Collections;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using TMPro;

public class ValveLogic : MonoBehaviour
{
    public XRKnob[] valves;
    public TMP_Text[] textValues;
    public TMP_Text currentValueOut;
    public TMP_Text targetValueOut;
    public float delayTime = 5f;
    public scoreObject Operator;

    private int currentValue = 0;
    private int startValue = 0;
    private int targetValue = 0;

    private int[] currentMathValue = new int[3];
    private int[] randomValves = new int[3];
    private int[][] mathValue = new int [3][]; 
    
    

    void Awake()
    {
        Operator = FindFirstObjectByType<scoreObject>();
        foreach (XRKnob valve in valves)
        {
           valve.GetComponent<XRKnob>();
        }
        RandomValue();
    }

    private void Update()
    {
        ValvesRotation();
        currentValueOut.text = (currentValue+currentMathValue[0] + currentMathValue[1] + currentMathValue[2]).ToString();
        targetValueOut.text = targetValue.ToString();
    }

    private void RandomValue()
    {
        startValue = Random.Range(0, 100);
        currentValue = startValue;
        int index = 0;
        for (int r = 0; r < randomValves.Length; r++)
        {
            randomValves[r] = Random.Range(1, 20);

            mathValue[r] = new int[] { (-1*randomValves[r]), 0, randomValves[r] };
            for (int c = 0; c < 3; c++)
            {
                textValues[index].text = mathValue[r][c].ToString();
                Debug.Log(index);
                index++;
            }
        }

        Debug.Log("Start Value: " + startValue);
        targetValue = startValue + mathValue[0][Random.Range(0, 2)] + mathValue[1][Random.Range(0, 2)] + mathValue[2][Random.Range(0, 2)];
        Debug.Log("Target Value: " + targetValue);
        if (targetValue == startValue)
        {
            RandomValue();
        }
        else
        {
            StartCoroutine(Countdouwn());
        }
    }

    private void ValvesRotation()
    {
        int i = 0;
        foreach (XRKnob valve in valves)
        {
            
            
            if (valve.value >= 0 && valve.value < 0.33f)
            {
                currentMathValue[i] = mathValue[i][0];
            }
            else if (valve.value >= 0.33f && valve.value < 0.66f)
            {
                currentMathValue[i] = mathValue[i][1];
            }
            else if (valve.value >= 0.66f && valve.value <= 1f)
            {
                currentMathValue[i] = mathValue[i][2];
            }
            i += 1;
        }
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

    IEnumerator Countdouwn()
    {
        yield return new WaitForSeconds(delayTime);
        currentValue += currentMathValue[0] + currentMathValue[1] + currentMathValue[2];
        if (CompareValues(currentValue, targetValue) == 0)
        {
            log log = new log("Good", +50);
            Debug.Log("goooood");
            RandomValue();
        }
        else
        {
            log log = new log("To slow", -50);
            Debug.Log("to slow");
            RandomValue();
        }
            foreach (XRKnob valve in valves)
            {
                valve.value = 0.5f;
            }
    }
}