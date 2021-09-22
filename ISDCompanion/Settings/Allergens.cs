using System;
namespace ISDCompanion
{
    [Flags]
    public enum Allergens
    {
        None = 0,
        A1 = 1 << 0,
        A2 = 1 << 1,
        A3 = 1 << 2,
        A4 = 1 << 3,
        A5 = 1 << 4,
        A6 = 1 << 5,
        A7 = 1 << 6,
        A8 = 1 << 7,
        A9 = 1 << 8,
        A10 = 1 << 9,
        A11 = 1 << 10,
        A12 = 1 << 11,
        A13 = 1 << 12,
        A14 = 1 << 13
    }
}
