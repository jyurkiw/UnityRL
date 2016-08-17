# UnityRL
A roguelike in Unity3D

Project Goals:

* Learn A-star Pathfinding
* Learn basic decision tree-driven AI
* Complete a minimum viable product

 MVP Featureset (**Bold** = In-progress, *Italics* = Placeholder in-place):
 
Gameplay:
* Simple character creation
	* Class + Race
* Simple Character Advancement
	* Level + Loot
	* Each level should have enough enemy XP for the player to advance a level
* Basic enemy set (3 enemies)
	* Should spawn in random squares in every level
	* Should automatically move towards the player
		* Should only move when the player moves
	* Player movement should be interrupted when an enemy moves adjacent
* Basic combat
	* Player can attack monsters
	* Monsters can attack player
* Simple Victory condition
	* Defeat boss monster on dungeon level 10
* Functional GUI
	* HP/MP
	* Dungeon level display
* Navigation
	* (COMPLETE) Click-to-move navigation
	* (COMPLETE) WASD stepping
* Map Generation:
	* *Simple procedural dungeon generation that is not Drunkard walk*
* Game State Save
	* Record character status
	* Record dungeon level
	* Record current level state
	* Record previous level states
