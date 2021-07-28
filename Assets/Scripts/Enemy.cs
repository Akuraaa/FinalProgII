using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minIdleTime = 3;
    public float maxIdleTime = 5;
    public float viewAngle = 90;
    public bool stun;

    protected Animator _anim;
  //protected StateMachine _sm;
    protected AudioSource _audio;
  //protected PlayerController player;
  //protected EnemyDecisionTree _enemyTree;
    private void Awake()
    {
        //_sm = new StateMachine();
        _anim = GetComponent<Animator>();
        //player = FindObjectOfType<PlayerController>();
        _audio = GetComponent<AudioSource>();
        //_enemyyTree = 
    }

    private void Start()
    {
         
    }
}
