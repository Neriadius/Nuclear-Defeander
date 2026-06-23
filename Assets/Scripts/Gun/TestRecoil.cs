using System.Collections;
using UnityEngine;

public class TestRecoil : MonoBehaviour
{
    [SerializeField] private float recoilX = 5f;
    [SerializeField] private float kickTime = 0.04f;   // how fast the gun snaps up
    [SerializeField] private float timeToReturn = 0.25f; // how long recovery takes

    private Coroutine _recoilCoroutine;

    public void DoRecoil()
    {
        if (_recoilCoroutine != null)
            StopCoroutine(_recoilCoroutine);
        _recoilCoroutine = StartCoroutine(RecoilSequence());
    }

    private IEnumerator RecoilSequence()
    {
        // --- Phase 1: kick up ---
        Quaternion startRot = transform.localRotation;
        Quaternion kickRot  = startRot * Quaternion.Euler(-recoilX, 0f, 0f);

        float elapsed = 0f;
        while (elapsed < kickTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / kickTime);
            transform.localRotation = Quaternion.Slerp(startRot, kickRot, t);
            yield return null;
        }
        transform.localRotation = kickRot;

        // --- Phase 2: return to original ---
        elapsed = 0f;
        while (elapsed < timeToReturn)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / timeToReturn);
            transform.localRotation = Quaternion.Slerp(kickRot, startRot, t);
            yield return null;
        }
        transform.localRotation = startRot;
    }
}