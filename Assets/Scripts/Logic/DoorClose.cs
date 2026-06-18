using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Content.Interaction;

public class DoorClose : MonoBehaviour
{
    public GameObject door;
    public float leverUpSpeed = 0.2f;
    public float doorMaxPosition = 0.5f;
    public float doorMinPosition = 0f;
    public scoreObject Operator;

    private bool isDoorClosed;

    private XRSlider slider;

    private void Awake()
    {
        Operator = FindFirstObjectByType<scoreObject>();
        slider = GetComponent<XRSlider>();
    }

    

    public void DoorTransition()
    {
        if (slider.value < 0.5 && isDoorClosed)
        {
            Debug.Log("Door not closed");
            isDoorClosed = !isDoorClosed;
            Operator.doorCount -= 1;
            StartCoroutine(DoorMove(doorMaxPosition));

        }
        else if (slider.value >= 0.5 && !isDoorClosed)
        {
            Debug.Log("Door closed");
            isDoorClosed = !isDoorClosed;
            Operator.doorCount += 1;
            StartCoroutine(DoorMove(doorMinPosition));
        }
        
        
    }

    IEnumerator DoorMove(float targetPosition)
    {
        float vector;
        vector = (targetPosition - door.transform.position.y) / Mathf.Abs(targetPosition - door.transform.position.y);
        door.transform.position = new Vector3 (door.transform.position.x, door.transform.position.y + vector * leverUpSpeed * Time.deltaTime, door.transform.position.z);
        yield return new WaitForSeconds(0.1f);
        if (door.transform.position.y > targetPosition && !isDoorClosed)
        {

        }
         else if (door.transform.position.y < targetPosition && isDoorClosed)
        {

        }
        else
        {
            StartCoroutine(DoorMove(targetPosition));
        }
        
    }


    
}
