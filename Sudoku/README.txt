Terry Van Hise
Mike DeMauro

To Execute:
The main function is found within the Program class (Program.cs).  Open the solution (.sln) in Visual Studio 2008 and Build/Run. 

Design:  
We implemented the observer pattern for this assignment.


Part A
======

1. Temperature Conversion Class

For the first bug, we were able to reproduce an error if you click or tab focus over to the fahrenheit (not celsius) textbox.  The issue was the double.Parse(string) command in post(), which was being called on a empty string and returning throwing a FormatException.  We solved this bug with the TryParse() alternate function, as seen in our revised post() method:

	private void post( TextBox from ) {
	    var model = from == celsius ? c2f : f2c;
	    var to = from == celsius ? fahrenheit : celsius;
	
	    double input;
	    if ( double.TryParse( from.Text, out input ) )
	        to.Text = model.Y( input ).ToString();
	    else
                to.Text = string.Empty;
	}

The second bug occured because we were not post()ing after a Deactivate event (form loses focus).  Although the values would be correct when we focused back and tabbed/clicked on the opposite field, we chose to handle the Deactivate event of Form to address this bug.  We added the following code to the Gui constructor (couldn't resist using C# 3.0 lambda expression syntax, pardon us please):

	Deactivate += ( sender, e ) =>	{
		TextBox from = ActiveControl as TextBox;
		if ( from != null ) { post( from ); }
	};

As a third note, the comment "BUG: does not happen on arrow key" for the box_KeyPress event can be explained because KeyPress events are not generated for non-character keys.  Instead, only KeyDown and KeyUp events are generated.  "The KeyPress event is not raised by noncharacter keys; however, the noncharacter keys do raise the KeyDown and KeyUp events." (MSDN: System.Windows.Forms.Control.KeyPress)


2.  Assignment 3 Bugs

In your solution to the previous week's assignment, there is a bug in that display on cloned windows does not update until focus enters them.  This is because when you calculate() you do not update each of the cloned windows.  We got around that bug by maintaining a static List<Window> of all the windows, and adding each window to the List in it's constructor.  Then, when one output textbox was updated we iterated through the List of windows to update each one (same with combobox selection).  You could implement our solution, or simple check if the value has been changed on the Active event of the Window.


Part B
======
We extended the IBoard interface to IClearableBoard and implementation to add the Clear method. We also used preprocessor directives to differentiate between original implementation and the current.

Part C
======
We created a SudokuForm which holds a BoardControl and also calculates the size of the board and each CellControl. Events (SetEvent and ClearEvent) are used to route user interactions to the model.

References:
Design Patterns (Gamma) - book
http://www.cs.rit.edu/~ats/papers/sudoku/sudoku.pdf
http://msdn.microsoft.com/en-us/library/system.windows.forms.control.keypress.aspx