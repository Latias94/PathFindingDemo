using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 贪婪最佳优先算法
/// </summary>
public class GreedBestFirstSearch
{
	/// <summary>
	/// 开放集合
	/// </summary>
	public List<Node> reachable;
	/// <summary>
	/// 封闭集合，存放已经被算法估值的节点
	/// </summary>
	public List<Node> explored;
	/// <summary>
	/// 终点节点
	/// </summary>
	public Node destination;
	public Graph graph;

	public GreedBestFirstSearch(Graph graph)
	{
		this.graph = graph;
	}

	// 设置起点、终点
	public void Start(Node start, Node destination)
	{
		this.destination = destination;
		reachable = new List<Node>();
		reachable.Add(start);
		explored = new List<Node>();
		// 清除节点信息
		for (int i = 0; i < graph.nodes.Length; i++)
		{
			graph.nodes[i].Clear();
		}
	}

	/// <summary>
	/// 开始查找
	/// </summary>
	public Stack<Node> Finding()
	{
		// 存放查找路径的栈
		Stack<Node> path;
		Node currentNode = reachable[0];
		// 迭代查找，直至找到终点节点
		while (currentNode != destination)
		{
			Debug.Log("node row: " + currentNode.row + " col: " + currentNode.col);
			explored.Add(currentNode);
			reachable.Remove(currentNode);
			// 将当前节点的相邻节点加入开放集合
			AddAjacent(currentNode);
			// 查找了相邻节点后依然没有可以考虑的节点，查找失败。
			if (reachable.Count == 0)
			{
				return null;
			}
			// 将开放集合中h值最小的节点当做当前节点
			currentNode = FindLowestH();
		}
		Debug.Log("node row: " + currentNode.row + " col: " + currentNode.col);
		// 查找成功，则根据节点parent找到查找到的路径
		path = new Stack<Node>();
		Node node = destination;
		// 先将终点压入栈，再迭代地把node的前一个节点压入栈
		path.Push(node);
		while (node.parent != null)
		{
			path.Push(node.parent);
			node = node.parent;
		}
		return path;
	}

	/// <summary>
	/// 把节点的相邻节点加入到开放集合
	/// </summary>
	public void AddAjacent(Node node)
	{
		foreach (Node adjecent in node.adjecent)
		{
			if (explored.Contains(adjecent) || reachable.Contains(adjecent))
			{
				continue;
			}
			adjecent.parent = node;
			// 曼哈顿算法计算启发式
			adjecent.h = H(adjecent, destination);
			reachable.Add(adjecent);
		}
	}

	/// <summary>
	/// 曼哈顿算法
	/// </summary>
	/// <param name="start">起点</param>
	/// <param name="end">终点</param>
	/// <returns>节点的h值</returns>
	public float H(Node start, Node end)
	{
		return Mathf.Abs(start.row - end.row) + Mathf.Abs(start.col - end.col);
	}

	/// <summary>
	/// 列表中找到h值最小的节点
	/// </summary>
	/// <returns>h值最小的节点</returns>
	public Node FindLowestH()
	{
		float min = graph.cols + graph.rows;
		Node minNode = null;
		for (int i = 0; i < reachable.Count; i++)
		{
			if (reachable[i].h < min)
			{
				minNode = reachable[i];
				min = reachable[i].h;
			}
		}
		// Debug.Log("---" + minNode.row + " col " + minNode.col);
		return minNode;
	}
}