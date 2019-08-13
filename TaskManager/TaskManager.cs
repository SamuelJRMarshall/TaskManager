using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

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

		public void SaveTaskFile ( User user )
		{
			ResetTaskFile ( );
			using ( var writer = File.AppendText ( "task.txt" ) )
			{
				user.Tasks = user.Tasks.OrderBy ( x => x.StartTime ).ToList ( );
				for ( int i = 0; i < user.Tasks.Count; i++ )
				{
					writer.WriteLine ( $"{user.Tasks[ i ].Name},{i.ToString ( )},{user.Tasks[ i ].StartTime.ToString ( "o" )},{user.Tasks[ i ].Duration.ToString ( "o" )}" );
				}
			}
			user.Tasks = LoadTasks ( );
		}

		void ResetTaskFile ( )
		{
			if ( File.Exists ( "task.txt" ) )
			{
				File.WriteAllText ( "task.txt", String.Empty );
			}
		}

		public List<Task> LoadTasks ( )
		{
			List<Task> tasks = new List<Task> ( );
			if ( File.Exists ( "task.txt" ) )
			{
				using ( var reader = File.OpenText ( "task.txt" ) )
				{
					var line = reader.ReadLine ( );
					while ( line != null )
					{
						string[ ] splitLine = line.Split ( ',' );
						if ( splitLine.Length >= 1 )
						{
							tasks.Add ( new Task ( splitLine[ 0 ], int.Parse ( splitLine[ 1 ] ), DateTimeOffset.Parse ( splitLine[ 2 ] ), DateTimeOffset.Parse ( splitLine[ 3 ] ) ) );
						}
						line = reader.ReadLine ( );
					}
				}
			}
			return tasks;
		}

		void CreateTask ( User user )
		{
			Console.WriteLine ( "Create a Task. Enter the task's name" );
			string taskName = Console.ReadLine ( );
			Console.WriteLine ( "When does the task occur? YYYY/MM/DD HH:MM:SS" );
			string startTime = Console.ReadLine ( );
			Console.WriteLine ( "How long is the task? HH:MM" );
			string Duration = Console.ReadLine ( );

			user.Tasks.Add ( new Task ( taskName, user.Tasks.Count, DateTimeOffset.Parse ( startTime ), DateTimeOffset.Parse ( Duration ) ) );

			Console.WriteLine ( $"Task created: {user.Tasks[ user.Tasks.Count - 1 ].ToString ( )}" );

			SaveTaskFile ( user );

			TakeInput ( user );
		}

		void ViewTasks ( User user )
		{
			Console.WriteLine ( "Tasks:" );
			var lastDate = new DateTimeOffset ( );
			foreach ( Task task in user.Tasks )
			{
				if ( task.StartTime.Date != lastDate.Date )
				{
					Console.WriteLine ( $"[{task.StartTime.Date.ToShortDateString ( )}]" );
					lastDate = task.StartTime;
				}
				Console.WriteLine ( task.ToString ( ) );
			}

		}

		void ChangeTask ( User user )
		{
			ViewTasks ( user );
			Console.WriteLine ( "Enter the id of the task you would like to change" );
			string id = Console.ReadLine ( );
			id = id.Trim ( );
			int intId = int.Parse ( id );
			if ( intId < 0 || intId >= user.Tasks.Count )
			{
				Console.WriteLine ( "A task with this id doesnt exist" );
				ChangeTask ( user );
				return;
			}
			ChangePart ( user, intId );
		}

		void ChangePart ( User user, int id )
		{

			Console.WriteLine ( $"Task: {user.Tasks[ id ].ToString ( )} selected" );
			Console.WriteLine ( "Enter 'name' to change the task name, 'start' to change the start date, 'duration' to change the duration or 'exit'to return to the menu" );

			string input = Console.ReadLine ( );
			input = input.ToLower ( );
			input = input.Trim ( );
			switch ( input )
			{
				case "name":
					user.Tasks[ id ].Name = ChangeName ( );
					ChangePart ( user, id );
					break;

				case "start":
					user.Tasks[ id ].StartTime = ChangeStartDate ( );
					ChangePart ( user, id );
					break;

				case "duration":
					user.Tasks[ id ].Duration = ChangeDuration ( );
					ChangePart ( user, id );
					break;

				case "exit":
					TakeInput ( user );
					break;
				default:
					Console.WriteLine ( "Invalid Input, try again" );
					break;
			}
		}

		string ChangeName ( )
		{
			Console.WriteLine ( "What is the new task name?" );
			return Console.ReadLine ( );
		}

		DateTimeOffset ChangeStartDate ( )
		{
			Console.WriteLine ( "What is the new start date? YYYY/MM/DD HH:MM:SS" );
			return DateTimeOffset.Parse ( Console.ReadLine ( ) );

		}

		DateTimeOffset ChangeDuration ( )
		{
			Console.WriteLine ( "What is the new duration? HH:MM" );
			return DateTimeOffset.Parse ( Console.ReadLine ( ) );
		}


		public void TakeInput ( User user )
		{
			Console.WriteLine ( "Enter 'task' to create a new tasks or 'change' to change a task or 'view' to show tasks or 'exit' to quit" );
			string input = Console.ReadLine ( );
			input = input.ToLower ( );
			input = input.Trim ( );
			switch ( input )
			{
				case "view":
					ViewTasks ( user );
					TakeInput ( user );
					break;

				case "task":
					CreateTask ( user );
					break;

				case "change":
					ChangeTask ( user );
					break;

				case "exit":
					break;

				default:
					Console.WriteLine ( "Invalid Input, try again" );
					TakeInput ( user );
					break;
			}

		}

	}

	public enum DateComparison
	{
		Earlier = -1, Later = 1, Same = 0
	};
}
