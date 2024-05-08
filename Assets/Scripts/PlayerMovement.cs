using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] DynamicJoystick joystick;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] GameObject childGameObject;
    [SerializeField] Animator myAnimator;
    public bool isMoving;
    public bool isCarrying;


    private void Update()
    {
        MovePlayer();
    }


    void MovePlayer()
    {   
        //movement
        Vector3 direction = new Vector3( joystick.Direction.x , 0f , joystick.Direction.y );
        transform.Translate(direction * Time.deltaTime * movementSpeed);

        //rotation
        Vector3 targetDirection = Vector3.RotateTowards(childGameObject.transform.forward, direction, rotationSpeed * Time.deltaTime, 0f);
        childGameObject.transform.rotation = Quaternion.LookRotation(targetDirection);

        //handle animation
        if(joystick.Direction == Vector2.zero )
        {
            if( myAnimator.GetBool("walking"))
            {
                myAnimator.SetBool("walking", false);
            }
            
        }
        else
        {
            if (!myAnimator.GetBool("walking"))
            {
                myAnimator.SetBool("walking", true);
            }
        }
        
    }
}
