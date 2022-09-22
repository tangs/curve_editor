using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CurveExport
{
    [Serializable]
    class Point
    {
        public float x;
        public float y;

        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [Serializable]
    class FishLine
    {
        public string name;
        public float baseSpeed;
        public List<Point> Points = new();
        public List<float> distances = new();
    }

    [Serializable]
    class FishLines
    {
        public List<FishLine> fishLines = new();
    }

    private static void CreateIfDirectoryNotExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    [MenuItem("Curve/Export")]
    public static void ExportAllCurve()
    {
        var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity");
        var objs = scene.GetRootGameObjects();
        var lineInfos = Resources.FindObjectsOfTypeAll<LineInfo>();
        var infos = new FishLines();
        var lineNames = new HashSet<string>();
        foreach (var info in lineInfos)
        {
            if (lineNames.Contains(info.lineName))
            {
                Debug.LogError($"line name repetition:{info.lineName}");
                return;
            }
            lineNames.Add(info.lineName);
            var path = info.GetComponent<EasySplinePath2D>();
            path.SetUp();
            var points = path.points;
            var distances = path.distances;

            var fishLine = new FishLine();
            fishLine.name = info.lineName;
            fishLine.baseSpeed = info.speed;
            foreach (var distance in distances)
            {
                fishLine.distances.Add(distance * 100.0f);
            }
            foreach (var point in points)
            {
                fishLine.Points.Add(new Point(point.x * 100.0f, point.y * 100.0f));
            }
            infos.fishLines.Add(fishLine);
        }
        string json = JsonUtility.ToJson(infos, true);
        string folder = $"{Application.dataPath}/../Output";
        string filepath = $"{folder}/fishLines.json.txt";
        CreateIfDirectoryNotExists(folder);
        StreamWriter writer = new StreamWriter(filepath, false);
        writer.Write(json);
        writer.Close();
        Debug.Log("export fish line finish.");
    }
}
