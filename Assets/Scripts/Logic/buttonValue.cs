using UnityEngine;

public class buttonValue : MonoBehaviour
{
    public int value;
    public GameObject memoryButton;

    private MemoryButtonLogic memoryButtonLogic;

    private void Awake()
    {
        memoryButtonLogic = memoryButton.GetComponent<MemoryButtonLogic>();
    }

    public void buttonChange()
    {
        memoryButtonLogic.buttonValueStored.Add(value);
    }
}
