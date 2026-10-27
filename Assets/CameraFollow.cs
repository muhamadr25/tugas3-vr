using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3f, -6f);
    public float followSpeed = 10f;
    public float lookSpeed = 10f;

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 targetPos = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

        Vector3 direction = (target.position + Vector3.up * 1.2f) - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
    }
}