using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PokerGame
{
    public static class CheckifHasAParent
    {
        public static Transform CheckParent(Transform transform)
        {
            Transform parent = transform;
            Transform prevParent = transform;
            int i = 1;
            while (parent != null)
            {
                prevParent = parent;
                parent = parent.parent;
                ++i;
            }
            if (parent == null) return prevParent;
            else return parent;
        }
    }
}
