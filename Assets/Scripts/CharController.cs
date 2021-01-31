using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private float speed = 5f;
    private Vector3 moveDir;
    private float gravityForce;
    private Animator anima;
    private CharacterController chControl;

    public GameObject bullet;
    public GameObject gun;
    public float power;
    public bool livePlayer;
    

    private float shootTimer;

    private void Awake()
    {
        chControl = GetComponent<CharacterController>();
        anima = GetComponent<Animator>();
        
        livePlayer = true;
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;

        //Debug.Log(shootTimer);
    }

    private void FixedUpdate()
    {

        if (livePlayer)
        {
            CharMove();
            Shoot();
        }
        
        charGravity();
        
    }

    private void CharMove()
    {
        moveDir = Vector3.zero;

        moveDir.x = Input.GetAxis("Horizontal") * speed;
        moveDir.z = Input.GetAxis("Vertical") * speed;

        if (moveDir.z != 0 || moveDir.x != 0)
        {
            anima.SetBool("Run", true);
        }
        else
        {
            anima.SetBool("Run", false);
        }

        if (Vector3.Angle(Vector3.forward, moveDir) > 1f || Vector3.Angle(Vector3.forward, moveDir) == 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveDir, speed, 0.0f);
            transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 3.5f);           
        }

        //Quaternion.LookRotation(direction);

        moveDir.y = gravityForce;

        chControl.Move(moveDir * Time.fixedDeltaTime);
    }

    public void charGravity()
    {
        if (!chControl.isGrounded)
        {
            gravityForce -= 20f * Time.fixedDeltaTime;
        }
        else gravityForce = -1f;
    }

    public void Shoot()
    {
        if (ShootDelay())
        {
            if (Input.GetButton("Space"))
            {
                GameObject bul = Instantiate(bullet, gun.transform.position, gun.transform.rotation);

                shootTimer = 0.1f;

                Rigidbody rb = bul.GetComponent<Rigidbody>();

                rb.AddForce(bul.transform.forward * power, ForceMode.Impulse);

                Destroy(bul, 5f);
            }
        }

        
    }

    public void Dead()
    {
        livePlayer = false;
        anima.SetTrigger("Dead");
        
    }

    public bool ShootDelay()
    {
        if (shootTimer < 0)
        {
            return true;
        }
        else
        {
            return false;            
        }
    }

}
 