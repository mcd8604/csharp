Terry Van Hise
Mike DeMauro

Our code is laid out into four different projects:

StateMVC - A Silverlight library project, this contains the three interfaces (IView, IController, and IModel) 
	as well as an abstract implementation of the IModel, BoardModel.  BlackoutModel and MemoryModel
	both subclass BoardModel.  These classes are referred to in the other projects.


ModelTestDriver - This is a simple C# Console Project that tests all the models on the command line.  Behavior is 
	identical to that of the first silverlight app (PuzzleGame1) from the first Silverlight assignment except 
	that interaction is through a console rather than silverlight.  Also allows user to specify puzzle size at the beginning.

PuzzleGame1 - Our first of two silverlight projects, this is identical to the top silverlight object displayed on
	the assignment page.  Users enter "row col" to simulate 'clicks' on those cells.  Returned are the update
	state messages from the model

PuzzleGame2 - This is the project that contains the puzzle completed game.  It seemed impractical to reuse the silverlight 

Blackout - This is the implementation of Blackout from A Beautiful Mind. Users picks rows and columns and image is 
	divided. The interfaces did not have to be changed for this.

Memory - This is the implementation of the Memory game. Users picks rows and columns and image is divided. We had to add 
	a delegate to IController to handle the visibility state of tiles.



To Execute:
	The ModelTestDriver compiles to a stand-alone exe.  The two games compile to xap files and generate
	stand-alone html files.
