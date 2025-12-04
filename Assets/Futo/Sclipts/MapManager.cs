using UnityEngine;
using System.Collections.Generic;

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

            for (int i = 0; i < floor.NodeDatas.Length; i++)
            {
                StageNodeData data = floor.NodeDatas[i];

                StageNode node = new StageNode();
                node.StageNodeType = data.Type;
                node.StageID = data.StageID;
                
                GameObject uiObj = Instantiate(nodePrefab, mapParent);
                StageNodeUI ui = uiObj.GetComponent<StageNodeUI>();
                ui.StageNodeData = node;
                node.UI = ui;

                // 【本家配置ロジック】をここにそのまま使う
                float baseX = (i - (floor.NodeDatas.Length - 1) / 2f)
                              * (horizontalSpread / floor.NodeDatas.Length);
                float rand = Random.Range(-40f, 40f);
                float x = baseX + rand;

                ui.rect.anchoredPosition = new Vector2(x, -y * layerHeight);

                layerNodes.Add(node);
            }

            // 密集していたら押し広げ
            RelaxNodes(layerNodes, 80f);

            map.Add(layerNodes);
        }

        // 接続（ランダム or データ指定も可能）
        ConnectFloors();

        // スタート地点（複数あれば選択できる）
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

    void ConnectFloors()
    {
        for (int y = 0; y < map.Count - 1; y++)
        {
            List<StageNode> current = map[y];
            List<StageNode> next = map[y + 1];

            foreach (StageNode n in current)
            {
                // 次の階層から 1～2個をランダムで接続
                int links = Random.Range(1, Mathf.Min(3, next.Count + 1));

                for (int i = 0; i < links; i++)
                {
                    StageNode target = next[Random.Range(0, next.Count)];
                    n.NextStageNodes.Add(target);

                    // 線を引く
                    DrawLine(
                        n.UI.rect,
                        target.UI.rect
                    );
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
