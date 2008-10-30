Mike DeMauro
Terry Van Hise

Our solution is based on the posted solution for milestone one. 

The following projects coincide with the deliverables for milestone two:

service - This is the web service which utilizes a local database. I decided to not use Sessions, since the service is to serve one database instance to multiple clients.

remote - This is the client for the web service. It hides the functionality required to speak with the web service. Contains an event for service connection errors.

app2 - This is the WPF application. It is similar to the app project. Unlike its predecessor, app2 can toggle between remote and local databases. Also listens for and displays remote connection errors. 

interfaces - The toggling functionality is defined in ToggleController.  This class inherits from Axel.Database.Controller and requires two database instance parameters instead of one. The ToggleController utilizes the WorkQueue for toggling to maintain thread safety. An event is dispatched from the foreground delegate to update any listeners (views).

The downside to the subclass - I removed the readonly modifier from Axel.Database.Controller.db. ToggleController uses db to store the current database. In order to do this, it has to assign to db after creation. I'm not sure if this is the best idea, is there a better way to implement this?

To Execute:
	Run app2 from Visual Studio.