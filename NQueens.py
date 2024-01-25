import time

class NQueens:
    def __init__(self, n):
        self.n = n
        self.board = [-1] * n
        self.cols = set()
        self.diagonals = set()
        self.antiDiagonals = set()
        self.solutions = []

    def is_safe(self, row, col):
        return col not in self.cols and \
               (row - col) not in self.diagonals and \
               (row + col) not in self.antiDiagonals

    def solve(self, row):
        if row == self.n:
            self.solutions.append(self.board.copy())
            return True
        for col in range(self.n):
            if self.is_safe(row, col):
                self.board[row] = col
                self.cols.add(col)
                self.diagonals.add(row - col)
                self.antiDiagonals.add(row + col)
                if self.solve(row + 1):
                    return True
                self.cols.remove(col)
                self.diagonals.remove(row - col)
                self.antiDiagonals.remove(row + col)
        return False

    def solve_n_queens(self):
        self.solve(0)
        return self.solutions

def display_board(board):
    for row in board:
        line = ['.' for _ in range(len(board))]
        line[row] = 'Q'
        print(''.join(line))
    print('-' * len(board) * 2)

if __name__ == "__main__":
    n = 25
    start_time = time.time()  # Start time
    solver = NQueens(n)
    solutions = solver.solve_n_queens()
    end_time = time.time()  # End time

    if solutions:
        print(f"Found a solution for {n}-Queens problem in python:")
        display_board(solutions[0])
    else:
        print(f"No solutions found for {n}-Queens problem.")

    print(f"Execution time: {end_time - start_time} seconds")
