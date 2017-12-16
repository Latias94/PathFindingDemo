using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 节点类
/// </summary>
public class Node
{
	/// <summary>
	/// 上一个节点
	/// </summary>
	public Node parent;
	/// <summary>
	/// 节点的 h(x) 值
	/// </summary>
	public float h;
	/// <summary>
	/// 与当前节点相邻的节点
	/// </summary>
	public List<Node> adjecent = new List<Node>();
	/// <summary>
	/// 节点所在的行
	/// </summary>
	public int row;
	/// <summary>
	/// 节点所在的列
	/// </summary>
	public int col;

	/// <summary>
	/// 清除节点信息
	/// </summary>
	public void Clear()
	{
		parent = null;
		h = 0.0f;
	}
}