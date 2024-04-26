# Building and running the game 
* Navigate to the ```./Breakout/``` folder in your CLI of choice.
* ```dotnet run``` should build and run the program for you.

# Playing the game
* Move the player left and right with the arrow keys <kbd>←</kbd> & <kbd>→</kbd>.
* Exit the game by pressing <kbd>escape</kbd>
* To print the current level data as parsed to the console, press <kbd>space</kbd>

# Running unit tests
* Navigate to the ```./BreakoutTests/``` folder in your CLI of choice.
* ```dotnet test``` should run the tests.

An exception is thrown and logged in the console. This is intended behaviour.


# If a lot of errors are throwin in regards to finding DIKUArcade
* Check that the submodule contains anything
* If it does, delete all folders in ```./DIKUArcade/``` except for the one titled ```DIKUArcade```
* Move now solitary ```DIKUArcade``` folder out  into the ```DIKUGames``` folder, and remove the now empty folder (the one that originally housed the populated ```DIKUArcade``` folder 
