using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMAOnline.Managers.Structs
{
    /* The reason for security structures is 
     * mainly based on the fact of if people 
     * crack the xex, we have a few structs
     * to combat the LMAO chal response. */

    public struct chalStruct1
    {
        string cpukeyRAW, cpukeyMEM;
        int moboType;
        byte[] rndConsoleDat; // We'll see what this is later.
        bool isSlim;
    }
}
