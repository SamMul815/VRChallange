using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Failer : DecoratorTask
{
    public override bool Run()
    {
        ChildNode.Run();
        return false;
    }

}
