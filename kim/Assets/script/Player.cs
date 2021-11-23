using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{
    Animator anim;
    public float speed = 5;
    Camera viewCamera;
    GunController gunController;
    PlayerController controller;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        viewCamera = Camera.main;
        gunController = GetComponent<GunController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)))
        {
            anim.SetBool("Walk", true);

        }
        else if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W)))
        {

            anim.SetBool("Walk", false);
        }
        Vector3 moveinput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveinput.normalized * speed;
        controller.Move(moveVelocity);

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if(groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);

        }
        if(Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }

    }
}
