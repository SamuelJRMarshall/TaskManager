using System;
using System.Collections.Generic;

namespace TaskManager
{
	public class User
	{
		public readonly string FirstName, SecondName, LoginName, Password;
		public readonly DateTimeOffset BirthDate;
		public List<Task> Tasks;
		public User ( string fName, string sName, string lName, string pass, DateTimeOffset date )
		{
			FirstName = fName;
			SecondName = sName;
			LoginName = lName;
			Password = pass;
			BirthDate = date;
			Tasks = new List<Task> ( );
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
	}
}
