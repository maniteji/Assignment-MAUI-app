using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveless.Data.Objects
{
	// Represents a flight.
	public class Flight
	{
		// get or set the flight number.
		public string FlightCode { get; set; }

		// get or set the flight airline.
		public string Airline { get; set; }

		// get or set the flight departure airport.
		public string DepartureAirport { get; set; }

		// get or set the flight arrival airport.
		public string ArrivalAirport { get; set; }

		// get or set the flight departure day.
		public DayOfWeek Day { get; set; }

		// get or set the flight total time.
		public TimeSpan FlightTime { get; set; }

		// get or set the seats available.
		public int NumberOfSeatsAvailable { get; set; }

		// get or set the flight price.
		public double Price { get; set; }

		// constructor with parameters for all properties of the class.
		public Flight(string flightCode, string airline, string departureAirport, string arrivalAirport, DayOfWeek day, TimeSpan flightTime, int numberOfSeatsAvailable, double price)
		{
			FlightCode = flightCode;
			Airline = airline;
			DepartureAirport = departureAirport;
			ArrivalAirport = arrivalAirport;
			Day = day;
			FlightTime = flightTime;
			NumberOfSeatsAvailable = numberOfSeatsAvailable;
			Price = price;
		}

		// Overide the ToString method to return properties of the class as a string.
		public override string ToString()
		{
			return FlightCode + ", " + Airline + ", " + DepartureAirport + ", " + ArrivalAirport + ", " + Day + ", " + FlightTime + ", " + NumberOfSeatsAvailable + ", " + Price;
		}
	}
}
