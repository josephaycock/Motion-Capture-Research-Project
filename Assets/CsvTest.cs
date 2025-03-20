using UnityEngine;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

public class CsvTest : MonoBehaviour
{
    void Start()
    {
        // A small CSV in memory (no actual file needed for a quick test).
        string testCsv = "Id,Name\n1,John\n2,Jane";

        using (var reader = new StringReader(testCsv))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Define a simple model class in your script to match CSV columns.
            IEnumerable<PersonRecord> records = csv.GetRecords<PersonRecord>();

            // Print out each record to the Unity console
            foreach (var record in records)
            {
                Debug.Log($"ID: {record.Id}, Name: {record.Name}");
            }
        }
    }

    private class PersonRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
