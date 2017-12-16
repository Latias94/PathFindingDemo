using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GBFSTest : MonoBehaviour
{
	public GameObject group;
	public int startRow = 0;
	public int startCol = 0;
	public int endRow = 0;
	public int endCol = 4;

	// Use this for initialization
	void Start()
	{
		int[, ] map = new int[5, 5]
		{
			{ 0, 0, 0, 1, 0 }, 
			{ 0, 0, 0, 1, 0 }, 
			{ 0, 0, 0, 1, 0 }, 
			{ 0, 1, 1, 1, 0 }, 
			{ 0, 0, 0, 0, 0 }
		};
		Graph graph = new Graph(map);
		GreedBestFirstSearch bfs = new GreedBestFirstSearch(graph);
		if (map[startRow, startCol] == 1)
		{
			Debug.Log("Error: 起点是墙！是墙！墙！");
			return;
		}

		if (map[endRow, endCol] == 1)
		{
			Debug.Log("Error: 终点是墙！是墙！墙！");
			return;
		}

		if (startRow > map.GetLength(0) || startCol > map.GetLength(1) || endRow > map.GetLength(0) || endCol > map.GetLength(1))
		{
			Debug.Log("Error: 非法的索引，请检查起点和终点的行列值！");
			return;
		}
		bfs.Start(graph.nodes[GetIndex(startRow, startCol, map)], graph.nodes[GetIndex(endRow, endCol, map)]);
		// 路径
		Stack<Node> path = bfs.Finding();
		if (path == null)
		{
			Debug.Log("Some error");
		}
		else
		{
			Debug.Log("Search done. Path length " + path.Count);
		}
		ResetGroup(graph);
		while (path.Count != 0)
		{
			Node node = path.Pop();
			GetImage(GetIndex(node.row, node.col, map)).color = Color.red;
		}
	}

	// 根据节点的所在行列信息，计算出其索引
	int GetIndex(int row, int col, int[, ] map)
	{
		int cols = map.GetLength(1);
		return row * cols + col;
	}

	// 根据节点的索引得到对应位置的图片
	Image GetImage(int index)
	{
		GameObject go = group.transform.GetChild(index).gameObject;
		return go.GetComponent<Image>();
	}

	void ResetGroup(Graph graph)
	{
		for (int i = 0; i < graph.nodes.Length; i++)
		{
			GetImage(i).color = graph.nodes[i].adjecent.Count == 0 ? Color.gray : Color.white;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}