using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class competences : MonoBehaviour
{
    public bool isRotate = false;
    public bool inCooldown = false;
    public bool inBigCooldown = false;
    public bool inCooldownTA = false;
    public int torotate = 0;
    public GameObject joueur;
    public GameObject bdf;
    public int etape;
    // Start is called before the first frame update
    void Start()
    {
        bdf.SetActive(false);
    }

    private void Update()
    {
        if (isRotate)
        {
            torotate--;
            if (torotate <= 0)
            {
                if (etape == 2)
                {
                    bdf.SetActive(false);
                    etape = 0;
                    inBigCooldown = false;
                    torotate = 2;
                    return;
                }
                isRotate = false;
                bdf.gameObject.SetActive(false);
                etape++;
            } else if (etape == 0)
            {
                bdf.transform.RotateAround(joueur.transform.position,new Vector3(0,1,0),300*Time.deltaTime);
            }else if (etape == 1)
            {
                bdf.transform.RotateAround(joueur.transform.position,new Vector3(0,1,0),-300*Time.deltaTime);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inCooldown) return;
        if (Input.GetKeyDown(KeyCode.A)&& !inCooldown)
        {
            StartCoroutine(Cooldown(1,0.5f));
            torotate = 40;
            isRotate = true;
            if (etape == 0)
            {
                bdf.transform.localScale = new Vector3( 1, 1, 1 );
                StartCoroutine(Cooldown(2,5f));
                bdf.transform.position = new Vector3(joueur.transform.position.x, joueur.transform.position.y,joueur.transform.position.z + 3);
                bdf.gameObject.SetActive(true);
            }else if (etape == 1 )
            {
                bdf.transform.position = new Vector3(joueur.transform.position.x, joueur.transform.position.y,joueur.transform.position.z - 3);
                bdf.gameObject.SetActive(true);
            }
            else if (etape == 2)
            {
                bdf.transform.localScale = new Vector3( 2, 2, 2 );
                bdf.transform.position = new Vector3(joueur.transform.position.x + 3, joueur.transform.position.y,joueur.transform.position.z);
                bdf.gameObject.SetActive(true);
                StartCoroutine(Cooldown(3,1f));

            }
        }
    }

    IEnumerator Cooldown(int choix ,float cd)
    {
        switch (choix)
        {
            case 1:
                if(inCooldown) yield break;
                inCooldown = true;
                yield return new WaitForSeconds(cd);
                inCooldown = false;
                break;
            case 2:
                if(inBigCooldown) yield break;
                inBigCooldown = false;
                yield return new WaitForSeconds(cd);
                inBigCooldown = false;
                etape = 0;
                break;
            case 3:
                if(inCooldownTA) yield break;
                inCooldownTA = true;
                yield return new WaitForSeconds(cd);
                inCooldownTA = false;
                etape = 0;
                break;
        }
    }
}