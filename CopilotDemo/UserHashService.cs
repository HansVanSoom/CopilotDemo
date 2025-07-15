using Azure;
using Azure.Data.Tables;
using System.Collections.Concurrent;

namespace CopilotDemo
{
    public class UserHashService 
    {
        private readonly TableClient _tableClient;
        private readonly ILogger<UserHashService> _logger;

        public UserHashService(TableClient tableClient, ILogger<UserHashService> logger)
        {
            _tableClient = tableClient;
            _logger = logger;
        }

        public async Task<UserHash?> GetUserHashAsync(string uid)
        {
          
            try
            {
                var response = await _tableClient.GetEntityAsync<UserHash>("users", uid);
                return response.Value;
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                Console.WriteLine($"Azure Table Storage request failed: {ex.Status} - {ex.Message}");
                _logger.LogInformation("UserHash entity not found for UID: {UID}", uid);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the user hash for UID: {UID}", uid);
                throw;
            }
        }
    }
}


