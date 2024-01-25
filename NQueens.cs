using System;
using System.Collections.Generic;

public class NQueens
{
    private int n;
    private int[] board;
    private HashSet<int> cols = new HashSet<int>();
    private HashSet<int> diagonals = new HashSet<int>();
    private HashSet<int> antiDiagonals = new HashSet<int>();
    private List<int[]> solutions = new List<int[]>();

    public NQueens(int n)
    {
        this.n = n;
        this.board = new int[n];
        for (int i = 0; i < n; i++)
        {
            board[i] = -1;
        }
    }

    private bool IsSafe(int row, int col)
    {
        return !cols.Contains(col) &&
               !diagonals.Contains(row - col) &&
               !antiDiagonals.Contains(row + col);
    }

    private bool Solve(int row)
    {
        if (row == n)
        {
            solutions.Add((int[])board.Clone());
            return true;
        }
        for (int col = 0; col < n; col++)
        {
            if (IsSafe(row, col))
            {
                board[row] = col;
                cols.Add(col);
                diagonals.Add(row - col);
                antiDiagonals.Add(row + col);
                if (Solve(row + 1))
                {
                    return true;
                }
                cols.Remove(col);
                diagonals.Remove(row - col);
                antiDiagonals.Remove(row + col);
            }
        }
        return false;
    }

    public List<int[]> SolveNQueens()
    {
        Solve(0);
        return solutions;
    }

    public static void DisplayBoard(int[] board)
    {
        foreach (int row in board)
        {
            char[] line = new char[board.Length];
            Array.Fill(line, '.');
            line[row] = 'Q';
            Console.WriteLine(new string(line));
        }
        Console.WriteLine(new string('-', board.Length * 2));
    }

    public static void Main(string[] args)
    {
        int n = 25;
        var startTime = DateTime.Now;
        NQueens solver = new NQueens(n);
        var solutions = solver.SolveNQueens();
        var endTime = DateTime.Now;

        if (solutions.Count > 0)
        {
            Console.WriteLine("Found a solution for " + n + "-Queens problem C# edition:");
            DisplayBoard(solutions[0]);
        }
        else
        {
            Console.WriteLine("No solutions found for " + n + "-Queens problem.");
        }
        Console.WriteLine("Execution time: " + (endTime - startTime).TotalSeconds + " seconds");
    }
}
