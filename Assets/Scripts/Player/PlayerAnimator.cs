using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement movement;
    private Animator anim;

    [Header("Particle FX")]
    [SerializeField] private GameObject jumpFX;
    [SerializeField] private GameObject landFX;
    //[SerializeField] private GameObject runFX;

    public bool startedJumping { private get; set; }
    public bool justLanded { private get; set; }
    public bool isRunning { private get; set; }

    [HideInInspector] public float currentVelX;
    [HideInInspector] public float currentVelY;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        anim = GetComponentInChildren<Animator>();
    }

    private void LateUpdate()
    {
        CheckAnimationState();
    }

    private void CheckAnimationState()
    {
        if (startedJumping)
        {
            anim.SetTrigger("Jump");
            GameObject obj = Instantiate(jumpFX, transform.position - (Vector3.up * transform.localScale.y / 2), Quaternion.Euler(-90, 0, 0));
            Destroy(obj, 1);
            startedJumping = false;
            return;
        }

        if (justLanded)
        {
            anim.SetTrigger("Land");
            GameObject obj = Instantiate(landFX, transform.position - (Vector3.up * transform.localScale.y / 1.5f), Quaternion.Euler(-90, 0, 0));
            Destroy(obj, 1);
            justLanded = false;
            return;
        }

        if (movement.IsSliding)
        {
            anim.Play("Base Layer.Slide"); //Playing over and over
        }

        if (Math.Abs(movement.RB.velocity.x) > .1 && isRunning)
        {
            anim.Play("Base Layer.Run");
            //GameObject obj = Instantiate(runFX, transform.position - (Vector3.up * transform.localScale.y / 1.5f), Quaternion.Euler(-90, 0, 0));
            //Destroy(obj, 1);
            isRunning = false;
            return;
        }

        currentVelX = movement.RB.velocity.x;
        currentVelY = movement.RB.velocity.y;

        anim.SetFloat("Vel X", movement.RB.velocity.x);
        anim.SetFloat("Vel Y", movement.RB.velocity.y);
    }
}
