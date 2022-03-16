using JOS.WeightedResult;

namespace TrisslottSimulator;
public class Scratcher
{

    public Scratcher()
    {
        ScratchResults = new Dictionary<int, int>();

        var numberOfTickets = 12_000_000;
        var totalNumberOfWins = 2_574_000;
        var totalNumberOfNonWins = numberOfTickets - totalNumberOfWins;
        _prices = new List<ProbabilityItem<int>>
            {
                new ProbabilityItem<int>(totalNumberOfNonWins, 0),
                new ProbabilityItem<int>(1_202_118, 30, "30 SEK"),
                new ProbabilityItem<int>(1131114, 60, "60 SEK"),
                new ProbabilityItem<int>(156000, 90, "90 SEK"),
                new ProbabilityItem<int>(43200, 120, "120 SEK"),
                new ProbabilityItem<int>(22560, 150, "150 SEK"),
                new ProbabilityItem<int>(7200, 180, "180 SEK"),
                new ProbabilityItem<int>(5580, 300, "300 SEK"),
                new ProbabilityItem<int>(1200, 600, "600 SEK"),
                new ProbabilityItem<int>(1200, 500, "500 SEK"),
                new ProbabilityItem<int>(960, 1_000, "1000 SEK"),
                new ProbabilityItem<int>(750, 450, "450 SEK"),
                new ProbabilityItem<int>(480, 1_500, "1500 SEK"),
                new ProbabilityItem<int>(360, 900, "900 SEK"),
                new ProbabilityItem<int>(300, 750, "750 SEK"),
                new ProbabilityItem<int>(300, 2_000, "2000 SEK"),
                new ProbabilityItem<int>(276, 10_000, "10 000 SEK"),
                new ProbabilityItem<int>(180, 5_000, "5000 SEK"),
                new ProbabilityItem<int>(90, 2_500, "2500 SEK"),
                new ProbabilityItem<int>(48, 20_000, "20 000 SEK"),
                new ProbabilityItem<int>(36, 265_000, "265 000 SEK"),
                new ProbabilityItem<int>(18, 50_000, "50 000 SEK"),
                new ProbabilityItem<int>(12, 100_000, "100 000 SEK"),
                new ProbabilityItem<int>(6, 200_000, "200 000 SEK"),
                new ProbabilityItem<int>(6, 1_000_000, "1 000 000 SEK"),
                new ProbabilityItem<int>(6, 2_765_000, "2 765 000 SEK")
            };
        _query = new WeightedResultQuery<int>(_prices);


    }
    private WeightedResultQuery<int> _query { get; }
    private List<ProbabilityItem<int>> _prices { get; }
    private Dictionary<int, int> _nonLosses => ScratchResults.Where(x => x.Key != 0).ToDictionary(x => x.Key, x => x.Value);
    private const int TicketPrice = 30;
    public int NumberOfScratches { get; set; }
    public int AmountSpent => TicketPrice * NumberOfScratches;
    public Dictionary<int, int> ScratchResults { get; }
    //public Dictionary<int,int> SortedDict { get => ScratchResults.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);}

    public int LostAmount
    {
        get
        {
            if (ScratchResults.ContainsKey(0))
            {
                return ScratchResults[0] * TicketPrice;
            }

            return 0;
        }
    }

    public int LostTickets
    {
        get
        {
            if (ScratchResults.ContainsKey(0))
            {
                return ScratchResults[0];
            }

            return 0;
        }
    }

    public int WonAmount
    {
        get
        {
            var won = 0;
            foreach (var item in _nonLosses)
            {
                won += item.Value * item.Key;
            }

            return won;
        }
    }

    public int WonTickets
    {
        get
        {
            return _nonLosses.Values.Sum();
        }
    }

    public int Balance => WonAmount - AmountSpent;

    public Dictionary<int,int> Scratch(int count)
    {
        

        var results = new Dictionary<int, int>();
        for (var i = 0; i < count; i++)
        {
            var result = _query.Execute();
            if (!results.ContainsKey(result))
            {
                results.Add(result, 0);
            }

            results[result]++;
        }

        NumberOfScratches += count;

        foreach (var item in results)
        {
            if (!ScratchResults.ContainsKey(item.Key))
            {
                ScratchResults[item.Key] = 0;
            }
            ScratchResults[item.Key] += item.Value;
        }
        return ScratchResults;
    }

}
