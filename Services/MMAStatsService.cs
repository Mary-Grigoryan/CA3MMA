using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class MMAStatsService
{
    private readonly HttpClient _httpClient;

    public MMAStatsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<FighterResult>?> SearchFighters(string searchTerm)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://mma-stats.p.rapidapi.com/search?name={Uri.EscapeDataString(searchTerm)}");
            request.Headers.Add("X-RapidAPI-Key", "ea1a7aa5d6msh5ba879d6516cbc6p196e76jsn3034a6cdcfb0");
            request.Headers.Add("X-RapidAPI-Host", "mma-stats.p.rapidapi.com");

            // var query = Uri.EscapeDataString(searchTerm);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var searchResults = JsonConvert.DeserializeObject<FighterStats>(jsonResponse);
            return searchResults?.Results;
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"An error occurred when calling the API: {httpEx.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return null;
        }
    }

    public async Task<FighterStats?> GetFighterStats(string fighterName)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://mma-stats.p.rapidapi.com/search?name={Uri.EscapeDataString(fighterName)}");
            request.Headers.Add("X-RapidAPI-Key", "ea1a7aa5d6msh5ba879d6516cbc6p196e76jsn3034a6cdcfb0");
            request.Headers.Add("X-RapidAPI-Host", "mma-stats.p.rapidapi.com");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FighterStats>(jsonResponse);
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"An error occurred when calling the API: {httpEx.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return null;
        }
    }
}

public class FighterStats
{
    [JsonProperty("results")]
    public List<FighterResult>? Results { get; set; } // It's okay for Results to be null
}

public class FighterResult
{
    [JsonProperty("Name")]
    public string? Name { get; set; }

    [JsonProperty("Nickname")]
    public string? Nickname { get; set; }

    [JsonProperty("Division Title")]
    public string? DivisionTitle { get; set; }

    [JsonProperty("Division Body")]
    public DivisionBody? DivisionBody { get; set; }

    [JsonProperty("Bio Data")]
    public BioData? BioData { get; set; }

    [JsonProperty("Stats")]
    public Dictionary<string, string>? Stats { get; set; }

    [JsonProperty("Sig. Strikes Landed")]
    public string? SigStrikesLanded { get; set; }

    [JsonProperty("Sig. Strikes Attempted")]
    public string? SigStrikesAttempted { get; set; }

    [JsonProperty("Takedowns Landed")]
    public string? TakedownsLanded { get; set; }

    [JsonProperty("Takedowns Attempted")]
    public string? TakedownsAttempted { get; set; }

    [JsonProperty("Striking accuracy")]
    public string? StrikingAccuracy { get; set; }

    [JsonProperty("Takedown Accuracy")]
    public string? TakedownAccuracy { get; set; }

    [JsonProperty("Records")]
    public Records? Records { get; set; }

    [JsonProperty("Last Fight")]
    public LastFight? LastFight { get; set; }

    [JsonProperty("Fighter Facts")]
    public List<string>? FighterFacts { get; set; }

    [JsonProperty("UFC History")]
    public List<string>? UFCHistory { get; set; }
}

public class DivisionBody
{
    [JsonProperty("Wins")]
    public string? Wins { get; set; }

    [JsonProperty("Losses")]
    public string? Losses { get; set; }

    [JsonProperty("Draws")]
    public string? Draws { get; set; }
}

public class BioData
{
    [JsonProperty("Status")]
    public string? Status { get; set; }

    [JsonProperty("Hometown")]
    public string? Hometown { get; set; }

    [JsonProperty("Trains at")]
    public string? TrainsAt { get; set; }

    [JsonProperty("Fighting style")]
    public string? FightingStyle { get; set; }

    [JsonProperty("Age")]
    public string? Age { get; set; }

    [JsonProperty("Height")]
    public string? Height { get; set; }

    [JsonProperty("Weight")]
    public string? Weight { get; set; }

    [JsonProperty("Octagon Debut")]
    public string? OctagonDebut { get; set; }

    [JsonProperty("Reach")]
    public string? Reach { get; set; }

    [JsonProperty("Leg reach")]
    public string? LegReach { get; set; }
}

public class Records
{
    [JsonProperty("Wins by Knockout")]
    public string? WinsByKnockout { get; set; }

    [JsonProperty("Wins by Submission")]
    public string? WinsBySubmission { get; set; }

    [JsonProperty("Sig. Str. Landed")]
    public string? SigStrLanded { get; set; }

    [JsonProperty("Sig. Str. Absorbed")]
    public string? SigStrAbsorbed { get; set; }

    [JsonProperty("Takedown avg")]
    public string? TakedownAvg { get; set; }

    [JsonProperty("Submission avg")]
    public string? SubmissionAvg { get; set; }

    [JsonProperty("Sig. Str. Defense")]
    public string? SigStrDefense { get; set; }

    [JsonProperty("Takedown Defense")]
    public string? TakedownDefense { get; set; }

    [JsonProperty("Knockdown Avg")]
    public string? KnockdownAvg { get; set; }

    [JsonProperty("Average fight time")]
    public string? AverageFightTime { get; set; }
}

public class LastFight
{
    [JsonProperty("Event")]
    public string? Event { get; set; }

    [JsonProperty("Fight Number")]
    public string? FightNumber { get; set; }

    [JsonProperty("Matchup")]
    public string? Matchup { get; set; }

    [JsonProperty("Date")]
    public string? Date { get; set; }
}