using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryButtonLogic : MonoBehaviour
{

    public GameObject[] buttons;
    public List<int> buttonValueStored = new List<int>();
    public int[] buttonRandomStored;
    public float delay = .5f;
    public Material lightMaterial;
    public Material defaultMaterial;
    public scoreObject Operator;
    public AudioSource audioPlayer;

    private int randValue;
    private int currentIndex = 0;
    void Awake()
    {
        Operator = FindFirstObjectByType<scoreObject>();
        ValueGeneration();
        StartCoroutine(ShowMemory());

    }

    private void ValueGeneration()
    {
        randValue = Random.Range(4, 8);
        buttonRandomStored = new int[randValue];

        for (int i = 0; i < randValue; i++)
        {
            buttonRandomStored[i] = Random.Range(0, buttons.Length);
        }
    }

    IEnumerator ShowMemory()
    {
        yield return new WaitForSeconds(2);
        int i = 0;

        for (i = 0; i < currentIndex + 1 && i < buttonRandomStored.Length; i++)
        //for ( i =0; i <= currentIndex; i++)
        {
            
            buttons[buttonRandomStored[i]].GetComponent<Renderer>().material = lightMaterial;
            yield return new WaitForSeconds(delay);
            buttons[buttonRandomStored[i]].GetComponent<Renderer>().material = defaultMaterial;
            yield return new WaitForSeconds(delay);



        }

        yield return new WaitUntil(() =>
            buttonValueStored.Count == currentIndex + 1);
        //if (buttonValueStored.Count != 0)
        //{
        //    Debug.Log("Correct sequence entered. Score updated.1");
        for (int j = 0; j < buttonValueStored.Count; j++)
        {
            if (buttonRandomStored[j] + 1 != buttonValueStored[j])
            {
                log log = new log("Memory Button Bad", -25);
                Operator.AddScore(log);
                ValueGeneration();
                Debug.Log("Wrong button pressed. Generating new sequence.");
                buttonValueStored.Clear();
                currentIndex = 0;
            } 
        }

        if (buttonValueStored.Count == buttonRandomStored.Length)
        {
            Debug.Log("Correct sequence entered. Score updated.");
            log log = new log("Memory Button Good", +50);
            Operator.AddScore(log);
            ValueGeneration();
            currentIndex = 0;
            buttonValueStored.Clear();
            audioPlayer.Play();
            yield return new WaitForSeconds(5f);
        }

        //}

        if (currentIndex < buttonRandomStored.Length - 1)
        {
            currentIndex++;
        }

        buttonValueStored.Clear();

        StartCoroutine(ShowMemory());
    }
}
