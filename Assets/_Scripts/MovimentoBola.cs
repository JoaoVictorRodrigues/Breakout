﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBola : MonoBehaviour
{   
    [Range(1,15)]
    public float velocidade = 5.0f;

    GameManager gm;

    private Vector3 direcao;

    // Start is called before the first frame update
    void Start()
    {
        float dirX = Random.Range(-5.0f, 5.0f);
        float dirY = Random.Range(1.0f, 5.0f);

        direcao = new Vector3(dirX,dirY).normalized;

        gm = GameManager.GetInstance();
    }

    void Update()
    {

        if(gm.gameState != GameManager.GameState.GAME) return;
        transform.position += direcao * Time.deltaTime * velocidade;

        Vector2 posicaoViewport = Camera.main.WorldToViewportPoint(transform.position);

       /* if(posicaoViewport.x < 0 || posicaoViewport.x > 1){
            direcao = new Vector3(-direcao.x,direcao.y);
        }

        if(posicaoViewport.y > 1){
            direcao = new Vector3(direcao.x,-direcao.y);
        }*/
        if(posicaoViewport.y < 0 ){
            Reset();
        }
    }

    private void Reset(){

        Vector3 playerPosition =  GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = playerPosition + new Vector3(0,0.5f,0);

        float dirX = Random.Range(-5.0f, 5.0f);
        float dirY = Random.Range(1.0f, 5.0f);

        direcao = new Vector3(dirX,dirY).normalized;

        gm.vidas--;

        //Debug.Log($"Vidas: {gm.vidas} \t | \t Pontos: {gm.pontos}");

        if(gm.vidas <= 0 && gm.gameState == GameManager.GameState.GAME){
            gm.changeState(GameManager.GameState.ENDGAME);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        
        if(col.gameObject.CompareTag("Player")){

            //float dirX = Random.Range(-5.0f, 5.0f);
            //float dirY = Random.Range(1.0f, 5.0f);

            direcao = new Vector3(direcao.x,-direcao.y);
        }

        else if (col.gameObject.CompareTag("Tijolo")){
            direcao = new Vector3(direcao.x, -direcao.y);
            gm.pontos++;

        }

        else if(col.gameObject.CompareTag("Parede")){
                direcao = new Vector3(-direcao.x, direcao.y);
        }

    }
}
