Terry Van Hise
Mike DeMauro

To Execute:
Open the solution in Visual Studio 2008 and Build/Run.  Ensure that the XcelGui project is set as the 
startup project.  Build solution and execute to bring up the WPF XcelWindow GUI.  (After built, only 
Xcel.exe and XcelGui.exe are needed--run XcelGui.exe).  Input argument list into the first textbox 
and change operations to be run on that list from the dropdown.  See assignment 
specifications for other features.

Design:
We chose to implement the command pattern for Assignment 1.  We further implemented a simple 
factory to generate command objects.  Assignment 3 expanded upon our Xcel project implementation as
follows:
-Adding a XcelProduct command object (subclass of XcelCommand) to add new functionality
-Changed implementation of XcelCommandFactory to use reflections rather than if-else chains
-Fixed bug in ToString() of XcelCommands that use double? variables

Our new project, XcelGui, is simply WPF with event handlers in the XcelWindow class.  This represents
the view and the controller (glue) of our project.


Note:
Visual studio builds this solution by first building the Xcel project, copying the assembly
(Xcel.exe) to the bin folder in XcelGui, and then referencing it from XcelGui.  This is the same
as we would do if we used csc.exe on the command line.

References:
standard deviation - http://en.wikipedia.org/wiki/Standard_deviation/
readonly type - http://en.csharp-online.net/const,_static_and_readonly
abstract type - http://www.brpreiss.com/books/opus6/html/page113.html
Linq - http://www.hookedonlinq.com/LinqToObject5MinuteOVerview.ashx