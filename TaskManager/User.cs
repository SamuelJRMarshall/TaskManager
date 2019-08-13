using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TaskManager
{
	public class User
	{
		public readonly string FirstName;
		public readonly DateTimeOffset BirthDate;
		public List<Task> Tasks;

		public User ( string fName, DateTimeOffset birthdate )
		{
			FirstName = fName;
			BirthDate = birthdate;
			Tasks = new List<Task> ( );
			Console.WriteLine ( FormatBirthdayResult ( GetDaysUntilNextBirthday ( ) ) );
		}

		public int GetDaysUntilNextBirthday ( )
		{
			var today = DateTime.UtcNow.Date;
			var birthday = new DateTime ( today.Year, BirthDate.Month, 1 );
			birthday = birthday.AddDays ( BirthDate.Day - 1 );

			if ( birthday < today )
			{
				birthday = new DateTime ( today.Year + 1, BirthDate.Month, 1 );
				birthday = birthday.AddDays ( BirthDate.Day - 1 );
			}

			return ( int ) ( birthday - today ).TotalDays;
		}

		string FormatBirthdayResult ( int i )
		{
			if ( i == 0 )
			{
				return "Happy Birthday!";
			}

			return $"There are {i.ToString ( )} days until your birthday!";
		}


	}
}
