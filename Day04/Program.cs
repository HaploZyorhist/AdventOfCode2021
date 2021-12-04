StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();

var bingo = data.Split("\r\n\r\n").ToList();

int finalCall = 0;

string numberline = bingo[0];
bingo.Remove(numberline);
var callNumbers = numberline.Split(",").ToList();

var calls = CreateCallList(callNumbers);
var boards = GenerateBoards(bingo);

var result = CallNumbers(calls, boards);

var final = CalculateWinValue(finalCall, result);

Console.WriteLine($"The winning board total is {final}");

#region Methods

(int, bool)[,] CallNumbers(List<int> calls, List<(int, bool)[,]> boards)
{
    for (int i = 0; i < calls.Count; i++)
    {
        var result = UpdateBoards(calls[i], boards);

        // part 1
        //if (result != null)
        //{
        //    finalCall = calls[i];
        //    return result;
        //}

        // part 2
        if (boards.Count == 1 && result.Count == 1)
        {
            finalCall = calls[i];
            return boards[0];
        }
        if (boards.Count > 1 &&
           result != null)
        {
            foreach (var b in result)
            {
                boards.Remove(b);
            }
        }
    }

    return null;
}

List<int> CreateCallList(List<string> callNumbers)
{
    List<int> calls = new List<int>();

    foreach (var callNumber in callNumbers)
    {
        int num = int.Parse(callNumber);
        calls.Add(num);
    }

    return calls;
}

List<(int, bool)[,]> GenerateBoards(List<string> inputs)
{
    List<(int number, bool pulled)[,]> boards = new List<(int, bool)[,]>();

    for (int i = 0; i < inputs.Count; i++)
    {
        var boardInputs = inputs[i].Split("\r\n").ToList();

        var board = GenerateBoard(boardInputs);
        boards.Add(board);
    }

    return boards;
}

(int number, bool called)[,] GenerateBoard(List<string> inputs)
{
    (int number, bool pulled)[,] board = new (int, bool)[5, 5];

    for (int i = 0; i < inputs.Count; i++)
    {
        var horizNum = inputs[i].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        for (int j = 0; j < horizNum.Count; j++)
        {
            var num = int.Parse(horizNum[j].ToString());

            board[i, j].number = num;
        }
    }

    return board;
}

List<(int num, bool called)[,]> UpdateBoards(int number, List<(int num, bool called)[,]> boards)
{
    List<(int num, bool called)[,]> removals = new List<(int num, bool called)[,]>();
    foreach (var board in boards)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (board[i, j].num == number && board[i, j].called == false)
                {
                    board[i, j].called = true;
                    var found = CheckWin(board);
                    if (found)
                    {
                        removals.Add(board);
                    }
                }
            }
        }
    }

    return removals;
}

bool CheckWin((int num, bool called)[,] board)
{
    for (int i = 0; i < 5; i++)
    {
        if (board[i, 0].called == true &&
            board[i, 1].called == true &&
            board[i, 2].called == true &&
            board[i, 3].called == true &&
            board[i, 4].called == true)
        {
            return true;
        }
    }

    for (int i = 0; i < 5; i++)
    {
        if (board[0, i].called == true &&
            board[1, i].called == true &&
            board[2, i].called == true &&
            board[3, i].called == true &&
            board[4, i].called == true)
        {
            return true;
        }
    }

    return false;
}

int CalculateWinValue(int final, (int num, bool called)[,] board)
{
    int uncalledTotal = 0;

    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            if (board[i, j].called == false)
            {
                uncalledTotal += board[i, j].num;
            }
        }
    }

    int total = uncalledTotal * final;

    return total;
}

#endregion