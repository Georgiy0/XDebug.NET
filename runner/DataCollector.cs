using System.Collections.Concurrent;
using System.Globalization;
using CsvHelper;

namespace xdebugnet.runner;

public class DataEntry
{
    public System.String FileUri { get; set; }
    public System.String Uri { get; set; }
    public System.String Correlator { get; set; }
}

internal class DataCollector
{
    private ConcurrentBag<DataEntry> _dataEntries = new ConcurrentBag<DataEntry>();
    public void AddData(DataEntry data)
    {
        _dataEntries.Add(data);
        Console.WriteLine($"PHP file access:\n\tfile uri: {data.FileUri}\n\trequest uri: {data.Uri}\n\tcorrelator: {data.Correlator}");
    }
    public void SaveToCsv(System.String filePath)
    {
        try
        {
            using (var writer = new StreamWriter(filePath))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(_dataEntries);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving to csv: {ex}");
        }
    }
}