using UnityEngine;
using DG.Tweening;
public class Testing : MonoBehaviour
{
    public Vector3 trialAxis;
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + trialAxis);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //transform.DORotateQuaternion(transform.rotation * Quaternion.AngleAxis(90f, trialAxis), 2f);
            transform.DORotate(trialAxis,2f,RotateMode.WorldAxisAdd);
        }
    }
}
