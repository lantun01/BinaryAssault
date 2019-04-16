using System;


[Flags]
public enum EditorListOption 
{
   None = 0,
   ListSize = 1,
   ListLabel = 2,
   ElementLabels = 4,
   Default = ListSize | ListLabel | ElementLabels,   //bitwise flag
   NoElementLabels = ListSize | ListLabel
}
