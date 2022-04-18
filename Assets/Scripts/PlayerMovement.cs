using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private enum State {
        STANDING,
        MANTLING,
        LEANING,
    }

#region Object Components
    public CharacterController controller;
    public Transform groundCheck;
    public Transform mantlePoint;
    public Animator animator;
    public Transform cam;

#endregion


#region Fields
    public float speed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 10f;
    public float leanDistance = 1f;

    public float groundDistance = 0.4f;
    public float mantleRange = 1.25f;
    public LayerMask groundMask; 

    private State state;

    private Vector3 velocity;
    private bool isGrounded;

    private int layerMask = 1 << 8;

    private Vector3 defaultCamPos;
    

#endregion
    
    // Start is called before the first frame update
    void Start()
    {
        defaultCamPos = cam.position;
        state = State.STANDING;
        layerMask = ~layerMask;
    }


    void Update()
    {
        switch(state) {
            case State.STANDING:
                #region Movement
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

                if (isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f;
                }
                
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");

                Vector3 move = transform.right * x + transform.forward * z;

                controller.Move(move * speed * Time.deltaTime);

                //Add Jump height
                if (Input.GetButtonDown("Jump") && isGrounded) {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                

                velocity.y += gravity * Time.deltaTime;

                controller.Move(velocity * Time.deltaTime);
                #endregion 

                #region Transition to Mantling
                if (Input.GetButton("Jump") && !isGrounded) {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, mantleRange, layerMask)) {
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                        Debug.Log("Mantle Ray Hit!");
                        if (hit.collider.bounds.Contains(mantlePoint.position) == false) {
                            state = State.MANTLING;
                        }
                    }
                    else {
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * mantleRange, Color.red);
                        Debug.Log("Mantle Ray Miss");
                    }
                }
                #endregion

                #region Transition to Leaning
                else if ((Input.GetKeyDown(KeyCode.E) ^ Input.GetKeyDown(KeyCode.Q)) && isGrounded)
                {
                    Vector3 camMove = new Vector3(0, 0, 0);
                    if (Input.GetKeyDown(KeyCode.E)) {
                        camMove = new Vector3(1, 0, 0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Q)) {
                        camMove = new Vector3(-1, 0, 0);
                    }
                    defaultCamPos = cam.position;
                    cam.Translate(camMove);
                    state = State.LEANING;
                }
                #endregion
                break;

            case State.MANTLING:
                Debug.Log("Mantling");
                animator.SetTrigger("Mantle Trigger");
                controller.enabled = false;
                state = State.STANDING;
                break;
            
            case State.LEANING:
                Debug.Log("Leaning");
                if (Input.GetKeyUp(KeyCode.Q) && cam.localPosition.x < defaultCamPos.x) {
                    cam.position = defaultCamPos;
                    state = State.STANDING;
                }
                else if (Input.GetKeyUp(KeyCode.E) && cam.localPosition.x > defaultCamPos.x) {
                    cam.position = defaultCamPos;
                    state = State.STANDING;
                }
                break;
        }
    }

    public void ResetState() 
    {
        controller.enabled = true;
    }

}
