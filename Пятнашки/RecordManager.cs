using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Пятнашки
{
    public struct RecordEntry
    {
        public int GridSize { get; set; }      
        public int Moves { get; set; }         
        public DateTime Date { get; set; }    
    }

    public class RecordManager
    {
        private readonly string _filePath;
        private List<RecordEntry> _records;

        public RecordManager()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "records.json");
            LoadRecords();
        }

        private void LoadRecords()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    _records = JsonSerializer.Deserialize<List<RecordEntry>>(json) ?? new List<RecordEntry>();
                }
                else
                {
                    _records = new List<RecordEntry>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки рекордов: {ex.Message}");
                _records = new List<RecordEntry>();
            }
        }

        private void SaveRecords()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_records, options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения рекордов: {ex.Message}");
            }
        }

        public int? GetBestMoves(int gridSize)
        {
            var best = _records
                .Where(r => r.GridSize == gridSize)
                .OrderBy(r => r.Moves)
                .FirstOrDefault();
            return best.GridSize == gridSize ? (int?)best.Moves : null;
        }

        public bool TryAddRecord(int gridSize, int moves)
        {
            int? currentBest = GetBestMoves(gridSize);

            if (!currentBest.HasValue || moves < currentBest.Value)
            {
                _records.Add(new RecordEntry
                {
                    GridSize = gridSize,
                    Moves = moves,
                    Date = DateTime.Now
                });
                SaveRecords();
                return true;
            }
            return false;
        }

        public List<RecordEntry> GetAllRecords()
        {
            return _records
                .OrderBy(r => r.GridSize)
                .ThenBy(r => r.Moves)
                .ToList();
        }
    }
}