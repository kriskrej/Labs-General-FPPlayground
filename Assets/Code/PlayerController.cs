using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 2;
    Camera camera;
    Rigidbody rigidbody;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();
    }

    int howManyTimesPlayerCanJump;
    
    void Update() {
        HandleWalking();
        HandleHorizontalRotation();
        HandleVerticalRotation();

        if (IsGrounded())
            howManyTimesPlayerCanJump = 1;

        if (howManyTimesPlayerCanJump > 0 && Input.GetKeyDown(KeyCode.Space)) {
            rigidbody.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            howManyTimesPlayerCanJump--;
        }

    }

    void HandleVerticalRotation() {
        var mouseVerticalRotation = Input.GetAxis("Mouse Y");
        var camRot = camera.transform.rotation.eulerAngles;
        camRot.x = camRot.x - mouseVerticalRotation;
        camera.transform.rotation = Quaternion.Euler(camRot);
    }

    void HandleHorizontalRotation() {
        var mouseHorizontalRotation = Input.GetAxis("Mouse X");
        var newRotation = transform.localRotation.eulerAngles;
        newRotation.y = newRotation.y + mouseHorizontalRotation;
        transform.localRotation = Quaternion.Euler(newRotation);
    }

    void HandleWalking() {
        var userKeyboardInput = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
        var velocity = transform.rotation * userKeyboardInput * movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) velocity = velocity * 2;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f) && rigidbody.velocity.y <= 0;
    }
}