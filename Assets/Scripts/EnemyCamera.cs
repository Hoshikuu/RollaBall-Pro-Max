using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    public Transform enemy;

    public Vector3 offset = new Vector3(0, 2, -3);

    void LateUpdate()
    {
        transform.position = enemy.TransformPoint(offset);
        transform.LookAt(enemy);
    }
}
