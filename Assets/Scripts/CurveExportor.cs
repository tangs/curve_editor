using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveExportor : MonoBehaviour
{
    //public BezierSpline spline;
    public EasySplinePath2D path;

    // Start is called before the first frame update
    void Start()
    {
        var len = 100;
        var points = new Vector2[len];
        for (int i = 0; i < len; i++)
        {
            points[i] = path.GetPointByPercent(i / 100.0f);
        }
        int a = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
