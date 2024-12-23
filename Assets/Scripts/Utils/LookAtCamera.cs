using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] bool rotateX = true;
    [SerializeField] bool rotateY = true;
    [SerializeField] bool rotateZ = true;

    private void Update()
    {
        Vector3 targetPosition = Camera.main.transform.position;

        transform.LookAt(targetPosition);

        Vector3 currentRotation = transform.rotation.eulerAngles;

        if (!rotateX) currentRotation.x = 0;
        if (!rotateY) currentRotation.y = 0;
        if (!rotateZ) currentRotation.z = 0;

        transform.rotation = Quaternion.Euler(currentRotation);
    }

    private void OnDrawGizmos()
    {
        Update();
    }
}
