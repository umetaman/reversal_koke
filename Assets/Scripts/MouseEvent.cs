using System.Collections;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    // 位置座標
    private Vector3 position;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPositionA, screenToWorldPointPositionB;

    private Vector3 start, present;
    private shake_koke shake;
    public SimpleMeshExploder SME;


    void Start()
    {
        shake = GetComponent<shake_koke>();

        //shake.shake(new Vector3(0,0,0), new Vector3(0,0,1));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
                // Vector3でマウス位置座標を取得する
               position = Input.mousePosition;
            //    // Z軸修正
                position.z = 10f;
            //    // マウス位置座標をスクリーン座標からワールド座標に変換する
                screenToWorldPointPositionA = Camera.main.ScreenToWorldPoint(position);
            //    // ワールド座標に変換されたマウス座標を代入
                start = screenToWorldPointPositionA;
            SME.Explode();

            }

            else if (Input.GetMouseButtonUp(0))
            {
            //    // Vector3でマウス位置座標を取得する
                position = Input.mousePosition;
            //    // Z軸修正
                position.z = 10f;
            //    // マウス位置座標をスクリーン座標からワールド座標に変換する
                screenToWorldPointPositionB = Camera.main.ScreenToWorldPoint(position);
            //    // ワールド座標に変換されたマウス座標を代入
                present = screenToWorldPointPositionB;
            var direction = present - start;
            shake.shake(start,direction);
        }
        /*
        float x = Random.Range(-40f,40f);
        float y = Random.Range(-40f, 40f);
        float z = Random.Range(-5f, 5f);
        float x1 = Random.Range(-40f, 40f);
        float y1 = Random.Range(-40f, 40f);
        float z1 = Random.Range(-5f, 5f);
        var position_ = new Vector3(x,y,z);
        var direction = new Vector3(x1,y1,z1);

        shake.shake(position_,direction);*/
    }
}