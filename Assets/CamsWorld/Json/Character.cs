using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameronsWorld
{
    [CreateAssetMenu(fileName = "Character", menuName = "Cameron's World/Character", order = 1)]
    public class Character : ScriptableObject
    {
        [SerializeField] List<Sprite> Poses;
        [SerializeField] List<Sprite> Expressions;

    }
}