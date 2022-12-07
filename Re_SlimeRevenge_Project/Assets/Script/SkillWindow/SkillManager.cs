using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public List<SkillData> skill = new List<SkillData>();

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void Awake()
    {
        instance = this;
    }
}
