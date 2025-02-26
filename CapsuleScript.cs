using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapsuleScript : MonoBehaviour
{
    
    CharacterController contr; 
    public float speed = 12f;
    Transform playerBody; 
    
    private float gravity = 10f;
    private float jumpHeight = 10f;
    private float velocity;
    int score = 0; 

    bool isGrounded;
    bool isCaptured = false;

    public TextMeshProUGUI Text;
    
    void Start()
    {
        contr = GetComponent<CharacterController>();
        playerBody = GetComponent<Transform>();
    }
    
    void Update()
    {   
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        contr.Move(playerBody.forward*vertical*Time.deltaTime*speed);
        contr.Move(playerBody.right*horizontal*Time.deltaTime*speed);             

        float mouseX = Input.GetAxis("Mouse X");
        playerBody.Rotate(0,mouseX,0);

        velocity -= gravity*Time.deltaTime;
        contr.Move(playerBody.up*velocity*Time.deltaTime);       

        if(isGrounded == true && Input.GetKeyDown("space")){
            velocity = jumpHeight;
        } else {
            velocity -= gravity*Time.deltaTime;
        }        

          isGrounded = false;    
          isCaptured= false;  
    }

    void OnControllerColliderHit(ControllerColliderHit col)
    {   
         
        if(col.gameObject.tag == "Ground"){
            isGrounded = true;
        }
        if (col.gameObject.tag == "Pig" && isCaptured== false) {           
        isCaptured = true;
        Destroy(col.gameObject);
        score = score + 1;
        Text.text = "Свиней поймано: " + score;
            if(score == 4){
                Text.text = "YOU WIN!!";
            }
        }

    }    
   
    
}
