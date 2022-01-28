using UnityEngine;
using System.Collections;

namespace CameronsWorld
{
    public interface ICoroutiner
    {
        public Coroutine StartCoroutine(IEnumerator enumerator);
    }
}
