using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clone_controller : MonoBehaviour
{
    public bool isMoving;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("isMoving", isMoving);
    }

    public void setMove(bool move){
        isMoving = move;
    }

}
