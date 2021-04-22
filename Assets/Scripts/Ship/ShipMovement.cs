using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] Camera mainCamera = null;
    Rigidbody2D shipRigidBody2D = null;
    Transform shootingPoint;
    [SerializeField]GameObject engineThrustAnimatorObject = null;
    Animator engineThrustAnimator;
    [SerializeField]Transform leftBorder = null, rightBorder = null, topBorder = null, bottomBorder = null;
    [SerializeField]HUDController hudController = null;
    SoundController soundController;
    AudioSource shipThrustsAudioSource;
    float shipSizeOffSet;
    float movSpeed, rotationSpeed;
    float xAxisDirection, yAxisDirection;
    float boostForce, boostDelay, boostingTime;
    bool isBoosting, canBoost;

    void Awake(){
        shipRigidBody2D = GetComponent<Rigidbody2D>();   
        shootingPoint = GetComponentInChildren<Transform>(); 
        movSpeed = 8f;
        rotationSpeed = 300f;
        shipSizeOffSet = 0.5f;

        boostForce = 8f;
        boostDelay = 3f;
        boostingTime = 0.3f;
        canBoost = true;

        engineThrustAnimator = engineThrustAnimatorObject.GetComponent<Animator>();
        engineThrustAnimatorObject.SetActive(false);

        shipThrustsAudioSource = GetComponent<AudioSource>();
        shipThrustsAudioSource.enabled = false;
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
    
        AdjustMovementLimitationBorders();
    }
    
    void Update(){
        xAxisDirection = Input.GetAxis("Horizontal");
        yAxisDirection = Input.GetAxis("Vertical");
        if(yAxisDirection > 0){
            MoveShip(yAxisDirection);
            engineThrustAnimatorObject.SetActive(true);
            shipThrustsAudioSource.enabled = true;
        }else{
            engineThrustAnimatorObject.SetActive(false);
            shipThrustsAudioSource.enabled = false;
        }
        
        if(xAxisDirection != 0){
            RotateShip(xAxisDirection);
        }else{
            RotateShip(0f);
        }

        if(isBoosting){
            hudController.DecreaseBoostBar(Time.deltaTime * 4);
        }else{
            hudController.IncreaseBoostBar(Time.deltaTime/(boostDelay + 1.3f));
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            if(canBoost){
                StartCoroutine(BoostShip(boostForce));
            }
        }
    }

    void MoveShip(float yDirection){
        Vector2 shipDirectionVector = new Vector2(0f, yDirection * movSpeed * Time.deltaTime);
        shipRigidBody2D.transform.Translate(shipDirectionVector, Space.Self);
        
        //TODO: Find a way of clampping the Ship in the limits of the camera, not transform object points
        float xPosClampped = Mathf.Clamp(transform.position.x, leftBorder.position.x + shipSizeOffSet, rightBorder.position.x - shipSizeOffSet);
        float yPosClampped = Mathf.Clamp(transform.position.y, bottomBorder.position.y + shipSizeOffSet, topBorder.position.y - shipSizeOffSet);
        transform.position = new Vector2(xPosClampped, yPosClampped);
    }

    void RotateShip(float zRotationAngle){
        Vector3 shipRotationVector = new Vector3(0f, 0f, -zRotationAngle * rotationSpeed * Time.deltaTime); 
        shipRigidBody2D.transform.Rotate(shipRotationVector);
    }

    void AdjustMovementLimitationBorders(){
        //Test funcion for adjusting the limitation borders of the ship's movement with the camera size
        float verticalHeightSeen = mainCamera.orthographicSize * 2.0f;
        float horizontalHeightSeen = mainCamera.orthographicSize * (Screen.width / Screen.height);
        //Debug.Log(verticalHeightSeen);
        //Debug.Log(horizontalHeightSeen);

        leftBorder.position = new Vector2(-horizontalHeightSeen - 2.5f, 0f);
        rightBorder.position = new Vector2(+horizontalHeightSeen + 2.5f, 0f);
        topBorder.position = new Vector2(0f, +verticalHeightSeen/2f);
        bottomBorder.position = new Vector2(0f, -verticalHeightSeen/2f);
    }

    IEnumerator BoostShip(float boostForce){
        soundController.playSFX("shipBoost");
        isBoosting = true;
        engineThrustAnimator.SetBool("isBoosting", true);
        Vector2 boostThrust = transform.up * boostForce;
        shipRigidBody2D.AddForce(boostThrust, ForceMode2D.Impulse);
        yield return new WaitForSeconds(boostingTime);
        isBoosting = false;
        canBoost = false;
        yield return new WaitForSeconds(1.3f);
        engineThrustAnimator.SetBool("isBoosting", false);
        yield return new WaitForSeconds(boostDelay);
        soundController.playSFX("shipBoostRecharge");
        canBoost = true;
    }
}
