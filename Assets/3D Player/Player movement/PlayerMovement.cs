using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Phyical and environment Management
public class PlayerMovement : MonoBehaviour
{
    //public AudioClip shoutingClip;
    public float speedDampTime = 0.01f;
    public float sensitivityX = 2.0f;
    private float elapsedTime = 1.0f;
    private bool noBackMove = true;
    private bool noRunning = true;

    public Vector3 Jump;
    public float JumpForce = 2.0f;
    public bool IsEthanOnFloor;

    private Animator anim;
    private HashIDs hash;

    private void Awake()
    {///
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();

        anim.SetLayerWeight(1, 1f);
    }/// 
    
    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");
        float mouseX = Input.GetAxis("MouseX");
        bool Backwards = false;
        bool Running = false;

        Rotating(mouseX);
        MovementManager(v, Backwards, sneak, Running);
    }

    void OnCollisionStay()
    {
        IsEthanOnFloor = true;
    }
    void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        anim.SetBool(hash.shoutingBool, shout);
    }

    // Player movement Management
    
    void MovementManager(float vertical, bool Backwards, bool sneaking, bool Running)
    {

        // Movement settings for Sneaking with animation
        anim.SetBool(hash.sneakingBool, sneaking);

        if (vertical > 0)
        {
            anim.SetFloat(hash.speedFloat, 1.5f, speedDampTime, Time.deltaTime);
        }
        else
        {
            anim.SetFloat(hash.speedFloat, 0);
        }

        // Movement settings for Walking Backwards with animation
        anim.SetBool(hash.backwardBool, Backwards);

        if (vertical > 0)
        {
            noBackMove = true;
            anim.SetFloat(hash.speedFloat, 2.5f, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", false);
        }
        if (vertical < 0)
        {
            if (noBackMove == true)
            {
                elapsedTime = 0;
                noBackMove = false;
            }
            anim.SetFloat(hash.speedFloat, -2.0f, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", true);

            Rigidbody ourBody = this.GetComponent<Rigidbody>();
            float movement = Mathf.Lerp(-1.5f, -0.5f, elapsedTime);
            Vector3 moveBack = new Vector3(0.0f, -0.5f, movement);
            moveBack = ourBody.transform.TransformDirection(moveBack);
            ourBody.velocity = moveBack;
        }
        if (vertical == 0)
        {
            noBackMove = true;
            anim.SetFloat(hash.speedFloat, 0.01f);
            anim.SetBool(hash.backwardBool, false);
        }

        // Movement settings for Running with animation -
        // Doesn't work correctly - Causes player to only have control over Run and Jump whilst disabling all other player controls and animations

        //anim.SetBool(hash.runningState, Running);

        //if (vertical > 1)
        //{
        //    noRunning = true;
        //    anim.SetFloat(hash.speedFloat, 2.5f, speedDampTime, Time.deltaTime);
        //    anim.SetBool("Running", false);
        //}
        //if (vertical < 1)
        //{
        //    if (noRunning == true)
        //    {
        //    elapsedTime = 0;
        //    noRunning = false;
        //    }
        //    anim.SetFloat(hash.speedFloat, 2.0f, speedDampTime, Time.deltaTime);
        //    anim.SetBool("Running", true);
        //
        //    Rigidbody ourBody = this.GetComponent<Rigidbody>();
        //    float movement = Mathf.Lerp(0.0f, 0.0f, elapsedTime);
        //    Vector3 MoveRun = new Vector3(0.0f, 0.0f, movement);
        //    MoveRun = ourBody.transform.TransformDirection(MoveRun);
        //    ourBody.velocity = MoveRun;
        //}
        //if (vertical == 0)
        //{
        //    noRunning = true;
        //    anim.SetFloat(hash.speedFloat, 0.01f);
        //    anim.SetBool(hash.runningState, false);
        //}

        // Movement settings for Jumping without animation
        Jump = new Vector3(0.0f, 2.0f, 0.0f);

        if (Input.GetKeyDown(KeyCode.Space) && IsEthanOnFloor)
        {
            Rigidbody ourBody = this.GetComponent<Rigidbody>();
            ourBody.AddForce(Jump * JumpForce, ForceMode.Impulse);
            IsEthanOnFloor = false;
        }
    }



    // Update is called once per frame
    void Rotating (float mouseXInput)
    {
        //access the avatar's rigidbody
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        //first check to see if we have rotation data that needs to be applied
        if (mouseXInput != 0)
        {
            
            //if so we use mouseX value to create a Euler angle which provides rotation in the Y axis
            //which is then turned to a Quaternion
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivityX, 0f);

            //and then applied to turn the avatar via the rigidbody
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);

        }
    }
}
