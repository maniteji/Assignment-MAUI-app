using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveless.Pages.Reservation;

namespace Traveless.Data.Objects
{
	// Represents a flight reservation.
	public class Reservation
	{
		// get or set the reservation number.
		public string ReservationNumber { get; set; }

		// get or set the reservation flight.
		public Flight Flight { get; set; }

		// get or set the reservation passenger name.
		public string PassengerName { get; set; }

		// get or set the reservation passenger citizenship.
		public string PassengerCitizenship { get; set; }

		// get or set the activation status.
		public bool Activated { get; set; }

		// constructor with parameters for all properties of the class.
		public Reservation(string reservationNumber, Flight flight, string passengerName, string passengerCitizenship, bool activated)
		{
			ReservationNumber = reservationNumber;
			Flight = flight;
			PassengerName = passengerName;
			PassengerCitizenship = passengerCitizenship;
			Activated = activated;
		}

		// Update the passenger name, citizenship and activation.
		public void Update(string passengerName, string passengerCitizenship, bool activation)
		{
			PassengerName = passengerName;
			PassengerCitizenship = passengerCitizenship;
			Activated = activation;
		}

		// Overide the ToString method to return properties of the class as a string.
		public override string ToString()
		{
			return ReservationNumber + ", " + Flight.FlightCode + ", " + Flight.Airline + ", " + Flight.Price + ", " + PassengerName + ", " + PassengerCitizenship + ", " + Activated;
		}
	}
}
