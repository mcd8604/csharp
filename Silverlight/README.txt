Terry Van Hise
Mike DeMauro

Our code is laid out into four different projects:

StateMVC - A Silverlight library project, this contains the three interfaces (IView, IController, and IModel) 
	as well as the implementation of the IModel, PuzzleModel.  It is referenced in each of the other projects.
	In hindsight, it would have been smart to put the model implementation is a separate assembly.

ModelTestDriver - This is a simple C# Console Project that tests the model on the command line.  Behavior is 
	identical to that of the first silverlight app (see PuzzleGame1 below) except that interaction is through
	a console rather than silverlight.  Also allows user to specify puzzle size at the beginning.

PuzzleGame1 - Our first of two silverlight projects, this is identical to the top silverlight object displayed on
	the assignment page.  Users enter "row col" to simulate 'clicks' on those cells.  Returned are the update
	state messages from the model

PuzzleGame2 - This is the project that contains the completed game.  It seemed impractical to reuse the silverlight 
	code from PuzzleGame1, so this project just references StateMVC.

Blackout - This is the implementation of Blackout. The interfaces did not have to be changed for this.

Memory - This is the implementation of Memory. We had to add a delegate to IController to handle the visibility state of tiles.



To Execute:
	The ModelTestDriver compiles to a stand-alone exe.  The two PuzzleGames compile to xap files and generate
	stand-alone html files.