using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Viking_Controller : MonoBehaviour
{

    Vector3 movingDirection, forwardDir, newObjectPos;
    public float jumpingForce = 500f, movingSpeed = 10f, turnSpeed = 5f;
    float originalY, recoverTime = 0f, scoreTime = 0f, timeToMenu = 0f, deadTime = 0f;//record angel
    public static int score = 0, bestScore = 0;
    public static bool gaming = false, mistake = false, isDead = false, killed = false;
    bool onTheGround = false, run = false, isRotating = false, isJumping = false;
    private Quaternion targetAngel;
    public Rigidbody rb;
    private Animator animator;
    GameObject initLand, initBridge, initRoad, startSensor;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        gaming = false;
        score = 0; recoverTime = 0f; scoreTime = 0f; timeToMenu = 0f; deadTime = 0f;
        gaming = false; mistake = false; isDead = false; killed = false;
        onTheGround = false; run = false; isRotating = false; isJumping = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        originalY = transform.rotation.y;//set y first
        newObjectPos = new Vector3(-72f, 0f, -10f);
        initLand = GameObject.Find("big_module_01");
        initBridge = GameObject.Find("wooden_bridge_02");
        initRoad = GameObject.Find("Road_with_oneend");
        startSensor = GameObject.Find("StartSensor");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "rock_05(Clone)" || collision.transform.name == "fence_03(Clone)")
        {
            recoverTime = 0f;
            if (mistake)
            {
                killed = true;
                isDead = true;
                deadTime = timeToMenu;
            }
            else
                mistake = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "StartSensor")
        {
            gaming = true;
            run = true;
            switch (originalY)
            {
                case 0:
                case 360:
                    targetAngel= Quaternion.Euler(0, -90 + originalY, 0) * Quaternion.identity;
                    break;
                case 90:
                case -270:
                    targetAngel = Quaternion.Euler(0, -180 + originalY, 0) * Quaternion.identity;
                    break;
                case 180:
                case -180:
                    targetAngel = Quaternion.Euler(0, 90 + originalY, 0) * Quaternion.identity;
                    break;
                default: break;
            }
            originalY = -90;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetAngel, Time.deltaTime * turnSpeed * 10);
            transform.position = new Vector3(-2.5f, 0, -10);
            //ªì©l5¦a¹Ï
            for (int i = 0; i < 5; i++)
                GameObject.Find("World").GetComponent<Infinite_Road>().autoGenerate();
        }
        if (other.transform.name == "Collectable(Clone)" || other.transform.name == "Collectable")
        {
            score += 50;
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.name == "ClearOrigionalItem")
        {
            Destroy(initLand, 3f);
            Destroy(initBridge, 3f);
            Destroy(initRoad, 3f);
            Destroy(startSensor, 3f);
            Destroy(other.gameObject, 3f);
        }
        if(other.transform.name == "EndSensor")
        {
            Destroy(other.transform.parent.gameObject, 3f);
            GameObject.Find("World").GetComponent<Infinite_Road>().autoGenerate();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!gaming)
            run = false;
        if (transform.position.y <= -3)
        {
            isDead = true;
            run = false;
            isJumping = false;
            if(deadTime == 0)
                deadTime = timeToMenu;
        }
        if (!isDead)
        {
            //get new forward of the viking
            forwardDir = getNewDirection(originalY);
            //move part
            if (Input.GetKey(KeyCode.A))
            {
                //forwardDir = getNewDirection(originalY);
                movingDirection = Quaternion.AngleAxis(-90, Vector3.up) * forwardDir;
                transform.localPosition += movingSpeed * Time.deltaTime * movingDirection;
                run = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                //forwardDir = getNewDirection(originalY);
                movingDirection = Quaternion.AngleAxis(90, Vector3.up) * forwardDir;
                transform.localPosition += movingSpeed * Time.deltaTime * movingDirection;
                run = true;
            }
            if (Input.GetKey(KeyCode.W) || gaming)
            {
                transform.localPosition += movingSpeed * Time.deltaTime * forwardDir;//keep going foward direction of character
                run = true;
            }
            if (Input.GetKey(KeyCode.S) && !gaming)
            {

                movingDirection = Quaternion.AngleAxis(180, Vector3.up) * forwardDir;
                transform.localPosition += movingSpeed * Time.deltaTime * movingDirection;
                run = true;
            }
            //rotate part
            if (Input.GetKey(KeyCode.Q) && !isRotating)//left rotate
            {
                targetAngel = Quaternion.Euler(0, -90 + originalY, 0) * Quaternion.identity;
                originalY -= 90;
                originalY %= 360;
                isRotating = true;
            }
            else if (Input.GetKey(KeyCode.E) && !isRotating)//right rotate
            {
                targetAngel = Quaternion.Euler(0, 90 + originalY, 0) * Quaternion.identity;
                originalY += 90;
                originalY %= 360;
                isRotating = true;
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetAngel, Time.deltaTime * turnSpeed);
                if (Quaternion.Angle(targetAngel, transform.rotation) < 1)
                {
                    transform.rotation = targetAngel;
                    isRotating = false;
                }
            }
            //jumping
            onTheGround = IsGround();
            
            if (Input.GetKeyDown(KeyCode.Space) && onTheGround)
            {
                rb.AddForce(Vector3.up * jumpingForce);
                isJumping = true;
            }
            
        }
        //set animation
        if (IsGround())
        {
            isJumping = false;
            if (gaming)
                run = true;
        }
        else
            isJumping = true;
        //set animation
        animator.SetBool("Jump", isJumping);
        animator.SetBool("Run", run);
        animator.SetBool("Dead", isDead);
        //gain score per second
        scoreTime += Time.deltaTime;
        recoverTime += Time.deltaTime;
        timeToMenu += Time.deltaTime;
        if (scoreTime >= 1f && !isDead && gaming)
        {
            score += 10;
            scoreTime = 0f;
        }
        if (recoverTime >= 5)
        {
            mistake = false;
            recoverTime = 0f;
        }
        //record highest score and beck to menu
        if (isDead && timeToMenu > (deadTime + 3))
        {
            PlayerPrefs.SetInt("BestScore", bestScore);
            GameObject.Find("BackToMenu").GetComponent<BackToMenu>().back();
        }
    }
    bool IsGround()
    {
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGround = Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0f), Vector3.down, (height / 2) + 0.1f);
        return isGround;
    }
    public Vector3 getNewDirection(float y)
    {
        int dir = (int)y;
        Vector3 tmp = Vector3.zero;
        switch (dir)
        {
            case 0:
            case 360: tmp = new Vector3(0, 0, 1); break;
            case -270:
            case 90: tmp = new Vector3(1, 0, 0); break;
            case -180:
            case 180: tmp = new Vector3(0, 0, -1); break;
            case -90:
            case 270: tmp = new Vector3(-1, 0, 0); break;
        }
        return tmp;
    }
}
