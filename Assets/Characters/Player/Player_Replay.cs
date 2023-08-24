using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Replay : MonoBehaviour
{
    // Duplicated buddy
    public GameObject playerClone;
    private GameObject cloneInstance;
    public clone_controller instanceScript;
    // Create a list of frames of ReplayActions for the player
    private List<ReplayActions> replays = new List<ReplayActions>();
    private bool isMoving = false;
    private bool isReplay = false;
    private int currentFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            isReplay = !isReplay;
            if(isReplay){
                cloneInstance = Instantiate(playerClone, new Vector3(0,0,0), Quaternion.identity);
                instanceScript = cloneInstance.GetComponent<clone_controller>(); 
                // SetTransform(0);
            }else{
                SetTransform(replays.Count - 1);
                DestroyImmediate(playerClone, true);
            }
        }
    }

    private void FixedUpdate(){
        if(!isReplay)
        {
            replays.Add(new ReplayActions {position = transform.position, rotation = transform.rotation, isMoving = true});
        }else{

            if(++currentFrame < replays.Count){
                SetTransform(currentFrame);
            }

        }
        
    }

    private void SetTransform(int index){
        currentFrame = index;
        ReplayActions action = replays[index];
        
        cloneInstance.transform.position = action.position;
        cloneInstance.transform.rotation = action.rotation;
        instanceScript.setMove(action.isMoving);
    }
}
