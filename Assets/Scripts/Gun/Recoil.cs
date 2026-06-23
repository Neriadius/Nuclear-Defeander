using System.Collections;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    [SerializeField] private float recoilX;

    [SerializeField] private float timeToReturn;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        //currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);
        //transform.localRotation = Quaternion.Euler(currentRotation);
    }

    IEnumerator RecoilStart(float timeToMove)
    {
        float elapsedTime = 0f;

        while (elapsedTime < timeToMove)
        {
            Debug.Log("Recoil progress:" + elapsedTime/timeToMove);
            Quaternion targetRotation = Quaternion.Euler(transform.localRotation.x - recoilX * (elapsedTime/timeToMove),transform.localRotation.y,transform.localRotation.z);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void DoRecoil()
    {
        transform.localEulerAngles += new Vector3(recoilX,0f,0f);
        //(transform.localRotation.x - recoilX,transform.localRotation.y,transform.localRotation.z,transform.localRotation.w);
        Debug.Log("DoRecoil called");
        StartCoroutine(RecoilStart(timeToReturn));
    }
}
