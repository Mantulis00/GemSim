
using System;

[Flags]
enum KeyboardRotations // only for rotating
{
    Hold = 0,
    RotateLeft = 1,
    RotateRight = 2,
    RotateUp = 4,
    RotateDown = 8,
}
enum KeyboardAction
{
    Hold,
    Reset,
    Rotate,
    Spawn,
    Move,
    Delete,
    ManageList,
}

enum KeyboardSwitch
{
    Hold,
    ChangeMode,
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
    // Delete
    Delete = 8,
    //Add to List
    ListAdd = 16,
    // Remove from list
    ListRemove = 32,
}


enum MouseAction
{
    Hold = 0,
    Select = 1,
    Unselect = 2,
    Delete = 4,
}

enum Mode
{
    Hold,
    Edit,
    Simulate,
    Select,
}

