using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speedRatio = 1.0f;
    public LineInfo line;

    //private EasySplinePath2D path;

    private float cur = 0.0f;
    private bool pause = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    void UpdatePosition()
    {
        var path = line.GetComponent<EasySplinePath2D>();
        var pos = path.GetPointByDistance(cur, true, Space.Self);
        this.gameObject.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            cur += Time.deltaTime * line.speed * speedRatio / 100.0f;
        }
        UpdatePosition();
    }

    public void Reset()
    {
        cur = 0.0f;
    }

    public void SwitchPauseState()
    {
        pause = !pause;
    }
}
