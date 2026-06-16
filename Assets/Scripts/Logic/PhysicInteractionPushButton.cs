using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PhysicInteractionPushButton : MonoBehaviour
{
    public GameObject buttonObject;

    private XRPushButton pushButton;

    protected new void Awake()
    {
        pushButton = buttonObject.GetComponent<XRPushButton>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("Button Pressed");
            //pushButton.UpdatePress();
        }
    }
}
