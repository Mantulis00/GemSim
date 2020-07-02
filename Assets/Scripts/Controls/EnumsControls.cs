
using System;

[Flags]
enum KeyboardRotations
{
    Hold = 0,
    RotateLeft = 1,
    RotateRight = 2,
    RotateUp = 4,
    RotateDown = 8,
}

[Flags]
enum ActionsQue
{
    Hold = 0,
    //Spawn
    SpawnStart = 1,
    SpawnFinish = 2,
    //Move
    Move = 4,
}


enum KeyboardActions
{
   Hold,
   Reset,
   Rotate,
   Spawn,
   Move,
}


enum MouseActions
{
    Hold = 0,
    Select = 1,
    Unselect = 2,
    Delete = 4,
}



