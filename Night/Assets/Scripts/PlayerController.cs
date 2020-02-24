using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Attributes")]
    public float moveSpeed;
    public float jumpForce;
    public Transform pivot;
    public float rotateSpeed;
    public GameObject particleDeathEffectPlayerPrefab;

    [Header("Unity Setup Fields")]
    private CharacterController characterController;
    private float gravityScale = 1f;
    public Animator animator;
    public GameObject playerModel;
    private Vector3 moveDirection;

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

        if (characterController.isGrounded)
        {
            UserInput();

            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Jump");
                moveDirection.y = jumpForce;
            }

        }


        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime * gravityScale);
        characterController.Move(moveDirection * Time.deltaTime);

        //Move the player in different directions based on camera look direction
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        animator.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    
        //this means that the player falls down
        if(transform.position.y < 0f)
        {
            Instantiate(particleDeathEffectPlayerPrefab, gameObject.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
       
        
    }

    private void UserInput()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack 01");

        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger("Attack 02");

        }

 
    }
}
