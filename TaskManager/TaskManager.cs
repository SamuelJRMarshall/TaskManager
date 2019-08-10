using System;
using System.Net;

namespace TaskManager
{
	public class TaskManager
	{
		public TaskManager ( )
		{
		}

		public void IsTaskOverlapping ( Task task )
		{
			//if(task.Id == Comparison id){ continue; }
		}

		public void TasksLater ( Task task )
		{

		}
	}

	public enum DateComparison
	{
		Earlier = -1, Later = 1, Same = 0
	};
}
