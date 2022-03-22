namespace DayTwo;

public enum Direction
{
    Forward,
    Down,
    Up,
}

public record struct Movement
{
    public Direction Direction;
    public int Steps;
}

public static class Extensions
{
    public static Direction DirectionFromString(string str)
    {
        switch (str)
        {
            case "forward": return Direction.Forward;
            case "down": return Direction.Down;
            case "up": return Direction.Up;
            default: break;
        }
        throw new ArgumentException($"direction {str} is invalid");
    }
}

public class Program
{
    public static void PartOne(List<Movement> inputs)
    {
        var horizontal = 0;
        var depth = 0;

        foreach (var move in inputs)
        {
            switch (move.Direction)
            {
                case Direction.Forward:
                    horizontal += move.Steps;
                    break;
                case Direction.Up:
                    depth -= move.Steps;
                    break;
                case Direction.Down:
                    depth += move.Steps;
                    break;
                default:
                    throw new InvalidOperationException(
                        "unreachable: direction is not recognized, this must be a bug in the implementation"
                    );
            }
        }
        Console.WriteLine(horizontal * depth);
    }

    public static void PartTwo(List<Movement> inputs)
    {
        var horizontal = 0;
        var depth = 0;
        var aim = 0;

        foreach (var move in inputs)
        {
            switch (move.Direction)
            {
                case Direction.Forward:
                    horizontal += move.Steps;
                    depth += aim * move.Steps;
                    break;
                case Direction.Up:
                    aim -= move.Steps;
                    break;
                case Direction.Down:
                    aim += move.Steps;
                    break;
                default:
                    throw new InvalidOperationException(
                        "unreachable: direction is not recognized, this must be a bug in the implementation"
                    );
            }
        }
        Console.WriteLine(horizontal * depth);
    }

    public static void Main()
    {
        var inputs = File
           .ReadAllLines("input.txt")
           .ToList()
           .ConvertAll((string x) =>
           {
               var args = x.Split(' ');

               return new Movement
               {
                   Direction = Extensions.DirectionFromString(args[0]),
                   Steps = Int32.Parse(args[1])
               };
           });

        Program.PartOne(inputs);
        Program.PartTwo(inputs);
    }
}
