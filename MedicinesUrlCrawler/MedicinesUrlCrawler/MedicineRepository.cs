using MongoDB.Driver;
using System.Configuration;
using System.Threading.Tasks;

namespace MedicinesUrlCrawler
{
    public class MedicineRepository
    {
        private readonly IMongoCollection<Medicine> collection;
        
        public MedicineRepository(string conn = null)
        {
            string connectionString = string.Empty;
            if (string.IsNullOrEmpty(conn))
            {
                connectionString = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
            }
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Medicines");
            collection = database.GetCollection<Medicine>("MedicinesUrls");
        }

        public async Task AddMedicine(Medicine medicine)
        {
            await collection.InsertOneAsync(medicine);
        }

        public async Task<int> GetMedicinesNumber()
        {
            return (int)await collection.CountDocumentsAsync(FilterDefinition<Medicine>.Empty);
        }
    }
}
