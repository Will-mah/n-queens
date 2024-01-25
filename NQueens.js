class NQueens {
    constructor(n) {
        this.n = n;
        this.board = new Array(n).fill(-1);
        this.cols = new Set();
        this.diagonals = new Set();
        this.antiDiagonals = new Set();
        this.solutions = [];
    }

    isSafe(row, col) {
        return !this.cols.has(col) &&
               !this.diagonals.has(row - col) &&
               !this.antiDiagonals.has(row + col);
    }

    solve(row) {
        if (row === this.n) {
            this.solutions.push([...this.board]);
            return true;
        }
        for (let col = 0; col < this.n; col++) {
            if (this.isSafe(row, col)) {
                this.board[row] = col;
                this.cols.add(col);
                this.diagonals.add(row - col);
                this.antiDiagonals.add(row + col);
                if (this.solve(row + 1)) {
                    return true;
                }
                this.cols.delete(col);
                this.diagonals.delete(row - col);
                this.antiDiagonals.delete(row + col);
            }
        }
        return false;
    }

    solveNQueens() {
        this.solve(0);
        return this.solutions;
    }
}

function displayBoard(board) {
    board.forEach(row => {
        let line = new Array(board.length).fill('.').join('');
        line = `${line.substring(0, row)}Q${line.substring(row + 1)}`;
        console.log(line);
    });
    console.log('-'.repeat(board.length * 2));
}

// Usage example
const n = 25;
const solver = new NQueens(n);
const solutions = solver.solveNQueens();
if (solutions.length > 0) {
    console.log(`Found a solution for ${n}-Queens problem javascript edition:`);
    displayBoard(solutions[0]);
} else {
    console.log(`No solutions found for ${n}-Queens problem.`);
}
