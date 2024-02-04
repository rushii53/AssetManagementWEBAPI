using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.DataContext
{
    public class DbContext
    {
        public List<Machine> machines = new List<Machine>();
        public DbContext() { }
    }
}
