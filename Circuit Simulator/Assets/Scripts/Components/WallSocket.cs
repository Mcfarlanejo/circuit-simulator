using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSocket : Component
{
    public List<WallPlug> plugs = new List<WallPlug>(); 
    // Start is called before the first frame update
    new private void Start()
    {
        base.Start();

        componentName = "Wall Socket";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void Interact()
    {
        throw new System.NotImplementedException();
    }
}