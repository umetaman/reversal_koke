using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake_koke : MonoBehaviour
{
    private GameObject par, koke_particle;
    //パーティクルのメインにアクセスするための型
    private ParticleSystem.MainModule P_M;


    // Start is called before the first frame update
    void Start()
    {
        //par = Resources.Load("koke_particle") as GameObject;
        par = Resources.Load("koke_particle_test") as GameObject;
        P_M = par.GetComponent<ParticleSystem>().main;

    }

    // Update is called once per frame
    void Update()
    {

    }
    //場所と方向(方向ベクトル)を引数に
    public void shake(Vector3 _position, Vector3 _direction)
    {

        var speed = _direction.magnitude;
        P_M.startSpeed = speed;
        koke_particle = Instantiate(par, _position, Quaternion.identity);
        koke_particle.transform.forward = _direction;

        //koke_particle.GetComponent<>.();
    }
}
