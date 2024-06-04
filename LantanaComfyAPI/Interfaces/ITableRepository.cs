using LantanaComfyAPI.Models;

namespace LantanaComfyAPI.Interfaces
{
    public interface ITableRepository
    {  
        ICollection<Table> GetTables();
        Table GetTable(int tableId);
        bool TableExists (int tableId);
        bool CreateTable(Table table);
        bool DeleteTable(Table table);
        bool Save();
    }
}
