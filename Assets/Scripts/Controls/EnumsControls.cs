
using System;

[Flags]
enum KeyboardActions
{
    Hold = 0,
    RotateLeft = 1,
    RotateRight = 2,
    RotateUp = 4,
    RotateDown = 8,
}

enum MouseActions
{
    Hold = 0,
    Select = 1,
    Unselect = 2,
    Delete = 4,
}

