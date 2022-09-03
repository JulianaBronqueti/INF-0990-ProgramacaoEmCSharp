BorderSides leftRight = BorderSides.Left | BorderSides.Right;   // Enum combinados via operador OR binário
Console.WriteLine(leftRight);

if ((leftRight & BorderSides.Left) != 0)
  Console.WriteLine("Includes Left");                           // Includes Left

Console.WriteLine(leftRight.ToString());                        // 3 = "Left, Right"

BorderSides s = BorderSides.Left;
s |= BorderSides.Right;
Console.WriteLine(s);
Console.WriteLine(s == leftRight);                              // True

s ^= BorderSides.Right;                                         // Remove Right
Console.WriteLine(s);                                           // Left

public enum BorderSides { None = 0, Left = 1, Right = 2, Top = 4, Bottom = 8 }