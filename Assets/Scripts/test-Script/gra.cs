/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gra : MonoBehaviour
{
    // 位置座標
    private Vector3 position;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;


    public GameObject cam;
    public Vector3 rot;
    public float gra_rotation;
    [SerializeField] GameObject target1;
    [SerializeField] GameObject target2;
    [SerializeField] GameObject target3;

    private int des;

    public float speed;
    public Rigidbody rb;
    public Rigidbody rb2;
    public int count, countInpar;

    private float noise;
    private float t1, t2, t3, ty1, ty2, ty3;
    private float add, add1, add2, add3;
    private float r, g, b;

    ParticleSystem.EmissionModule mEmObj;
    ParticleSystem.MainModule par;
    ParticleSystem ParticleObj, particleIn;

    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find("MainCamera");
        target1 = GameObject.Find("target_1");
        target2 = GameObject.Find("target_2");
        target3 = GameObject.Find("target_3");

        GameObject king = GameObject.Find("king");
        //des = king.GetComponent<king>().des;

        Physics.gravity = new Vector3(0f, -50, 0f);

        speed = 3f;
        rb = GetComponent<Rigidbody>();
        add = 10;
        t1 = Random.Range(-30, 100);
        t2 = Random.Range(-30, 100);
        t3 = Random.Range(-30, 100);
        ty1 = Random.Range(-100, -30);
        ty2 = Random.Range(-100, -30);
        ty3 = Random.Range(-100, -30);
        add1 = Random.Range(0.05f, 0.1f);
        add2 = Random.Range(0.05f, 0.1f);
        add3 = Random.Range(0.05f, 0.1f);

        if (this.transform.name == "ball(Clone)")
        {
            ParticleObj = transform.Find("par").GetComponent<ParticleSystem>();
            mEmObj = ParticleObj.emission;
            par = transform.Find("par").GetComponent<ParticleSystem>().main;
            // par.startColor = Color.red;
            r = Random.Range(50f, 255f);

        }
        else if (this.transform.name == "inParticle(Clone)")
        {
            particleIn = transform.Find("Particle System").GetComponent<ParticleSystem>();
        }




    }

    // Update is called once per frame
    void Update()
    {




        float rot_c = cam.transform.rotation.z;

        gra_rotation = rot_c * -3000000000;

        if (Input.GetKeyDown("return"))
        {
            Physics.gravity = new Vector3(0f, -9.8f, 0f);
        }
        //落ちたら消える
        if (this.transform.position.y < -800)
        {
            Destroy(this.gameObject);

        }

        if (this.transform.name == "target_1")
        {
            add += add1;
            //Rigidbody rb_t = GetComponent<Rigidbody>();
            transform.position = new Vector3(t1 + (200 * Mathf.Cos(add / 10) - 100), ty1 + 10 * Mathf.Sin(add), t1 * Mathf.Sin(add / 10));
            // rb_t.AddForce(add,0,0, ForceMode.Force);
            //Debug.Log(add);
        }
        else if (this.transform.name == "target_2")
        {
            add += add2;
            transform.position = new Vector3(t2 + (200 * Mathf.Cos(add / 10) - 100), ty2 + 10 * Mathf.Sin(add), t2 * Mathf.Sin(add / 10));
            //Debug.Log(Mathf.Sin(add / 100));
        }
        else if (this.transform.name == "target_3")
        {
            add += add3;
            transform.position = new Vector3(t3 + (200 * Mathf.Cos(add / 10) - 100), ty3 + 10 * Mathf.Sin(add), t3 * Mathf.Sin(add / 10));
        }


        if (this.transform.name == "ball(Clone)")
        {
            count++;

            var aim_t_1 = target1.transform.position - this.transform.position;
            aim_t_1.Normalize();
            var aim_t_2 = target2.transform.position - this.transform.position;
            aim_t_2.Normalize();
            var aim_t_3 = target3.transform.position - this.transform.position;
            aim_t_3.Normalize();

            int rnd = Random.Range(0, 2);
            float F = Random.Range(100f, 200f);
            if (rnd == 0) { rb.AddForce(F * aim_t_1, ForceMode.Acceleration); }
            else if (rnd == 1) { rb.AddForce(F * aim_t_2, ForceMode.Acceleration); }
            else if (rnd == 2) { rb.AddForce(F * aim_t_3, ForceMode.Acceleration); }


            if (count <= 500)
            {
                if (count < 100)
                {
                    par.startColor = new Color(Random.Range(0f, 100f), Random.Range(0f, 100f), 0.8f, 1.0f);
                }

                else if (count >= 100 && count < 300)
                {

                    par.startColor = new Color(Random.Range(100f, 150f), Random.Range(100f, 150f), 0.5f, 1.0f);
                }

                else if (count >= 400 && count < 450)
                {
                    ParticleObj.Stop();
                    par.startColor = new Color(Random.Range(150f, 200f), Random.Range(150f, 200f), 0.3f, 1.0f);

                }
                else if (count == 500) { Destroy(this.gameObject); }

                else
                {

                    par.startColor = new Color(Random.Range(200f, 255f), Random.Range(200f, 255f), 0.1f, 1.0f);
                }
            }

        }
        if (this.transform.name == "inParticle(Clone)")
        {
            countInpar++;
            if (countInpar == 100)
            {
                particleIn.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }

            else if (countInpar == 300)
            {
                Destroy(this.gameObject);
            }

        }

    }
    //collisionがぶつかった相手的な感じ
    private void OnTriggerExit(Collider collision)
    {
        // 物体がトリガーと離れたとき、１度だけ呼ばれる

        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "wall")
        {
            Destroy(this.gameObject);
            GameObject prefab = (GameObject)Resources.Load("fall-obj/ball");
            GameObject pre_particle = (GameObject)Resources.Load("particle/inParticle");

            GameObject obj = Instantiate(prefab, new Vector3(x: this.transform.position.x, y: this.transform.position.y, z: this.transform.position.z), Quaternion.identity);
            GameObject one_particle = Instantiate(pre_particle, new Vector3(x: this.transform.position.x, y: this.transform.position.y, z: this.transform.position.z), Quaternion.identity);
            Destroy(transform.root.gameObject);

        }
    }
}*/

