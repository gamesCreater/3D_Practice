using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private Animator anima;
    private CharacterController aiChController;

    
    private Vector3 moveDir;
    public float gravityForce;
    private Vector3 directionRotate;


    private float shootTimer;
    private float power = 50f;
    private float distance;
    private bool live;    

    public GameObject bullet;
    public GameObject gun;
    public NavMeshAgent navig;
    public GameObject player;
    public CharController chContr;
    public Resurrection res;



    private void Awake()
    {        
        anima = GetComponent<Animator>();
        aiChController = GetComponent<CharacterController>();
        navig = GetComponent<NavMeshAgent>();
        live = true;
        res = Camera.main.GetComponent<Resurrection>();
        navig.enabled = false;

    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;
        
        
        PlayerSearch();        
        AiBehavior();
        charGravity();


    }

    public void Dead()
    {
        live = false;
        navig.enabled = false;        
        anima.SetTrigger("Dead");
        Destroy(gameObject, 5f);
        res.WaitRes();
    }

    public void charGravity()
    {
        if (!aiChController.isGrounded)
        {
            gravityForce -= 20f * Time.fixedDeltaTime;
        }
        else gravityForce = -1f;
    }

    public void AiBehavior()
    {
        transform.rotation = Quaternion.LookRotation(directionRotate);

        if (live && chContr.livePlayer)
        {
            if (distance > 5f || distance < 30f)
            {
                if (live)
                {
                    AiShoot();
                }
            }

            if (distance > 20f)
            {
                navig.enabled = true;
                navig.SetDestination(player.transform.position);
                anima.SetBool("Run", true);
            }
            else
            {
                if (distance <= 20f)
                {
                    navig.enabled = false;
                    anima.SetBool("Run", false);
                }
            }
        }

        if (!chContr.livePlayer)
        {
            navig.enabled = false;
            anima.SetBool("Run", false);
        }
    }

    public void AiShoot()
    {
       if (ShootDelay())
       {    
            GameObject bul = Instantiate(bullet, gun.transform.position, gun.transform.rotation);

            shootTimer = 1f;

            Rigidbody rb = bul.GetComponent<Rigidbody>();

            rb.AddForce(bul.transform.forward * power, ForceMode.Impulse);

            Destroy(bul, 5f);
           
       }        
    }

    public bool ShootDelay()
    {
        if (shootTimer <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlayerSearch()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Transform>();
        chContr = player.GetComponent<CharController>();

        directionRotate = player.transform.position - gameObject.transform.position;
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        moveDir.y = gravityForce;
        aiChController.Move(moveDir * Time.fixedDeltaTime);

    }

}
