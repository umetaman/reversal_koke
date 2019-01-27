using UnityEngine;
using System.Collections.Generic;

// メモ帳 | UnityでKinect v2を利用する
// https://blog.sky-net.pw/article/37

public class KinectSample : MonoBehaviour
{
    Windows.Kinect.KinectSensor _Sensor;
    Windows.Kinect.BodyFrameReader _Reader;
    Windows.Kinect.Body[] _Data = null;

    List<Transform> tt = new List<Transform>();

    void Start()
    {
        _Sensor = Windows.Kinect.KinectSensor.GetDefault();
        if (_Sensor != null)
        {
            _Reader = _Sensor.BodyFrameSource.OpenReader();
            if (!_Sensor.IsOpen)
            {
                _Sensor.Open();
            }
        }
    }

    static Vector3 GetVector3FromJoint(Windows.Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X, joint.Position.Y, joint.Position.Z);
    }

    void RefreshBodyObject(Windows.Kinect.Body body)
    {
        bool isGen = false;
        if (tt.Count - 1 != (int)Windows.Kinect.JointType.ThumbRight)
        {
            tt.Clear();
            isGen = true;
            print("ボーンを生成します.");
        }

        for (Windows.Kinect.JointType jt = Windows.Kinect.JointType.SpineBase; jt <= Windows.Kinect.JointType.ThumbRight; jt++)
        {
            Windows.Kinect.Joint sourceJoint = body.Joints[jt];
            Vector3 tmp = GetVector3FromJoint(sourceJoint) * 20;
            if (isGen)
            {
                var t = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
                t.name = jt.ToString();
                t.position = tmp;
                t.parent = transform;
                Destroy(t.GetComponent<BoxCollider>());
                tt.Add(t);
            }
            else
            {
                Transform t = tt[(int)jt];
                if (sourceJoint.TrackingState == Windows.Kinect.TrackingState.Tracked)
                {
                    t.position = tmp;
                    // t.gameObject.SetActive(true);
                }
                else
                {
                    // t.gameObject.SetActive(false);
                }
            }
        }
    }

    void Update()
    {
        var frame = _Reader.AcquireLatestFrame();
        if (frame == null)
        {
            return;
        }
        if (_Data == null)
        {
            _Data = new Windows.Kinect.Body[_Sensor.BodyFrameSource.BodyCount];
        }
        frame.GetAndRefreshBodyData(_Data);
        frame.Dispose();
        frame = null;

        if (_Data == null)
        {
            return;
        }

        // ここのデータには何が入っているかよくわからない。1人しか映っていなくても、配列の長さは6だった。多分数フレーム分のデータが入っている？
        foreach (var body in _Data)
        {
            if (body == null)
            {
                continue;
            }
            if (body.IsTracked)
            {
                RefreshBodyObject(body);
                break; // とりあえず、一回でもRefreshBodyObjectが呼ばれたらbreak;
            }
        }
    }

    void OnApplicationQuit()
    {
        if (_Reader != null)
        {
            _Reader.Dispose();
            _Reader = null;
        }
        if (_Sensor != null)
        {
            if (_Sensor.IsOpen)
            {
                _Sensor.Close();
            }
            _Sensor = null;
        }
    }
}
