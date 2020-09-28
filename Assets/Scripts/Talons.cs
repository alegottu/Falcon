using UnityEngine;

public class Talons : MonoBehaviour
{
    private bool useTalons = false;
    private Transform transformToGrab = null;
    private Transform grabbedTransform = null;
    private Transform lastParent = null;

    private void Update()
    {
        useTalons = Input.GetKey(KeyCode.Space);

        if (!useTalons && grabbedTransform != null)
        {
            grabbedTransform.parent = lastParent;
            grabbedTransform = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.TryGetComponent(out Rigidbody2D rb))
        {
            transformToGrab = obj.transform;
            lastParent = transformToGrab.parent;
        }
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (useTalons && obj.transform.Equals(transformToGrab))
        {
            grabbedTransform = obj.transform;
            grabbedTransform.parent = transform;

            grabbedTransform.localPosition = Vector3.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.transform.Equals(grabbedTransform))
        {
            grabbedTransform.transform.parent = lastParent;
            grabbedTransform = null;
        }
    }
}
