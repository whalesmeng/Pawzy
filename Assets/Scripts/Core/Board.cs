using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PawzyPop.Core
{
    public class Board : MonoBehaviour
    {
        public static Board Instance { get; private set; }

        [Header("Board Settings")]
        [SerializeField] private int width = 6;
        [SerializeField] private int height = 6;
        [SerializeField] private float tileSize = 1f;
        [SerializeField] private float tileSpacing = 0.1f;

        [Header("References")]
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Transform tilesParent;
        [SerializeField] private TileType[] tileTypes;

        public int Width => width;
        public int Height => height;
        public Tile[,] Tiles { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            Tiles = new Tile[width, height];
            GenerateBoard();
            CenterBoard();
        }

        private void GenerateBoard()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    CreateTile(x, y);
                }
            }

            // 确保初始棋盘没有匹配
            while (MatchFinder.Instance != null && MatchFinder.Instance.FindAllMatches().Count > 0)
            {
                ClearAndRegenerate();
            }
        }

        private void CreateTile(int x, int y)
        {
            Vector3 position = GetWorldPosition(x, y);
            GameObject tileObj = Instantiate(tilePrefab, position, Quaternion.identity, tilesParent);
            tileObj.name = $"Tile_{x}_{y}";

            Tile tile = tileObj.GetComponent<Tile>();
            TileType randomType = GetRandomTileType();
            tile.Initialize(x, y, randomType);
            Tiles[x, y] = tile;
        }

        private TileType GetRandomTileType()
        {
            if (tileTypes == null || tileTypes.Length == 0)
            {
                Debug.LogError("No tile types configured!");
                return null;
            }
            return tileTypes[Random.Range(0, tileTypes.Length)];
        }

        public TileType GetRandomTileTypeExcluding(TileType exclude)
        {
            if (tileTypes.Length <= 1) return tileTypes[0];
            
            TileType newType;
            do
            {
                newType = tileTypes[Random.Range(0, tileTypes.Length)];
            } while (newType == exclude);
            
            return newType;
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            float totalSize = tileSize + tileSpacing;
            return new Vector3(x * totalSize, y * totalSize, 0);
        }

        private void CenterBoard()
        {
            float totalSize = tileSize + tileSpacing;
            float offsetX = (width - 1) * totalSize / 2f;
            float offsetY = (height - 1) * totalSize / 2f;
            tilesParent.position = new Vector3(-offsetX, -offsetY, 0);
        }

        private void ClearAndRegenerate()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tiles[x, y].SetType(GetRandomTileType());
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
                return null;
            return Tiles[x, y];
        }

        public void SwapTiles(Tile tileA, Tile tileB)
        {
            int tempX = tileA.X;
            int tempY = tileA.Y;

            Tiles[tileA.X, tileA.Y] = tileB;
            Tiles[tileB.X, tileB.Y] = tileA;

            tileA.SetPosition(tileB.X, tileB.Y);
            tileB.SetPosition(tempX, tempY);
        }

        public bool AreAdjacent(Tile tileA, Tile tileB)
        {
            int dx = Mathf.Abs(tileA.X - tileB.X);
            int dy = Mathf.Abs(tileA.Y - tileB.Y);
            return (dx == 1 && dy == 0) || (dx == 0 && dy == 1);
        }

        public IEnumerator RefillBoard()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (Tiles[x, y] == null || Tiles[x, y].IsEmpty)
                    {
                        if (Tiles[x, y] == null)
                        {
                            CreateTile(x, y);
                        }
                        else
                        {
                            Tiles[x, y].SetType(GetRandomTileType());
                        }
                        Tiles[x, y].PlaySpawnAnimation();
                    }
                }
            }
            yield return new WaitForSeconds(0.2f);
        }

        public IEnumerator CollapseColumns()
        {
            bool hasMoved = false;

            for (int x = 0; x < width; x++)
            {
                int emptyY = -1;

                for (int y = 0; y < height; y++)
                {
                    if (Tiles[x, y].IsEmpty)
                    {
                        if (emptyY == -1)
                            emptyY = y;
                    }
                    else if (emptyY != -1)
                    {
                        // 移动元素到空位
                        Tile tile = Tiles[x, y];
                        Tiles[x, emptyY] = tile;
                        Tiles[x, y] = CreateEmptyTile(x, y);
                        
                        tile.SetPosition(x, emptyY);
                        tile.AnimateToPosition(GetWorldPosition(x, emptyY));
                        
                        emptyY++;
                        hasMoved = true;
                    }
                }
            }

            if (hasMoved)
            {
                yield return new WaitForSeconds(0.3f);
            }
        }

        private Tile CreateEmptyTile(int x, int y)
        {
            Vector3 position = GetWorldPosition(x, y);
            GameObject tileObj = Instantiate(tilePrefab, position, Quaternion.identity, tilesParent);
            tileObj.name = $"Tile_{x}_{y}";

            Tile tile = tileObj.GetComponent<Tile>();
            tile.Initialize(x, y, null);
            tile.SetEmpty(true);
            return tile;
        }
    }
}
