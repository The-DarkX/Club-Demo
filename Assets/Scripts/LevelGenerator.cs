using System.Collections.Generic;
using UnityEngine;
using System.IO;

[DisallowMultipleComponent]
public class LevelGenerator : MonoBehaviour
{
    public int mapIndex;

    public Texture2D[] maps;

    public GameObject groundPlane;

    [Header("Color Presets")]
    public ColorToPrefab[] colorMappings;

    Texture2D[] builtInLevels;
    List<Texture2D> customLevels = new List<Texture2D>();

    List<Vector3> objectPositions = new List<Vector3>();

    Texture2D currentMap;

    void Awake()
    {
        //LoadLevels();
        GenerateLevel();
    }

    void LoadLevels()
    {
        //Load built in Levels
        builtInLevels = Resources.LoadAll<Texture2D>("Levels");

        //Load custom levels
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/StreamingAssets/Custom Levels");
        FileInfo[] info = dir.GetFiles("*.png");
        foreach (FileInfo f in info)
        {
            byte[] bytes = File.ReadAllBytes(f.FullName);
            Texture2D levelMap = new Texture2D(2, 2);
            levelMap.LoadImage(bytes);

            customLevels.Add(levelMap);
        }
    }

    void GenerateLevel()
	{
		//SelectMap(true, mapIndex);

		currentMap = maps[mapIndex];

		for (int x = 0; x < currentMap.width; x++)
		{
			for (int y = 0; y < currentMap.height; y++)
			{
				GenerateTile(x, y);
			}
		}

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Wall"))
                objectPositions.Add(transform.GetChild(i).position);
        }

        GroundSpawner();
	}

	private void GroundSpawner()
	{
        float width = Mathf.Abs(objectPositions[0].x - objectPositions[objectPositions.Count - 1].x);
        float height = Mathf.Abs(objectPositions[0].z - objectPositions[objectPositions.Count - 1].z);
		int a = objectPositions.Count / 2;

		Vector3 centerPos = new Vector3(objectPositions[a].x, 0, objectPositions[a].z);
        Vector3 planeScale = new Vector3(width + 1f, 0.1f, height + 1f);

        GameObject ground = Instantiate(groundPlane, centerPos, Quaternion.identity, transform);

		ground.transform.localScale = planeScale;
	}

	void GenerateTile(int x, int z) 
    {
        Color pixelColor = currentMap.GetPixel(x, z);

        if (pixelColor.a != 1 || pixelColor == Color.white) { return; }      //Ignore transparent or white pixels

        foreach (ColorToPrefab colorMaping in colorMappings) 
        {
            if (colorMaping.color.Equals(pixelColor))
            {
                print("color equal");
                Vector3 position = new Vector3(x, 0.5f, z);

                if (colorMaping.prefab.layer == LayerMask.NameToLayer("Wall"))
                    Instantiate(colorMaping.prefab, position, Quaternion.identity, transform);

                else
                    Instantiate(colorMaping.prefab, position, Quaternion.identity);
            }
        }
    }

    void SelectMap(bool useBuiltIn, int index) 
    {
        if (useBuiltIn)
        {
            try
            {
                currentMap = builtInLevels[index];
                print(currentMap.name);
            }
            catch
            {
                currentMap = new Texture2D(5, 5);
                Debug.LogError("Map could not be found!");
                return;
            }
        }
        else 
        {
            try
            {
                currentMap = customLevels[index];
                print(currentMap.name);
            }
            catch
            {
                currentMap = new Texture2D(5, 5);
                Debug.LogError("Map could not be found!");
                return;
            }
        }

    }
}

[System.Serializable]
public class ColorToPrefab
{
    public Color color;
    public GameObject prefab;
}