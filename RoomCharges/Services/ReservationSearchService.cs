using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using RoomCharges.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoomCharges.Services
{
    public class ReservationSearchService
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public ReservationSearchService(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DataBase");
        }
        public async Task<List<Reservation>> SearchReservations(SearchReservation searchReservation)
        {
            var query = "Select * From Reservations";
            using var connection = new SqliteConnection(connectionString);
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IEnumerable<Reservation> result = await connection.QueryAsync<Reservation>(query);

                if (!searchReservation.AllResorts)
                {
                    result = result.Where(r => r.Site == searchReservation.DefaultResort);
                }
                if (!string.IsNullOrEmpty(searchReservation.FirstName))
                {
                    result = result.Where(r => r.GuestFirstName == searchReservation.FirstName);
                }
                if (!string.IsNullOrEmpty(searchReservation.LastName))
                {
                    result = result.Where(r => r.GuestLastName == searchReservation.LastName);
                }
                if (!string.IsNullOrEmpty(searchReservation.RoomNumber))
                {
                    result = result.Where(r => r.RoomNumber == searchReservation.RoomNumber);
                }
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<Reservation>();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
