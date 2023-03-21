using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveless.Data.Objects;

namespace Traveless.Data.Managers
{
	// Manages the reservation data.
	public class ReservationManager
	{
		// create a instance of reservation manager.
		private static ReservationManager _instance;
		public static ReservationManager Instance
		{
			get
			{
				// make a new instance of reservation manager if one does not exist otherwise return the existing instance.
				if (_instance == null)
				{
					_instance = new ReservationManager();
				}
				return _instance;
			}
		}

		private ReservationManager()
		{

		}

		private List<Reservation> _reservations;

		// get the list of reservations.
		public List<Reservation> Reservations
		{
			get
			{
				// If there are no reservations check if there are any in the saved file.
				if (_reservations == null)
				{
					_reservations = new List<Reservation>();

					try
					{
						var filePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Assets", "Reservations.bin");

						// If there is a saved file load the reservations from it.
						using (StreamReader sReader = new StreamReader(filePath))
						{
							// Iterate through the file until reaches the end.
							while (!sReader.EndOfStream)
							{
								// Read the reservation code.
								string reservation = sReader.ReadLine();
								string[] reservationParam = reservation.Split(',');
								string reservationNumber = reservationParam[0];
								bool isActive = bool.Parse(reservationParam[1]);

								// read the flights data.
								string flight = sReader.ReadLine();
								string[] flightParams = flight.Split(',');
								string flightNumber = flightParams[0];
								string airline = flightParams[1];
								string departureAirport = flightParams[2];
								string dstinationAirport = flightParams[3];
								DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), flightParams[4]);
								TimeSpan time = TimeSpan.Parse(flightParams[5]);
								int availableSeats = int.Parse(flightParams[6]);
								double price = double.Parse(flightParams[7]);

								// read the passenger data.
								string passenger = sReader.ReadLine();
								string[] passengerParams = passenger.Split(',');
								string passengerName = passengerParams[0];
								string passengerCitizenship = passengerParams[1];

								// create a flight object.
								Flight flightObj = new Flight(flightNumber, airline, departureAirport, dstinationAirport, day, time, availableSeats, price);

								// add the reservation to the list.
								Reservation reservationObj = new Reservation(reservationNumber, flightObj, passengerName, passengerCitizenship, isActive);
								_reservations.Add(reservationObj);
							}
						}
					}
					catch (Exception)
					{
					}
				}
				return _reservations;
			}
		}

		private List<Flight> _flights;

		// get or set the list of flights.
		public List<Flight> Flights
		{
			get
			{
				if (_flights == null)
				{
					// create a new list of flights.
					_flights = new List<Flight>();

					// check if there are any in the saved file.
					var filePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Assets", "flights.csv");

					// if there is a saved file load the flights from it.
					using (StreamReader sReader = new StreamReader(filePath))
					{
						// iterate through the file until reaches the end.
						while (!sReader.EndOfStream)
						{
							// read the flight data.
							string flight = sReader.ReadLine();
							string[] flightParams = flight.Split(',');
							string flightNumber = flightParams[0];
							string airline = flightParams[1];
							string departureAirport = flightParams[2];
							string dstinationAirport = flightParams[3];
							DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), flightParams[4]);
							TimeSpan time = TimeSpan.Parse(flightParams[5]);
							int availableSeats = int.Parse(flightParams[6]);
							double price = double.Parse(flightParams[7]);
								
							// create a flight object.
							Flight flightObj = new Flight(flightNumber, airline, departureAirport, dstinationAirport, day, time, availableSeats, price);

							// add the flight to the list.
							_flights.Add(flightObj);
						}
					}
				}
				return _flights;
			}
		}

		private List<string> _airports;

		// get the airports list.
		public List<string> Airports
		{
			get
			{
				if (_airports == null)
				{
					_airports = new List<string>();

					// check if there are any in the saved file.
					var filePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Assets", "airports.csv");

					using (StreamReader sReader = new StreamReader(filePath))
					{
						while (!sReader.EndOfStream)
						{
							_airports.Add(sReader.ReadLine().Split(",")[0]);
						}
					}
				}
				return _airports;
			}
		}

		// generate the reservation number.
		private string GenerateReservationNumber()
		{
			string letters = "";
			string numbers = "";

			Random rand = new Random();
			// extract the first two letters from the flight code
			for (int i = 0; i < 2; i++)
			{
					letters += (char)rand.Next('A', 'Z' + 1);
			}

			// generate a four-digit number
			for (int i = 0; i < 4; i++)
			{
				numbers += rand.Next(10).ToString();
			}

			// combine the letters and numbers to form the reservation number
			return letters + "-" + numbers;
		}

		// add a new reservation.
		public Reservation MakeReservation(Flight flight, string passengerName, string passengerCitizenship)
		{
			// check if the parameters are valid.
			if (!string.IsNullOrEmpty(passengerName) && !string.IsNullOrEmpty(passengerCitizenship) && flight != null)
			{
				// Make a new reservation and add it to the list.
				Reservation reservation = new Reservation(GenerateReservationNumber(), flight, passengerName, passengerCitizenship, true);
				Reservations.Add(reservation);
				SaveReservation();
				return reservation;
			}

			// If the parameters are not valid throw an exception.
			throw new Exception("Invalid reservation data.");
		}

		public void UpdateResrvation(Reservation reservation, string passengerName, string passengerCitizenship, bool isActive)
		{
			if (!string.IsNullOrEmpty(passengerName) && !string.IsNullOrEmpty(passengerCitizenship))
			{
				reservation.Update(passengerName, passengerCitizenship, isActive);
				SaveReservation();
				return;
			}
			throw new Exception("Invalid reservation data.");
		}

		// Find the flights on the basis of the searched parameters.
		public List<Flight> FindFlights(string from, string to, DayOfWeek day)
		{
			// check if the flight matches the search criteria.
			return Flights.Where(f => (string.IsNullOrEmpty(from) || f.DepartureAirport == from) &&
						   (string.IsNullOrEmpty(to) || f.ArrivalAirport == to) &&
						   (day == null || f.Day == day)).ToList();
		}

		// Find resrvations on the basis of the searched parameters.
		public List<Reservation> FindReservations(string code, string airline, string name)
		{
			// check if the flight matches the search criteria.
			return Reservations.Where(r => (string.IsNullOrEmpty(code) || r.ReservationNumber == code) &&
						   (string.IsNullOrEmpty(airline) || r.Flight.Airline == airline) &&
						   (string.IsNullOrEmpty(name) || r.PassengerName == name)).ToList();
		}

		// Save the reservation to the file.
		private void SaveReservation()
		{
			// check if there are any reservations files.
			var filePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Assets", "Reservations.bin");

			// Write the reservations to the file.
			using (StreamWriter sWriter = new StreamWriter(filePath))
			{
				foreach (var item in Reservations)
				{
					// Write the reservations data in 3 lines.
					sWriter.WriteLine(item.ReservationNumber + "," + item.Activated);
					sWriter.WriteLine(item.Flight.FlightCode + "," + item.Flight.Airline + "," + item.Flight.DepartureAirport + "," + item.Flight.ArrivalAirport + "," + item.Flight.Day + "," + item.Flight.FlightTime + "," + item.Flight.NumberOfSeatsAvailable + "," + item.Flight.Price);
					sWriter.WriteLine(item.PassengerName + "," + item.PassengerCitizenship);
				}
			}
		}
	}
}
