using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Succeeder : DecoratorTask
{
    public override bool Run()
    {
        ChildNode.Run();
        return true;
    }

}
