using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 图类
/// </summary>
public class Graph
{
	public int rows = 0;
	public int cols = 0;
	public Node[] nodes;

	public Graph(int[, ] grid)
	{
		rows = grid.GetLength(0);
		cols = grid.GetLength(1);

		nodes = new Node[grid.Length];
		for (int i = 0; i < nodes.Length; i++)
		{
			Node node = new Node();
			node.row = i / cols;
			node.col = i - (node.row * cols);
			nodes[i] = node;
		}

		// 找到每一个节点的相邻节点
		foreach (Node node in nodes)
		{
			int row = node.row;
			int col = node.col;
			// 墙，即节点不能通过的格子 
			// 1 为墙，0 为可通过的格子
			if (grid[row, col] != 1)
			{
				// 上方的节点
				if (row > 0 && grid[row - 1, col] != 1)
				{
					node.adjecent.Add(nodes[cols * (row - 1) + col]);
				}
				// 右边的节点
				if (col < cols - 1 && grid[row, col + 1] != 1)
				{
					node.adjecent.Add(nodes[cols * row + col + 1]);
				}

				// 下方的节点
				if (row < rows - 1 && grid[row + 1, col] != 1)
				{
					node.adjecent.Add(nodes[cols * (row + 1) + col]);
				}

				// 左边的节点
				if (col > 0 && grid[row, col - 1] != 1)
				{
					node.adjecent.Add(nodes[cols * row + col - 1]);
				}
			}
		}
	}
}