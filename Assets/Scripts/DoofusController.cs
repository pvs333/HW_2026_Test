using UnityEngine;

public class DoofusController : MonoBehaviour
{
    public float moveSpeed = 5f, speedMultiplier = 1f;
    public bool faceMovementDirection = true;
    public float rotationSpeed = 540f;
    public float idleYRotation = 180f;


    private static readonly int WalkingParam = Animator.StringToHash("walking");
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        moveSpeed = gameManager.GetPlayerSpeed();
    }


    // Update is called once per frame
    void Update()
    {

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 move = new Vector3(input.x, 0f, input.y).normalized;

        transform.position += move * moveSpeed * Time.deltaTime * speedMultiplier;

        bool isWalking = move.sqrMagnitude > 0.0001f;

        if (faceMovementDirection)
        {
            Quaternion targetRotation = isWalking
                ? Quaternion.LookRotation(move, Vector3.up)
                : Quaternion.Euler(0f, idleYRotation, 0f);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (animator != null)
        {
            animator.SetBool(WalkingParam, isWalking);
        }
    }
}
