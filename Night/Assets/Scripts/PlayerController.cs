using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce;
    private CharacterController characterController;

    private Vector3 moveDirection;
    public float gravityScale;

    public Animator animator;

    public Transform pivot;
    public float rotateSpeed;

    public GameObject playerModel;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        // moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical") * moveSpeed) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed);
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (characterController.isGrounded) {

            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }

        }
      

        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime * gravityScale);
        characterController.Move(moveDirection * Time.deltaTime);

        //Move the player in different directions based on camera look direction
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f,pivot.rotation.eulerAngles.y,0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x,0f,moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        animator.SetBool("isGrounded",characterController.isGrounded);
        animator.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }
}
