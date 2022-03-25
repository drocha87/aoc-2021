namespace DayThree;

public class Program
{
    public class SupportRating
    {
        public enum Data
        {
            Oxygen,
            CO2,
        }

        private readonly int _bits;
        private readonly List<uint> _report;

        public SupportRating(List<string> inputs)
        {
            _bits = inputs[0].Count();
            _report = inputs.ConvertAll(x => Convert.ToUInt32(x, 2));
        }

        public Task<uint> GetRatingAsync(Data data)
        {
            List<uint> report = _report;
            for (var shr = _bits - 1; shr >= 0; shr--)
            {
                var zeroes = new List<uint>();
                var ones = new List<uint>();
                foreach (var number in report)
                {
                    if (((number >> shr) & 1) > 0) ones.Add(number);
                    else zeroes.Add(number);
                }
                if (data == Data.Oxygen)
                {
                    report = zeroes.Count() > ones.Count() ? zeroes : ones;
                }
                else
                {
                    report = zeroes.Count() > ones.Count() ? ones : zeroes;

                }
                if (report.Count() == 1) break;
            }
            return Task.FromResult(report[0]);
        }

        public uint GetGammaRate()
        {
            int[] bits = new int[_bits];
            foreach (var data in _report)
            {
                for (var index = 0; index < _bits; index++)
                {
                    bits[index] += ((data >> index) & 1) == 0 ? -1 : 1;
                }
            }
            uint result = 0;
            for (var index = 0; index < _bits; index++)
            {
                result |= (uint)(bits[index] > 0 ? 1 : 0) << index;
            }
            return result;
        }

        public uint GetEpsilonRate(uint gammaRate)
        {
            return ~(~0u << _bits) ^ gammaRate;
        }
    }

    public static void PartOne(List<string> inputs)
    {
        var supportRating = new SupportRating(inputs);
        var gammaRate = supportRating.GetGammaRate();
        var epsilonRate = supportRating.GetEpsilonRate(gammaRate);
        Console.WriteLine(gammaRate * epsilonRate);
    }

    public static async Task PartTwo(List<string> inputs)
    {
        var supportRating = new SupportRating(inputs);
        Console.WriteLine(await supportRating.GetRatingAsync(SupportRating.Data.Oxygen) *
                          await supportRating.GetRatingAsync(SupportRating.Data.CO2));
    }

    public static async Task Main()
    {
        var inputs = File.ReadAllLines("input.txt").ToList();
        Program.PartOne(inputs);
        await Program.PartTwo(inputs);
    }
}
