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
    Select,
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
    //Group
    GroupAdd = 16,
    GroupRemove = 32,
    //Select
    Select = 64,
}


enum MouseAction
{
    Hold = 0,
    Select = 1,
    Unselect = 2,
    Delete = 4,
}