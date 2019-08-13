using System;
namespace TaskManager
{
	public class Task
	{
		public string Name;
		public int Id;
		public DateTimeOffset Duration, StartTime;

		public Task ( string taskName, int id, DateTimeOffset startTime, DateTimeOffset duration )
		{
			Name = taskName;
			Id = id;
			StartTime = startTime;
			Duration = duration;

		}

		public override string ToString ( )
		{

			return $"{Id.ToString ( )}, {Name}, {StartTime.ToString ( "yyyy/MM/dd H:mm" )}, {Duration.Hour.ToString ( )}:{Duration.Minute.ToString ( )}";
		}
	}
}
