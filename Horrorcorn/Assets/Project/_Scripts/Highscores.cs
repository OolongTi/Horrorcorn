
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class HighScores
{
    
    
    public class Entry
    {
        public string PlayerName;
        public float time;

        public override string ToString()
        {
            return $"{PlayerName}: {time:F1}s";
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
        FillEmptySlots();
    }
    
    private void FillEmptySlots()
    {
        while (entries.Count < 5)
        {
            entries.Add(new Entry { PlayerName = "Dude", time = 50.0f });
        }
    }
    
    public void AddEntry(string name, float score)
    {
        if (string.IsNullOrEmpty(name)) name = "Unknown";
        var entry = new Entry(){PlayerName = name, time = score};
        entries.Add(entry);
        Sort();
        Save();
    }

    private void Sort()
    {
        entries.Sort((entryA, entryB) => { return entryA.time.CompareTo(entryB.time); });
    }

    public override string ToString()
    {
        string result = "Highscores:\n";
        for (int i = 0; i < 5; i++)
        {
            result += $"{i + 1}. {entries[i]}\n";
        }
        return result;
    }
}
