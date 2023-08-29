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
            throw new NotImplementedException("Prospect is to implement");
        }

        public async Task<ChargeResult> SubmitCharge(FolioCharge folioCharge)
        {
            if (!folioCharge.Amount.HasValue)
            {
                return new ChargeResult { Message = "Charge Amount Required", Success = false };
            }

            BusinessConfiguration business = businessConfigurationService.GetBusinessConfiguration(folioCharge.BusinessID);

            bool allowed = business.ChargeAuthorized(folioCharge.EmployeeID);
            User user = business.ChargeUser(folioCharge.EmployeeID);

            if (!allowed)
            {
                return new ChargeResult
                {
                    Success = false,
                    Message = "Employee not authorized"
                };
            }

            //Business Name is Description
            //Employee Name is Reference

            throw new NotImplementedException("Prospect is to implement");

            return new ChargeResult
            {
                Success = false,
                Message = "Unknown Error Occurred. Please contact Front Desk for assistance."
            };
        }
    }
}
