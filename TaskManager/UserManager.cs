using System;
using System.IO;
using System.Linq.Expressions;

namespace TaskManager
{
	public class UserManager
	{
		string _userName;
		DateTimeOffset _birthDate;

		public UserManager ( )
		{
			Console.WriteLine ( $"Welcome" );
			LoadData ( );
		}

		void LoadData ( )
		{
			try
			{
				using ( var reader = File.OpenText ( "data.txt" ) )
				{
					var line = reader.ReadLine ( );
					while ( line != null )
					{
						switch ( line )
						{
							case var i when i.Contains ( "Name:" ):
								PrintNameFromFileIfNotEmpty ( line );
								break;

							case var i when i.Contains ( "Birth:" ):
								PrintBirthFromFileIfNotEmpty ( line );
								break;
						}
						line = reader.ReadLine ( );
					}
				}
			}
			catch ( SystemException e )
			{
				CreateNewDataFile ( );
			}
		}

		void CreateNewDataFile ( )
		{
			if ( File.Exists ( "data.txt" ) )
			{
				File.WriteAllText ( "data.txt", String.Empty );
			}

			Console.WriteLine ( "You dont have an account, please enter your name:" );
			string userName = Console.ReadLine ( );
			SaveData ( $"Name: {userName.Trim ( )}" );

			Console.WriteLine ( "Please enter your birth date in YYYY/MM/DD" );
			DateTimeOffset Birthday = DateTimeOffset.Parse ( Console.ReadLine ( ) );
			SaveData ( $"Birth: {Birthday.Date.ToShortDateString ( )}" );

			PrintNameFromFileIfNotEmpty ( userName );
			PrintBirthFromFileIfNotEmpty ( Birthday.ToString ( ) );

		}

		void SaveData ( string input )
		{
			using ( var writer = File.AppendText ( "data.txt" ) )
			{
				writer.WriteLine ( input );
			}
		}

		void PrintNameFromFileIfNotEmpty ( string userName )
		{
			if ( string.IsNullOrWhiteSpace ( userName ) )
			{
				CreateNewDataFile ( );
				return;
			}

			_userName = userName.Replace ( "Name: ", "" );
			Console.WriteLine ( $"Hi { _userName}!" );

		}

		void PrintBirthFromFileIfNotEmpty ( string birth )
		{
			if ( string.IsNullOrWhiteSpace ( birth ) )
			{
				CreateNewDataFile ( );
				return;
			}

			_birthDate = DateTimeOffset.Parse ( birth.Replace ( "Birth: ", "" ) );
			Console.WriteLine ( $"Your birthday is on { _birthDate.Date.ToShortDateString ( )}" );
		}

		public User CreateUser ( )
		{
			return new User ( _userName, _birthDate );
		}

	}
}
