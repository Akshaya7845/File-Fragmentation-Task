using System;
using System.IO;

public class FileModel
{
    public string InputFileName { get; } = "input.txt";
    public string OutputFileName { get; } = "output.txt";

    public void SaveInputText(string text)
    {
        File.WriteAllText(InputFileName, text);
    }

    public void DeleteOldSplitFiles()
    {
        foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt"))
        {
            string name = Path.GetFileName(file);
            if (name != InputFileName && name != OutputFileName)
            {
                File.Delete(file);
            }
        }
    }

    public void WriteSplitFile(string fileLabel, string content)
    {
        File.WriteAllText(fileLabel, content);
    }

    public string ReadFileContent(string fileLabel)
    {
        return File.ReadAllText(fileLabel);
    }

    public void MergeFiles(string[] fileLabels)
    {
        string merged = "";
        foreach (var file in fileLabels)
        {
            merged += File.ReadAllText(file);
        }

        File.WriteAllText(OutputFileName, merged);
    }

    public bool CompareInputAndOutput()
    {
        if (!File.Exists(InputFileName) || !File.Exists(OutputFileName))
            return false;

        string input = File.ReadAllText(InputFileName);
        string output = File.ReadAllText(OutputFileName);
        return input == output;
    }
}
