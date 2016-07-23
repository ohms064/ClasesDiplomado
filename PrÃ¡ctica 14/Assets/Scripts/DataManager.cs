using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class DataManager : MonoBehaviour {

    public string fileName;
    public static DataManager instance;

    string[] tmpString;
    char[] delimeter = {',', '|'};
    public int items;
    public int lives;

    public void LoadData() {
        if (File.Exists(fileName)) {
            StreamReader sr = new StreamReader(fileName);
            string line;
            while((line = sr.ReadLine()) != null) {
                tmpString = line.Split(delimeter);
                items = int.Parse(tmpString[0]);
                lives = int.Parse(tmpString[1]);
            }

            sr.Close();
        }
        else {
            items = 0;
            lives = 3;
        }

    }

    public void SaveData() {
        StreamWriter sw = new StreamWriter(fileName);
        string data2Write = items + "|" + lives;
        sw.WriteLine(data2Write);
        sw.Close();
    }

	// Use this for initialization
	void Awake () {
        instance = this;
        LoadData();
	}

}
