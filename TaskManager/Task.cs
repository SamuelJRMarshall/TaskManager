using System;
namespace TaskManager
{
	public class Task
	{
		public readonly string Name;
		public int Id;
		//Duration, StartTime;

		public Task ( string taskName )
		{
			Name = taskName;
		}
	}
}
