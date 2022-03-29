namespace DayFour;

public class Program
{
    public class Board
    {
        public record class Field
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public bool Marked { get; set; }
        }

        private Dictionary<int, Field> _board = new();

        public Board(List<string> data, int rows, int cols)
        {
            var board = data.ConvertAll(x => Int32.Parse(x));
            int row = 0;
            int col = 0;

            for (var index = 0; index < (rows * cols); index++)
            {
                row = index / rows;
                col = Math.Abs((row * rows) - index);
                _board.Add(board[index], new Field { Row = row, Col = col, Marked = false });
            }
        }

        public bool MarkNumber(int number)
        {
            Field? value;
            if (_board.TryGetValue(number, out value))
            {
                value.Marked = true;
                return CheckRowAndCol(value.Row, value.Col);
            }
            return false;
        }

        public bool CheckRowAndCol(int row, int col)
        {
            var fields = _board.Where(x => (x.Value.Row == row || x.Value.Col == col) && !x.Value.Marked);

            bool markedRow = true;
            bool markedCol = true;
            foreach (var field in fields)
            {
                if (markedRow && field.Value.Row == row)
                {
                    markedRow = false;
                }
                if (markedCol && field.Value.Col == col)
                {
                    markedCol = false;
                }
                if (!markedRow && !markedCol) return false;
            }
            return true;
        }

        public int FinalScore(int lastNumber)
        {
            int sum = _board.Where(x => x.Value.Marked == false).ToList().Sum(x => x.Key);
            return sum * lastNumber;
        }
    }

    public static void Main()
    {
        var inputs = File.ReadAllLines("input.txt").ToList();

        var chosenNumbers = inputs.ElementAt(0).Split(",");

        // after collecting the chosen numbers we can safely remove the first line
        inputs.RemoveAt(0);

        List<Board> boards = new();
        List<string> numbers = new();
        foreach (var line in inputs)
        {
            // the input file has an empty line as the board separator
            if (String.IsNullOrEmpty(line))
            {
                if (numbers.Count() > 0)
                {
                    boards.Add(new Board(numbers, 5, 5));
                    numbers.Clear();
                }
                continue;
            }
            var xs = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            numbers.AddRange(xs);
        }

        // this code works as well, but I think I'm traversing the list to many times unnecessary. I
        // need to benchmark it to see if the diff in performance is relevant. 
        // we are getting the chunk of 6 because we want to get the empty line separator as well
        // var boards = inputs
        //                 .Chunk(6)
        //                 .ToList()
        //                 .ConvertAll(
        //                     x => String.Join(" ", x)
        //                                .Split(" ")
        //                                .Where(x => !String.IsNullOrEmpty(x))
        //                                .ToList()
        //                                .ConvertAll(x => Int32.Parse(x))
        //                                .ToList())
        //                 .ConvertAll(x => new Board(x, 5, 5));

        for (var index = 0; index < chosenNumbers.Count(); index++)
        {
            for (var board = 0; board < boards.Count(); board++)
            {
                var number = Int32.Parse(chosenNumbers[index]);
                var won = boards[board].MarkNumber(number);
                if (won)
                {
                    Console.WriteLine(boards[board].FinalScore(number));
                    return;
                }
            }
        }
    }
}
