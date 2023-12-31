using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * Required to have a dynamic number of instances, each created on a different loop
 * Once the R key is pressed (testing) then the player has the loop play out, but saves the data
 * for the current loop at the same time.
 * This saved data must be kept separate to the previous replay loop and also be played out once the R key is pressed again
 * Limited value based on the level.
 * 
 * Summary:
 * Each loop record player, on key press, replay previous actions
 * Include the actions of all loops per round
 * Store each loop and actions separately
 */

/*
 * Always recording except on the last loop and playing them out each loop, except the first loop
 */


/*
 * Now we want to track bullets 
 */

public class Player_Replay : MonoBehaviour
{

    public GameObject player_clone;
    public Animator animator;
    private GameObject player_instance;
    private player_clone_controller clone_script;
    private int currentFrame = 0;
    //store all clones and its data
    private int currentLoop = -1;
    //Starting with 5 by default
    // 5 clones, each with 1 replay list 
    private clone_data[] clones = new clone_data[10];
    private bool isMoving = true;
    private bool loopTrigger = false;

    private GameObject child;
    private string currentWeapon;
    private bool clicked = false;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        child = transform.Find("Revolver").gameObject;
        for (int i = 0; i < 10; i++)
        {
            player_instance = Instantiate(player_clone);
            player_instance.SetActive(false);
            clone_script = player_instance.GetComponent<player_clone_controller>();

            clones[i] = new clone_data(player_instance, clone_script);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (loopTrigger)
        {
            if(currentLoop + 1 < clones.Length)
            {
                loopTrigger = false;
                // Simulating a reset of the loop, progressing to next one
                if (currentLoop == -1)
                {
                    transform.position = new Vector3(0, 0, 0);
                    currentLoop++;
                }
                else
                {
                    transform.position = new Vector3(0, 0, 0);
                    currentLoop++;
                    resetClones();
                }
            }
            else
            {
                SceneManager.LoadScene("Complete");
            }     
            //reset current frame every reset
            currentFrame = 0;
        }
        
        // Activate clones until 1+ final index of clones
        if(currentLoop <= clones.Length)
        {
            //play and track
            for (int i = 0; i <= Math.Min(currentLoop, clones.Length - 1); i++)
            {
                if (!clones[i].instance.activeSelf)
                {
                    clones[i].instance.SetActive(true);
                }
                
            }
        }
    }

    private void FixedUpdate()
    {

        if (currentLoop+1 < clones.Length) 
        {
            clones[currentLoop+1].frames.Add(new ReplayActions { 
                position = transform.position, 
                rotation = transform.rotation, 
                animState = animator.GetCurrentAnimatorStateInfo(0),
                flipX = spriteRenderer.flipX,
                weaponPosition = new Vector2(100f,100f),
                itemRotation = Quaternion.Euler(0f, 0f, 0f),
                currentWeapon = "none"
            });
        }
        if (currentLoop != -1)
        {
            if(++currentFrame <= clones[currentLoop].frames.Count-1 && currentLoop != -1){
                SetTransform(currentLoop);
            }
            else
            {
                currentFrame = 0;
            }
        }
        if (currentLoop >= clones.Length)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
            
    private void SetTransform(int index){
        int loop = Math.Min(index, clones.Length - 1);
        // play frame for each instance up to the current loop (index)
        for (int i = 0; i <= loop; i++)
        {
            clones[i].instance.transform.position = clones[i].frames[currentFrame].position;
            clones[i].instance.transform.rotation= clones[i].frames[currentFrame].rotation;
            clones[i].script.UpdateAnim(clones[i].frames[currentFrame].animState, clones[i].frames[currentFrame].flipX, child);
            clones[i].script.HandleWeapon(clones[i].frames[currentFrame].weaponPosition,
                                            clones[i].frames[currentFrame].currentWeapon, 
                                            clones[i].frames[currentFrame].itemRotation);
        }
    }
    
    private void resetClones()
    {

        for (int i = 0; i < clones.Length; i++)
        {
            clones[i].instance.SetActive(false);
            clones[i].instance.transform.position = new Vector3(0, 0, 0);
        }
    }

    public void TriggerEffect()
    {
        Debug.Log("Triggered by timer");
        loopTrigger = true;
    }

    public void triggerClick()
    {
        //check the weapon and get data accordingly
        currentWeapon = child.GetComponent<PlayerAttack>().currentWeapon;

        if (!currentWeapon.Equals("wall"))
        {
            Vector2 weaponPosition = child.GetComponent<PlayerAttack>().lastPositionShot;

            clones[currentLoop + 1].frames.Add(new ReplayActions
            {
                position = transform.position,
                rotation = transform.rotation,
                animState = animator.GetCurrentAnimatorStateInfo(0),
                flipX = spriteRenderer.flipX,
                weaponPosition = weaponPosition,
                itemRotation = Quaternion.Euler(0f, 0f, 0f),
                currentWeapon = currentWeapon
            });
        }        
    }

    public void triggerClickWall(Vector2 position, Quaternion rotation)
    {
        //check the weapon and get data accordingly
        currentWeapon = child.GetComponent<PlayerAttack>().currentWeapon;
        clones[currentLoop + 1].frames.Add(new ReplayActions
        {
            position = transform.position,
            rotation = transform.rotation,
            animState = animator.GetCurrentAnimatorStateInfo(0),
            flipX = spriteRenderer.flipX,
            weaponPosition = position,
            itemRotation = rotation,
            currentWeapon = currentWeapon
        });
    }
}