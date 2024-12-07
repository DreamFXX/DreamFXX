namespace TCSA.WebAPI.FlightsData2.Models;
public class Flight
{ 
        public int Id { get; set; } 
        public int FlightNumber { get; set; }
        public string AirlineName { get; set; } = string.Empty;
        public string DepartureAirportCode { get; set; } = string.Empty;
        public string ArrivalAirportCode { get; set; } = string.Empty;
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int PassengerCapacity { get; set; }
}
