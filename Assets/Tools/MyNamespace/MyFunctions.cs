namespace MyCommonFunction
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class MyFunction
    {
        public int[] Shuffle(int[] target)
        {
            for(int i = 0; i<target.Length; i++)
            {
                int random = Random.Range(0, target.Length);
                int temp = target[i];
                target[i] = target[random];
                target[random] = temp;
            }
            return target;
        }
    }
    
}

