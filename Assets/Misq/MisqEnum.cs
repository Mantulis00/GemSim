

using System;

namespace Assets.Misq
{
    public static class MisqEnum
    {
        public static int RotateEnumValues (int enumValue, Type enumType) // rotate through enum values, holding enum value 0 as error passing it 
        {
            int size =  Enum.GetNames(enumType).Length;

            enumValue++;
            if (enumValue >= size) enumValue = 1;

            return enumValue;
        }
    }
}
