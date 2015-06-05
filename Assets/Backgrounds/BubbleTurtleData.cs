using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.IO;

public class BubbleTurleData : MonoBehaviour
{

    public bool Save(string fileName)
    {
        //get the position of death then save it to the file


        // Handle any problems that might arise when reading the text
        try
        {
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine("Something");

                return true;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            return false;
        }
    }

    public bool Load(string fileName)
    {
        // Handle any problems that might arise when reading the text
        try
        {
            string line;
            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            StreamReader theReader = new StreamReader(fileName, Encoding.Default);

            // Immediately clean up the reader after this block of code is done.
            // You generally use the "using" statement for potentially memory-intensive objects
            // instead of relying on garbage collection.
            // (Do not confuse this with the using directive for namespace at the 
            // beginning of a class!)
            using (theReader)
            {
                // While there's lines left in the text file, do this:
                do
                {
                    line = theReader.ReadLine();

                    if (line != null)
                    {
                        // Do whatever you need to do with the text line, it's a string now
                        // In this example, I split it into arguments based on comma
                        // deliniators, then send that array to DoStuff()
                        string[] entries = line.Split(',');
                        //if (entries.Length > 0)
                        //DoStuff(entries);
                        //probably want to insert the graves into the positions here...
                    }
                }
                while (line != null);

                // Done reading, close the reader and return true to broadcast success    
                theReader.Close();
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            return false;
        }
    }
}
