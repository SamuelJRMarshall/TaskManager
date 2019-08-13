using System;

namespace TaskManager
{
	class MainClass
	{
		public static void Main ( string[ ] args )
		{
			var userManager = new UserManager ( );
			var taskManager = new TaskManager ( );
			var user = userManager.CreateUser ( );
			user.Tasks = taskManager.LoadTasks ( );
			Console.WriteLine ( $"You have {user.Tasks.Count} tasks" );

			taskManager.TakeInput ( user );
		}
	}
}
