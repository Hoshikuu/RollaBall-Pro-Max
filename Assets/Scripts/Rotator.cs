using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Vector3 startingPoint;
    private int tweak = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3 (15, 30, 45) * Time.deltaTime);
        if (transform.position.y >= startingPoint.y + 0.5f)
        {
            tweak = -1;
        }
        else if (transform.position.y <= startingPoint.y)
        {
            tweak = 1;            
        }
        transform.position = new Vector3 (transform.position.x, transform.position.y + 0.25f * Time.deltaTime * tweak, transform.position.z);

        
    }
}
