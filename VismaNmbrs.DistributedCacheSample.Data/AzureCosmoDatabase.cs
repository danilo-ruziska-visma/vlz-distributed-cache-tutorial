using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using VismaNmbrs.DistributedCacheSample.Data.Options;
using VismaNmbrs.DistributedCacheSample.Entities;

namespace VismaNmbrs.DistributedCacheSample.Data
{
    public class AzureCosmoDatabase<T> : IAsyncDatabase<T> where T : BaseEntity
    {
        // The container we will create.
        private readonly Container container;

        public AzureCosmoDatabase(IOptions<AzureCosmoOptions> azureCosmoOptions)
        {
            // The database we will create
            Database database;
            // The Cosmos client instance
            string databaseId = "CustomerService";
            CosmosClient cosmosClient;
            cosmosClient = new CosmosClient(azureCosmoOptions.Value.EndpointUri, azureCosmoOptions.Value.PrimaryKey);
            // The name of the database and container we will create
            database = cosmosClient.GetDatabase(databaseId);
            container = database.GetContainer(typeof(T).Name);
        }

        public async Task<T> Add(T itemToAdd)
        {            
            itemToAdd.Id = Guid.NewGuid();
            var response = await container.CreateItemAsync<T>(itemToAdd);            
            return response.Resource;
        }

        public async Task<IList<T>> GetAll()
        {
            string sqlQueryText = "SELECT * FROM c";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            using FeedIterator<T> queryResultSetIterator = container.GetItemQueryIterator<T>(queryDefinition);

            List<T> itemList = new List<T>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<T> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (T item in currentResultSet)
                {
                    itemList.Add(item);                    
                }
            }

            return itemList;
        }

        public async Task<T> GetById(Guid id)
        {
            var itemResponse = await container.ReadItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
            return itemResponse.Resource;
        }

        public async Task<T> Update(T itemToUpdate)
        {
            var response = await container.ReplaceItemAsync<T>(itemToUpdate, itemToUpdate.Id.ToString(), new PartitionKey(itemToUpdate.Id.ToString()));
            return response.Resource;
        }

        public async Task<T> Delete(Guid id)
        {
            var itemResponse = await container.DeleteItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
            return itemResponse.Resource;
        }
    }
}