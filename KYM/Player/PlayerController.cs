using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class PlayerController
{
    public static bool MoveRight()  { return Input.GetKey(KeyCode.D); }
    public static bool MoveLeft()    { return Input.GetKey(KeyCode.A); }
    public static bool MoveForward()     { return Input.GetKey(KeyCode.W); }
    public static bool MoveBackward() { return Input.GetKey(KeyCode.S); }
    public static bool Jump()        { return Input.GetKeyDown(KeyCode.Space); }
    public static bool Attack()       { return Input.GetMouseButton(0); }
    public static bool Flash()        { return Input.GetKeyDown(KeyCode.LeftShift); }
    public static bool Slow()         { return Input.GetKey(KeyCode.CapsLock); }
    public static float TurnX()        { return Input.GetAxis("Mouse Y");}
    public static float TurnY()        { return Input.GetAxis("Mouse X"); }

}



