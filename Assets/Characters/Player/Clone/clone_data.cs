using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clone_data
{
    public GameObject instance;
    public player_clone_controller script;
    public List<ReplayActions> frames = new List<ReplayActions>();
    
    public clone_data(GameObject instance, player_clone_controller script)
    {
        this.instance = instance;
        this.script = script;
    }
}
