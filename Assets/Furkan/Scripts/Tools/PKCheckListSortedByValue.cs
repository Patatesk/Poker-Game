using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace PK.Tools
{
    public static class PKCheckListSortedByValue 
    {
       
        // Returns true if the numbers in the list are ordered from smallest to largest
        public static bool CheckListSortedByValue<T>(this List<int> nums)
        {
            nums.Sort();
            for (int i = 1; i < nums.Count; i++)
                if (nums[i] - nums[i - 1] != 1)
                    return false;
            return true;
        }
    }
}
