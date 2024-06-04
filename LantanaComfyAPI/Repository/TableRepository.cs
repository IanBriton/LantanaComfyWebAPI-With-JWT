using LantanaComfyAPI.Data;
using LantanaComfyAPI.Interfaces;
using LantanaComfyAPI.Models;

namespace LantanaComfyAPI.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly DataContext _context;

        public TableRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Table> GetTables()
        {
            return _context.Tables.OrderBy(t=>t.Id).ToList();
        }

        public Table GetTable(int tableId)
        {
            return _context.Tables.FirstOrDefault(t => t.Id == tableId);
        }


        public bool TableExists(int tableId)
        {
            return _context.Tables.Any(t => t.Id == tableId);
        }

        public bool CreateTable(Table table)
        {
            _context.Add(table);
            return Save();
        }

        public bool DeleteTable(Table table)
        {
            _context.Remove(table);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
