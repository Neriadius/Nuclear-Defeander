using System.Collections;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class DoorClose : MonoBehaviour
{
    public GameObject door;
    public float leverUpSpeed = 3f;
    public float doorMaxPosition = 0.5f;
    public float doorMinPosition = 0f;

    private bool isDoorClosed;

    private XRSlider slider;

    private void Awake()
    {
        slider = GetComponent<XRSlider>();
    }

    

    public void DoorTransition()
    {
        if (slider.value < 0.5 && isDoorClosed)
        {
            Debug.Log("Door not closed");
            StartCoroutine(DoorMove(doorMaxPosition));

        }
        else if (slider.value >= 0.5 && !isDoorClosed)
        {
            Debug.Log("Door closed");
            StartCoroutine(DoorMove(doorMinPosition));
        }
        
        
    }

    IEnumerator DoorMove(float targetPosition)
    {
        float vector;
        vector = (targetPosition - door.transform.position.y) / Mathf.Abs(targetPosition - door.transform.position.y);
        door.transform.position = new Vector3 (door.transform.position.x, door.transform.position.y + vector * Time.deltaTime, door.transform.position.z);
        yield return new WaitForSeconds(0.1f);
        if (Mathf.Abs(door.transform.position.y) >= Mathf.Abs(targetPosition))
        {
            isDoorClosed = !isDoorClosed;
        }
        else
        {
            StartCoroutine(DoorMove(targetPosition));
        } 
    }


    
}
