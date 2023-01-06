# Gridlocked Game in Unity

This is an effort to recreate a certain car based puzzle game that I grew up with. I was mostly focusing on using it to learn stuff like C# data structures, OOP including random stuff like creating hash representations and quick review on search algorithms. 

## Status as of 1/5/2023

### Completed

- Board generation with car placements
- Basic BFS solver
  - This is buggy tho. It was kind of working but has issues with moves that don't go anywhere. 
  - When adding utilities to remove redundant moves, I got some new issues so that's not totally right.
- Test suite for basic board operations

### Stuff to do

This isn't really a game yet, just a board solver POC. If you wanted to make it a real game, I would want to do the following.

- Pre-generate boards and save to text files along with difficulty for retrieval/loading
- Add any visual representation/interaction instead of just console test logging
- Play capability to drag cars with constraints/snapping to locations
  - Undo moves
- Hints for solution since we always generate these

Keeping this here in case I want to revisit and flesh that out. 

[GPL v3 license](http://opensource.org/licenses/GPL-3.0)
