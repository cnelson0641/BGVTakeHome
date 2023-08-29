using Microsoft.Extensions.Configuration;
using RoomCharges.Data;
using System.Collections.Generic;
using System.Linq;
using RoomCharges.Models;

namespace RoomCharges.Services
{
    public class BusinessConfigurationService
    {
        private readonly IConfiguration configuration;

        public BusinessConfigurationService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<Data.BusinessConfiguration> GetBusinessConfigurations()
        {
            return GetBusinessConfigurationsLocalFile();
        }

        public Data.BusinessConfiguration GetBusinessConfiguration(int id)
        {
            var businesses = GetBusinessConfigurations();
            return businesses.Where(b => b.ID == id).FirstOrDefault();
        }

        private  List<BusinessConfiguration> GetBusinessConfigurationsLocalFile()
        {
            return configuration.GetSection("Businesses")
                .GetChildren()
                    .Select(x => new BusinessConfiguration
                    {
                        ID = x.GetValue<int>("ID"),
                        Name = x.GetValue<string>("Name"),
                        Site = x.GetValue<Site>("Site"),
                        Users = x.GetSection("Users").GetChildren().Select(u => new User
                        {
                            UserId = u.GetValue<int>("UserId"),
                            AuthPIN = u.GetValue<string>("AuthPIN"),
                            FirstName = u.GetValue<string>("FirstName"),
                            LastName = u.GetValue<string>("LastName")

                        }).ToList(),
                        TransactionCodes = x.GetSection("TransactionCodes").GetChildren().Select(c => new TransactionCode
                        {
                            ID = c.GetValue<int>("ID"),
                            Type = c.GetValue<TransactionType>("FirstName")
                        }).ToList()
                    }).ToList(); 
        }

    }
}
