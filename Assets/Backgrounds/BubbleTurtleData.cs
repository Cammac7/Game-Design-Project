using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Text;
using System.IO;

public class BubbleTurtleData : MonoBehaviour
{
    public Transform graveObject;

    public bool Save(string fileName, Vector3 deathPosition)
    {
        // Handle any problems that might arise when reading the text
        try
        {
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine(deathPosition.x + "," + deathPosition.y + "," + deathPosition.z);

                return true;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return false;
        }
    }

    public bool Load(string fileName)
    {
        // Handle any problems that might arise when reading the text
        try
        {
            string line;
            
            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                // While there's lines left in the text file, do this:
                do
                {
                    line = sr.ReadLine();

                    if (line != null)
                    {
                        string[] entries = line.Split(',');
                        if (entries.Length > 0)
                        {
                            Vector3 deathPosition = new Vector3(float.Parse(entries[0]), float.Parse(entries[1]), float.Parse(entries[2]));
                        
                            InsertGrave(deathPosition);
                        }
                    }
                }
                while (line != null);

                // Done reading, close the reader and return true to broadcast success    
                sr.Close();
                return true;
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("No Deaths Written Yet");
            return false;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return false;
        }
    }

    private void InsertGrave(Vector3 gravePosition)
    {
        Transform grave = UnityEngine.Object.Instantiate(graveObject);
        grave.position = gravePosition;
    }
}
