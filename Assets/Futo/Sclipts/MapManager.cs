using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    [Header("Prefabs")]
    public GameObject nodePrefab;
    public RectTransform mapParent;

    [Header("Map Settings")]
    public int layers = 8;
    public int minNodes = 1;
    public int maxNodes = 4;
    public float layerHeight = 150f;
    public float horizontalSpread = 200f;

    [Header("Map Data")]
    public MapData mapData;

    private List<List<StageNode>> map = new List<List<StageNode>>();

    private StageNode currentNode;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        map.Clear();

        for (int y = 0; y < mapData.FloorDatas.Length; y++)
        {
            FloorData floor = mapData.FloorDatas[y];
            List<StageNode> layerNodes = new List<StageNode>();

            int nodeCount = Random.Range(floor.MinNodes, floor.MaxNodes + 1);

            List<StageNodeData> chosen = new List<StageNodeData>();

            List<StageNodeData> candidates = new List<StageNodeData>(floor.NodeDatas);

            for (int i = 0; i < nodeCount; i++)
            {
                if (candidates.Count == 0)
                    break;

                int r = Random.Range(0, candidates.Count);
                chosen.Add(candidates[r]);
                candidates.RemoveAt(r);
            }

            for (int i = 0; i < chosen.Count; i++)
            {
                StageNodeData data = chosen[i];

                StageNode node = new StageNode();
                node.StageNodeType = data.Type;
                node.StageID = data.StageID;

                GameObject uiObj = Instantiate(nodePrefab, mapParent);
                StageNodeUI ui = uiObj.GetComponent<StageNodeUI>();
                ui.StageNodeData = node;
                node.UI = ui;

                float baseX = (i - (chosen.Count - 1) / 2f)
                              * (horizontalSpread / chosen.Count);
                float rand = Random.Range(-40f, 40f);
                float x = baseX + rand;

                ui.rect.anchoredPosition = new Vector2(x, -y * layerHeight);

                layerNodes.Add(node);
            }

            RelaxNodes(layerNodes, 80f);

            map.Add(layerNodes);
        }

        ConnectFloors();

        StartNodeSelectUI(map[0]);
    }


    void RelaxNodes(List<StageNode> layer, float minDist)
    {
        bool changed = true;
        int loop = 0;

        while (changed && loop < 10)
        {
            changed = false;
            loop++;

            for (int i = 0; i < layer.Count; i++)
            {
                for (int j = i + 1; j < layer.Count; j++)
                {
                    RectTransform a = layer[i].UI.rect;
                    RectTransform b = layer[j].UI.rect;

                    float dist = Mathf.Abs(a.anchoredPosition.x - b.anchoredPosition.x);

                    if (dist < minDist)
                    {
                        float push = (minDist - dist) / 2f;

                        if (a.anchoredPosition.x < b.anchoredPosition.x)
                        {
                            a.anchoredPosition -= new Vector2(push, 0);
                            b.anchoredPosition += new Vector2(push, 0);
                        }
                        else
                        {
                            a.anchoredPosition += new Vector2(push, 0);
                            b.anchoredPosition -= new Vector2(push, 0);
                        }

                        changed = true;
                    }
                }
            }
        }
    }

    void DrawLine(RectTransform a, RectTransform b)
    {
        GameObject lineObj = new GameObject("Line");
        lineObj.transform.SetParent(mapParent);

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.startWidth = 5f;
        lr.endWidth = 5f;

        lr.useWorldSpace = true;

        lr.SetPosition(0, a.position);
        lr.SetPosition(1, b.position);
    }

    void DrawUILine(RectTransform a, RectTransform b)
    {
        GameObject line = new GameObject("UILine", typeof(RectTransform));
        line.transform.SetParent(mapParent);

        RectTransform rt = line.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
        rt.pivot = new Vector2(0.5f, 0.5f);

        Vector2 posA = a.anchoredPosition;
        Vector2 posB = b.anchoredPosition;

        Vector2 dir = (posB - posA);
        float distance = dir.magnitude;

        rt.sizeDelta = new Vector2(distance, 6f);
        rt.anchoredPosition = posA + dir / 2f;
        rt.rotation = Quaternion.FromToRotation(Vector3.right, dir);

        Image img = line.AddComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0.5f); 
    }

    void ConnectFloors()
    {
        for (int y = 0; y < map.Count - 1; y++)
        {
            List<StageNode> current = map[y];
            List<StageNode> next = map[y + 1];

            foreach (StageNode n in current)
            {
                int links = Random.Range(1, Mathf.Min(3, next.Count + 1));

                for (int i = 0; i < links; i++)
                {
                    StageNode target = next[Random.Range(0, next.Count)];
                    n.NextStageNodes.Add(target);

                    DrawUILine(n.UI.rect, target.UI.rect);
                }
            }

            foreach (StageNode upper in next)
            {
                bool connected = false;

                // 誰かに nextNodes として登録されているか？
                foreach (StageNode lower in current)
                {
                    if (lower.NextStageNodes.Contains(upper))
                    {
                        connected = true;
                        break;
                    }
                }

                // 誰にもつながっていなかったら強制接続
                if (!connected)
                {
                    StageNode lower = current[Random.Range(0, current.Count)];
                    lower.NextStageNodes.Add(upper);

                    DrawUILine(lower.UI.rect, upper.UI.rect);
                }
            }
        }
    }

    void StartNodeSelectUI(List<StageNode> startNodes)
    {
        Debug.Log("スタート地点を選んでください：");

        // 単純に「最初のノードをスタート」とする例
        // → UIで選択する版は後で作る
        currentNode = startNodes[0];

        // TODO: 本当はここで UI を出して選ばせる
    }

    public void OnNodeClicked(StageNode clicked)
    {
        if (currentNode.NextStageNodes.Contains(clicked))
        {
            Debug.Log("ノード移動！");
            currentNode = clicked;
        }
        else
        {
            Debug.Log("そのノードへは行けません！");
        }
    }
}
