using UnityEngine;
using System.Collections.Generic;

public class RagdollHandler : MonoBehaviour
{
    private List<Rigidbody> _rigidbodies;
    public void Initialize()
    {
        _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        Disable();
    }

    public void Enable()
    {
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    public void Disable()
    {
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = true;
        }
    }
}
