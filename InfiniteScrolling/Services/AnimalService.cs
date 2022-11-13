using System.Net.Http;
using System.Text.Json;

namespace InfiniteScrolling.Services;

public class AnimalService
{
    //private HttpClient client;

    //public AnimalService()
    //{
    //    client = new();
    //}

    public async Task<List<EntryDetails>> GetAnimalList()
    {
        using HttpClient client = new();
        var returnResponse = new List<EntryDetails>();
        var response = await client.GetAsync("https://api.publicapis.org/entries");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            var deserializeContent =
                JsonSerializer.Deserialize<MainResponse>(content,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if(deserializeContent?.Entries?.Count() > 0)
            {
                returnResponse =  deserializeContent.Entries;
            }
        }
        return returnResponse;
    }
}
