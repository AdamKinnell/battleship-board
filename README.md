
# battleship-board

This was developed as a solution to a technical assignment.

## Background

The classic game of 'Battleship' has the following rules:

* There are two opposing players.
* Each player has a `10x10` board.
* During the setup phase, each player places some number ( >= 1) of battleships on their board.
	* A battleship is represented as a `1-by-n` or `n-by-1` sequence of grid squares.
	* A battleship must be placed such that it fits entirely on the board.
	* A battleship cannot overlap with another battleship on the same board.
* After setup, both players alternate taking turns to choose a single coordinate on the opponent's board to "attack".
	* If the opponent contains a battleship at that coordinate, that part of the battleship is "damaged".
	* If all squares occupied by a battleship have been damaged, it is "sunk".
	* The attacker receives information about whether their attack was "hit" or "miss".
* Players continue to take turns until a player wins by sinking all of the opponent's battleships.

## Task

We are not required to implement the full game, just a single component: The state tracker for each player. 
In particular, we must implement the following functionality:
* Create a board.
* Add a battleship to the board.
* Take an “attack” at a given position, and report back whether the attack resulted in a hit or a miss.
* Return whether the player has lost the game yet (i.e. all battleships are sunk).
