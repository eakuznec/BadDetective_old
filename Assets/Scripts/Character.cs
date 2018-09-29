using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public Sprite characterAvatar;
    public CharacterSex sex;
    public int age;
    [TextArea]
    public string characterStory;
}

public enum CharacterSex
{
    male,
    female
}
