using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using RoomCharges.Data;
using RoomCharges.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace RoomCharges.Services
{
    public class FolioService
    {
        private readonly BusinessConfigurationService businessConfigurationService;
        private readonly IConfiguration configuration;
        private readonly string ConnectionString;

        public FolioService(BusinessConfigurationService businessConfigurationService, IConfiguration configuration)
        {
            this.businessConfigurationService = businessConfigurationService;
            this.configuration = configuration;
            this.ConnectionString = configuration.GetConnectionString("DataBase");
        }

        private async Task<Folio> GetFolio(int reservationId)
        {
            using var connection = new SqliteConnection(ConnectionString);
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                Folio result = await connection.QuerySingleAsync<Folio>($"SELECT * FROM Folio f WHERE f.ReservationId = {reservationId};");
                return result;
            }
            catch (Exception)
            {
                return new();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public async Task<List<FolioTransaction>> GetFolioTransactions(int reservationID)
        {
            // Get db connection info
            using var connection = new SqliteConnection(ConnectionString);
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

            // Get folioId from the reservationID
            int folioId = (await GetFolio(reservationID)).FolioId;

            // Get all transactions from db for the folioId
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                List<FolioTransaction> result = (await connection.QueryAsync<FolioTransaction>($"SELECT * FROM FolioTransactions ft WHERE ft.FolioId = {folioId};")).ToList();
                return result;
            }
            catch (Exception)
            {
                return new();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public async Task<ChargeResult> SubmitCharge(FolioCharge folioCharge)
        {
            // Verify folioCharge provided has a value
            if (!folioCharge.Amount.HasValue)
            {
                return new ChargeResult { Message = "Charge Amount Required", Success = false };
            }

            // Get business and verify employee is authorized
            BusinessConfiguration business = businessConfigurationService.GetBusinessConfiguration(folioCharge.BusinessID);
            bool allowed = business.ChargeAuthorized(folioCharge.EmployeeID);

            if (!allowed)
            {
                return new ChargeResult
                {
                    Success = false,
                    Message = "Employee not authorized"
                };
            }

            // Get employee object
            User user = business.ChargeUser(folioCharge.EmployeeID);

            // Get description and reference
            String description = business.Name;
            String reference = user.FirstName.Substring(0, 1) + user.LastName;

            // Write to db
            using var connection = new SqliteConnection(ConnectionString);
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                // Get the current date as a string in form mm/dd/yyyy
                String date = DateTime.Now.ToString("M/d/yyyy");

                // Get the folioID from the reservationID
                int folioID = (await GetFolio(folioCharge.ReservationID)).FolioId;

                // Insert into FolioTransactions
                int rowsAffected = await connection.ExecuteAsync($"INSERT INTO FolioTransactions (TransactionDate, Description, Amount, UserID, FolioID, Reference) VALUES ('{date}', '{description}', {folioCharge.Amount}, {folioCharge.EmployeeID}, {folioID}, '{reference}');");
                if (rowsAffected == 1)
                {
                    return new ChargeResult
                    {
                        Success = true,
                        Message = "Charge Successful"
                    };
                }
            }
            catch (Exception e)
            {
                // Print error to console
                Console.WriteLine(e.Message);

                return new();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            // Return error
            return new ChargeResult
            {
                Success = false,
                Message = "Unknown Error Occurred. Please contact Front Desk for assistance."
            };
        }
    }
}
