using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class GetItemTexture : ScriptableObject
{
    [SerializeField] public Sprite[] item;
}
