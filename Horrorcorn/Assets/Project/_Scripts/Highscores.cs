using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class HighScores
{
    public class Entry
    {
        public string Name;
        public int Score;

        public override string ToString()
        {
            return $"{Name}: {Timer.timerText.text}";
        }
    }
    
    private List<Entry> entries = new List<Entry>();
    
    private string fileName = "highscores.json";

    public HighScores()
    {
        Load();
    }

    private void Save()
    {
        string json = JsonConvert.SerializeObject(entries);
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json); //immer path zuerst sonst wird ins json der path gespeichert
    }

    private void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            entries = JsonConvert.DeserializeObject<List<Entry>>(json);
        }
        
    }
    
    public void AddEntry(string name, int score)
    {
        var entry = new Entry(){Name = name, Score = score};
        entries.Add(entry);
        Sort();
        Save();
    }

    private void Sort()
    {
        entries.Sort((entryA, entryB) => { return entryA.Score.CompareTo(entryB.Score); });
    }

    public override string ToString()
    {
        string result = "";
        foreach (Entry entry in entries)
        {
            result += entry.ToString() + "\n";
        }
        return result;
    }
}
