using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
	public GameObject cellPrefab;
	public GameObject playerPrefab;

	public int sizeX = 10;
	public int sizeY = 15;
	public float width = 1.0f;
	public float height = 1.0f;

	public struct LevelCoord
	{
		public int x;
		public int y;

		public void Set(int a_x, int a_y)
		{
			x = a_x;
			y = a_y;
		}
	};

	GameObject player;
	LevelCoord playerPosition;

	Cell[] cells;

	void Start()
	{
		// create root node for adding all renderable gameobjects to
		GameObject root = new GameObject();
		root.transform.position = transform.position;
		root.transform.parent = transform;
        root.name = "root";

		// add all parts to the level
		GenerateCells();
		StartPlayer();

		// set the root transform to make sure it is scaled to the correct width and height
		SetTransform();
	}

	void StartPlayer()
	{
		Transform root = transform.Find("root");
		Vector3 pos = transform.position + new Vector3(0.5f, 0.5f, 0.0f);
		player = Object.Instantiate(playerPrefab, pos, Quaternion.identity) as GameObject;
		playerPosition = new LevelCoord();
		playerPosition.x = 0;
		playerPosition.y = 0;
		player.transform.parent = root.transform;
	}

	void GenerateCells()
	{
		cells = new Cell[sizeX * sizeY];
		
		Transform root = transform.Find("root");
		Vector3 offset = transform.position + new Vector3(0.5f, 0.5f, 0.0f);

		for (int y = 0; y < sizeY; ++y)
		{
			for (int x = 0; x < sizeX; ++x)
			{
				int i = y * sizeX + x;
				Vector3 pos = new Vector3(x, y, 0.0f) + offset;
				GameObject go = Object.Instantiate(cellPrefab, pos, Quaternion.identity) as GameObject;
                go.transform.parent = root.transform;
                cells[i] = go.AddComponent<Cell>();
            }
		}
	}

	void SetTransform()
	{
		Transform root = transform.Find("root");
		Vector3 scale = new Vector3(width / sizeX, height / sizeY, 1.0f);
		root.transform.localScale = scale;
		root.transform.rotation = transform.rotation;
		root.transform.Translate(new Vector3(width * -0.5f, height * -0.5f, 0.0f));
	}

	public bool TryMove(int xDir, int yDir)
	{
		xDir = Mathf.Clamp(xDir, -1, 1);
		yDir = Mathf.Clamp(yDir, -1, 1);
		return TryMoveTo(playerPosition.x + xDir, playerPosition.y + yDir);
	}

	bool TryMoveTo(int x, int y)
	{
		if (x >= 0 && x < sizeX && y >= 0 && y < sizeY)
		{
			int i = y * sizeX + x;
			if (!cells[i].solid)
			{
				//player.transform.Translate(new Vector3(x - playerPosition.x, y - playerPosition.y, 0.0f));
				player.transform.localPosition += new Vector3(x - playerPosition.x, y - playerPosition.y, 0.0f);
				playerPosition.Set(x, y);
				UncoverCell(x, y);
				return true;
			}
		}

		return false;
	}

	void UncoverCell(int x, int y)
	{
		int minX = (x > 0) ? x - 1 : 0;
		int maxX = (x < sizeX - 1) ? x + 1 : sizeX;
		int minY = (y > 0) ? y - 1 : 0;
		int maxY = (y < sizeY - 1) ? y + 1 : sizeY;

		for (y = minY; y < maxY; ++y)
		{
			for (x = minX; x < maxX; ++x)
			{
				int i = y * sizeX + x;
				cells[i].SetVisible(true);
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.matrix = transform.localToWorldMatrix;

		Gizmos.DrawWireCube(Vector3.zero, new Vector3(width, height, 0.0f));
	}
}
