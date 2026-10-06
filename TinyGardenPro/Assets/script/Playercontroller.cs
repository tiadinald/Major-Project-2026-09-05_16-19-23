using UnityEngine;

/// <summary>
/// Basic third-person garden movement: WASD/arrow keys to walk, character
/// rotates to face the direction of travel. Drives an Animator "Speed"
/// float if one is present, so a walk/idle blend works automatically.
///
/// Put this on each character prefab (Male, Female) alongside a
/// CharacterController component.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3.5f;
    public float turnSpeed = 12f;
    public float gravity = -9.81f;

    [Header("Optional")]
    public Animator animator; // leave empty if you don't have one yet

    private CharacterController controller;
    private float verticalVelocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float v = Input.GetAxisRaw("Vertical");   // W/S or Up/Down

        Vector3 inputDir = new Vector3(h, 0f, v);
        if (inputDir.sqrMagnitude > 1f) inputDir.Normalize();

        // Move relative to world axes (top-down/angled garden camera).
        // If you want movement relative to the camera instead, swap
        // 'inputDir' below for a camera-relative direction.
        Vector3 move = inputDir * moveSpeed;

        // Simple gravity so the character sticks to uneven terrain.
        if (controller.isGrounded)
            verticalVelocity = -0.5f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);

        // Rotate to face movement direction.
        if (inputDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(inputDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
        }

        if (animator != null)
            animator.SetFloat("Speed", inputDir.magnitude);
    }
}