using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject waterPrefab;
    public GameObject flagPrefab;
    public GameObject coinPrefab;
    public Transform environmentRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        // Go through the rows from bottom to top
        int row = 0;
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int column = 0; column < letters.Length; column++)
            {
                var letter = letters[column];
                Vector3 position = new Vector3(column + 0.5f, row + 0.5f, -0.5f);
                Vector3 flagPosition = new Vector3(column + 0.5f, row + 5f, -0.5f);
                if (letter == 'x')
                    Instantiate(rockPrefab, position, Quaternion.identity, environmentRoot);
                else if (letter == '?')
                    Instantiate(questionBoxPrefab, position, Quaternion.identity, environmentRoot);
                else if (letter == 'b')
                    Instantiate(brickPrefab, position, Quaternion.identity, environmentRoot);
                else if (letter == 's')
                    Instantiate(stonePrefab, position, Quaternion.identity, environmentRoot);
                else if (letter == 'w')
                    Instantiate(waterPrefab, position, Quaternion.identity, environmentRoot);
                else if (letter == 'f')
                    Instantiate(flagPrefab, flagPosition, Quaternion.identity, environmentRoot);
                else if (letter == 'c')
                    Instantiate(coinPrefab, position, Quaternion.identity, environmentRoot);
            }
            row++;
        }

    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
