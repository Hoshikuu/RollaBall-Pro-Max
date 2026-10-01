using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float followSmoothness;

    private Rigidbody playerRb;

    private float followDistance;
    private float followHeight;

    private Vector3 followDirection;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();

        Vector3 offset = transform.position - player.transform.position;

        followHeight = offset.y;

        Vector3 horizontalOffset = new Vector3(offset.x, 0f, offset.z);

        followDistance = horizontalOffset.magnitude;

        if (horizontalOffset.sqrMagnitude > 0.001f)
        {
            followDirection = -horizontalOffset.normalized;
        }
        else
        {
            followDirection = Vector3.forward;
        }
    }

    void LateUpdate()
    {
        Vector3 velocity = playerRb.linearVelocity;

        Vector3 horizontalVelocity = new Vector3(
            velocity.x,
            0f,
            velocity.z
        );

        if (horizontalVelocity.sqrMagnitude > 0.1f)
        {
            Vector3 targetDirection = horizontalVelocity.normalized;

            Quaternion currentRotation = Quaternion.LookRotation(followDirection);
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion newRotation = Quaternion.RotateTowards( currentRotation, targetRotation, rotationSpeed * Time.deltaTime );

            followDirection = newRotation * Vector3.forward;
        }

        Vector3 targetPosition = player.transform.position - followDirection * followDistance + Vector3.up * followHeight;

        float smooth = 1f - Mathf.Exp(-followSmoothness * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smooth);

        transform.LookAt(player.transform.position);
    }
}