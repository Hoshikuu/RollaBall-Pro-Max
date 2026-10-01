using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private int count;
    
    private bool isGrounded;

    private float movementX;
    private float movementZ;

    [SerializeField] private float speed = 0;
    [SerializeField] private float boost = 0;
    [SerializeField] private float jumpSpeed = 0;
    [SerializeField] private float jumpBoost = 0;
    
    public FadeOut fadeOut;
    public Transform spawnTransform;
    public Transform cameraTransform;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = cameraRight * movementX + cameraForward * movementZ;

        if (movement.sqrMagnitude > 1f)
        {
            movement.Normalize();
        }

        rb.AddForce(movement * speed);

        if (this.transform.position.y < -3)
        {
            Respawn();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

            if (count >= 10)
            {
                winTextObject.SetActive(true);
                GameObject.FindGameObjectWithTag("Enemy").gameObject.SetActive(false);
                fadeOut._Start();
                StartCoroutine(ResetGame());
            }
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(PowerUp());
        }

        if (other.gameObject.CompareTag("JumpPad"))
        {
            rb.AddForce(Vector3.up * jumpSpeed * jumpBoost, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            rb.useGravity = false;
            rb.isKinematic = true;

            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            fadeOut._Start();
            StartCoroutine(ResetGame());
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Respawn()
    {
        rb.position = spawnTransform.position;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    IEnumerator PowerUp()
    {
        speed *= boost;
        yield return new WaitForSeconds(5f);
        speed /= boost;
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");
    }
}