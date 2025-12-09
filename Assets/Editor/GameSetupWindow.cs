using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace PawzyPop.Editor
{
    public class GameSetupWindow : EditorWindow
    {
        [MenuItem("PawzyPop/Setup Game Scene")]
        public static void SetupGameScene()
        {
            // 创建主相机
            GameObject cameraObj = GameObject.Find("Main Camera");
            if (cameraObj == null)
            {
                cameraObj = new GameObject("Main Camera");
                cameraObj.AddComponent<Camera>();
                cameraObj.AddComponent<AudioListener>();
            }
            
            Camera cam = cameraObj.GetComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = 5;
            cam.backgroundColor = new Color(0.2f, 0.6f, 0.8f);
            cam.transform.position = new Vector3(0, 0, -10);

            // 创建 GameManager
            CreateManagerObject("GameManager", typeof(Core.GameManager));
            
            // 创建 Board
            GameObject boardObj = CreateManagerObject("Board", typeof(Core.Board));
            
            // 创建 Tiles 父对象
            GameObject tilesParent = new GameObject("Tiles");
            tilesParent.transform.SetParent(boardObj.transform);

            // 创建 MatchFinder
            CreateManagerObject("MatchFinder", typeof(Core.MatchFinder));

            // 创建 MatchProcessor
            CreateManagerObject("MatchProcessor", typeof(Core.MatchProcessor));

            // 创建 InputManager
            CreateManagerObject("InputManager", typeof(Core.InputManager));

            Debug.Log("Game scene setup complete!");
            
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        private static GameObject CreateManagerObject(string name, System.Type componentType)
        {
            GameObject obj = GameObject.Find(name);
            if (obj == null)
            {
                obj = new GameObject(name);
            }

            if (obj.GetComponent(componentType) == null)
            {
                obj.AddComponent(componentType);
            }

            return obj;
        }

        [MenuItem("PawzyPop/Create Tile Prefab")]
        public static void CreateTilePrefab()
        {
            string prefabPath = "Assets/Prefabs";
            if (!AssetDatabase.IsValidFolder(prefabPath))
            {
                AssetDatabase.CreateFolder("Assets", "Prefabs");
            }

            // 创建 Tile GameObject
            GameObject tileObj = new GameObject("Tile");
            
            // 添加 SpriteRenderer
            SpriteRenderer sr = tileObj.AddComponent<SpriteRenderer>();
            sr.sortingOrder = 1;

            // 添加 BoxCollider2D
            BoxCollider2D collider = tileObj.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(0.9f, 0.9f);

            // 添加 Tile 脚本
            tileObj.AddComponent<Core.Tile>();

            // 创建默认 Sprite (圆形)
            CreateDefaultSprite(tileObj);

            // 保存为 Prefab
            string path = $"{prefabPath}/Tile.prefab";
            PrefabUtility.SaveAsPrefabAsset(tileObj, path);
            DestroyImmediate(tileObj);

            Debug.Log("Created Tile prefab at " + path);
        }

        private static void CreateDefaultSprite(GameObject obj)
        {
            // 使用 Unity 内置的圆形 Sprite
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            
            // 创建一个简单的白色圆形纹理
            Texture2D texture = new Texture2D(128, 128);
            Color[] colors = new Color[128 * 128];
            
            Vector2 center = new Vector2(64, 64);
            float radius = 60;

            for (int y = 0; y < 128; y++)
            {
                for (int x = 0; x < 128; x++)
                {
                    float dist = Vector2.Distance(new Vector2(x, y), center);
                    if (dist <= radius)
                    {
                        colors[y * 128 + x] = Color.white;
                    }
                    else
                    {
                        colors[y * 128 + x] = Color.clear;
                    }
                }
            }

            texture.SetPixels(colors);
            texture.Apply();

            // 保存纹理
            string spritePath = "Assets/Resources/Sprites";
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }
            if (!AssetDatabase.IsValidFolder(spritePath))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Sprites");
            }

            byte[] bytes = texture.EncodeToPNG();
            string texturePath = $"{spritePath}/DefaultTile.png";
            System.IO.File.WriteAllBytes(texturePath, bytes);
            AssetDatabase.Refresh();

            // 设置纹理导入设置
            TextureImporter importer = AssetImporter.GetAtPath(texturePath) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.spritePixelsPerUnit = 128;
                importer.SaveAndReimport();
            }

            // 加载并设置 Sprite
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(texturePath);
            sr.sprite = sprite;
        }
    }
}
