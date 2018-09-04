using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inverter : DecoratorTask
{
    public override bool Run()
    {
        return !ChildNode.Run();
    }

}
