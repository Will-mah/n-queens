#include <iostream>
#include <vector>
#include <set>
#include <chrono>

class NQueens {
    int n;
    std::vector<int> board;
    std::set<int> cols, diagonals, antiDiagonals;
    std::vector<std::vector<int>> solutions;

public:
    NQueens(int n) : n(n), board(n, -1) {}

    bool isSafe(int row, int col) {
        return cols.find(col) == cols.end() &&
               diagonals.find(row - col) == diagonals.end() &&
               antiDiagonals.find(row + col) == antiDiagonals.end();
    }

    bool solve(int row) {
        if (row == n) {
            solutions.push_back(board);
            return true;
        }
        for (int col = 0; col < n; col++) {
            if (isSafe(row, col)) {
                board[row] = col;
                cols.insert(col);
                diagonals.insert(row - col);
                antiDiagonals.insert(row + col);
                if (solve(row + 1)) {
                    return true;
                }
                cols.erase(col);
                diagonals.erase(row - col);
                antiDiagonals.erase(row + col);
            }
        }
        return false;
    }

    std::vector<std::vector<int>> solveNQueens() {
        solve(0);
        return solutions;
    }

    static void displayBoard(const std::vector<int>& board) {
        for (int row : board) {
            for (int i = 0; i < board.size(); i++) {
                std::cout << (i == row ? 'Q' : '.');
            }
            std::cout << std::endl;
        }
        std::cout << std::string(board.size() * 2, '-') << std::endl;
    }
};

int main() {
    int n = 25;
    auto startTime = std::chrono::high_resolution_clock::now();
    NQueens solver(n);
    auto solutions = solver.solveNQueens();
    auto endTime = std::chrono::high_resolution_clock::now();

    if (!solutions.empty()) {
        std::cout << "Found a solution for " << n << "-Queens problem C++ edition:" << std::endl;
        NQueens::displayBoard(solutions[0]);
    } else {
        std::cout << "No solutions found for " << n << "-Queens problem." << std::endl;
    }
    std::chrono::duration<double> elapsedTime = endTime - startTime;
    std::cout << "Execution time: " << elapsedTime.count() << " seconds" << std::endl;

    return 0;
}
