﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoRaquete : MonoBehaviour
{
    GameManager gm;
    [Range(1,10)]
    public float velocidade;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gameState != GameManager.GameState.GAME) return;
        float inputX = Input.GetAxis("Horizontal");
        transform.position += new Vector3(inputX,0,0) * Time.deltaTime * velocidade;

        if(Input.GetKeyDown(KeyCode.Escape)&& gm.gameState == GameManager.GameState.GAME){
            gm.changeState(GameManager.GameState.PAUSE);
        }
    }
}
