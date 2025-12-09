using UnityEngine;
using UnityEditor;
using PawzyPop.Core;

namespace PawzyPop.Editor
{
    public class TileTypeCreator : EditorWindow
    {
        [MenuItem("PawzyPop/Create Default Tile Types")]
        public static void CreateDefaultTileTypes()
        {
            string path = "Assets/Resources/TileTypes";
            
            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "TileTypes");
            }

            CreateTileType("Shiba", new Color(1f, 0.8f, 0.4f), path);      // 柴犬 - 橙黄色
            CreateTileType("Corgi", new Color(1f, 0.6f, 0.2f), path);      // 柯基 - 橙色
            CreateTileType("Golden", new Color(1f, 0.85f, 0.5f), path);    // 金毛 - 金色
            CreateTileType("Husky", new Color(0.6f, 0.8f, 1f), path);      // 哈士奇 - 蓝灰色
            CreateTileType("Teddy", new Color(0.8f, 0.5f, 0.3f), path);    // 泰迪 - 棕色
            CreateTileType("Samoyed", new Color(1f, 1f, 1f), path);        // 萨摩 - 白色

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("Created 6 default tile types in " + path);
        }

        private static void CreateTileType(string typeName, Color color, string path)
        {
            TileType tileType = ScriptableObject.CreateInstance<TileType>();
            tileType.typeName = typeName;
            tileType.color = color;
            tileType.scoreValue = 10;

            string assetPath = $"{path}/{typeName}.asset";
            AssetDatabase.CreateAsset(tileType, assetPath);
        }
    }
}
