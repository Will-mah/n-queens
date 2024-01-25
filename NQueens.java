import java.util.HashSet;
import java.util.ArrayList;
import java.util.List;

public class NQueens {

    private int n;
    private int[] board;
    private HashSet<Integer> cols = new HashSet<>();
    private HashSet<Integer> diagonals = new HashSet<>();
    private HashSet<Integer> antiDiagonals = new HashSet<>();
    private List<int[]> solutions = new ArrayList<>();

    public NQueens(int n) {
        this.n = n;
        this.board = new int[n];
        for (int i = 0; i < n; i++) {
            board[i] = -1;
        }
    }

    private boolean isSafe(int row, int col) {
        return !cols.contains(col) &&
               !diagonals.contains(row - col) &&
               !antiDiagonals.contains(row + col);
    }

    private boolean solve(int row) {
        if (row == n) {
            solutions.add(board.clone());
            return true;
        }
        for (int col = 0; col < n; col++) {
            if (isSafe(row, col)) {
                board[row] = col;
                cols.add(col);
                diagonals.add(row - col);
                antiDiagonals.add(row + col);
                if (solve(row + 1)) {
                    return true;
                }
                cols.remove(col);
                diagonals.remove(row - col);
                antiDiagonals.remove(row + col);
            }
        }
        return false;
    }

    public List<int[]> solveNQueens() {
        solve(0);
        return solutions;
    }

    public static void displayBoard(int[] board) {
        for (int row : board) {
            char[] line = new char[board.length];
            for (int i = 0; i < board.length; i++) {
                line[i] = '.';
            }
            line[row] = 'Q';
            System.out.println(new String(line));
        }
        System.out.println("-".repeat(board.length * 2));
    }

    public static void main(String[] args) {
        int n = 25;
        long startTime = System.currentTimeMillis();
        NQueens solver = new NQueens(n);
        List<int[]> solutions = solver.solveNQueens();
        long endTime = System.currentTimeMillis();
        if (!solutions.isEmpty()) {
            System.out.println("Found a solution for " + n + "-Queens problem:");
            displayBoard(solutions.get(0));
        } else {
            System.out.println("No solutions found for " + n + "-Queens problem.");
        }
        System.out.println("Execution time: " + (endTime - startTime) / 1000.0 + " seconds");
    }
}
