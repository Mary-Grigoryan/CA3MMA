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

    public async Task<FighterStats?> GetFighterStats(string fighterName)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://mma-stats.p.rapidapi.com/search?query={fighterName}");
            request.Headers.Add("X-RapidAPI-Key", "ea1a7aa5d6msh5ba879d6516cbc6p196e76jsn3034a6cdcfb0");
            request.Headers.Add("X-RapidAPI-Host", "mma-stats.p.rapidapi.com");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FighterStats>(jsonResponse);

        }
        catch (HttpRequestException httpEx)
        {
            // Log the exception details, adjust the logging mechanism as per your requirements
            Console.WriteLine($"An error occurred when calling the API: {httpEx.Message}");
            // Optionally, you could handle different status codes differently
            return null; // Or you could return a new FighterStats with an error message
        }
        catch (Exception ex)
        {
            // General exception handling, logging etc.
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return null; // Or you could return a new FighterStats with an error message
        }
    }
}

public class FighterStats
{
    public List<FighterResult> Results { get; set; }
}

public class FighterResult
{
    public string Name { get; set; }
    public string Nickname { get; set; }
    public string DivisionTitle { get; set; }
    public DivisionBody DivisionBody { get; set; }
    public BioData BioData { get; set; }
    public Dictionary<string, string> Stats { get; set; }
    public string SigStrikesLanded { get; set; }
    public string SigStrikesAttempted { get; set; }
    public string TakedownsLanded { get; set; }
    public string TakedownsAttempted { get; set; }
    public string StrikingAccuracy { get; set; }
    public string TakedownAccuracy { get; set; }
    public Records Records { get; set; }
    public LastFight LastFight { get; set; }
    public List<string> FighterFacts { get; set; }
    public List<string> UFCHistory { get; set; }
}

public class DivisionBody
{
    public string Wins { get; set; }
    public string Losses { get; set; }
    public string Draws { get; set; }
}

public class BioData
{
    public string Status { get; set; }
    public string Hometown { get; set; }
    public string TrainsAt { get; set; }
    public string Age { get; set; }
    public string Height { get; set; }
    public string Weight { get; set; }
    public string OctagonDebut { get; set; }
    public string Reach { get; set; }
    public string LegReach { get; set; }
}

public class Records
{
    public string WinsByKnockout { get; set; }
    public string WinsBySubmission { get; set; }
    public string FormerChampion { get; set; }
    public string SigStrLanded { get; set; }
    public string SigStrAbsorbed { get; set; }
    public string TakedownAvg { get; set; }
    public string SubmissionAvg { get; set; }
    public string SigStrDefense { get; set; }
    public string TakedownDefense { get; set; }
    public string KnockdownAvg { get; set; }
    public string AverageFightTime { get; set; }
}

public class LastFight
{
    public string Event { get; set; }
    public string FightNumber { get; set; }
    public string Matchup { get; set; }
    public string Date { get; set; }
}