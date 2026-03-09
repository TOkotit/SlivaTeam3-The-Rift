using System.Collections;
using UnityEngine;

namespace Utils.MiscClasses
{
    public class Waiter
    {
        public static IEnumerator WaitForSec(float sec)
        {
            yield return new WaitForSeconds(sec);
        }
    }
}