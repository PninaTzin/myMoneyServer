using Logic.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IExpandedRevenuesService
    {
        List<PresenceSettingsDTO> GetDaysRevenuesExpanded();
        bool AddPresenceRevenue(PresenceSettingsDTO newRevenue, int CurrentUserId);
        bool DeletePresenceRevenue(int id, int currentId);
        bool updatePresenceRevenue(PresenceSettingsDTO newRevenue, int CurrentUserId);
        List<AmountSettingsDTO> GetProductsRevenuesExpanded();
        bool AddAmountRevenue(AmountSettingsDTO newRevenue, int CurrentUserId);
        bool DeleteAmountRevenue(int id, int currentId);
        public bool updateAmountRevenue(AmountSettingsDTO newRevenue, int CurrentUserId);
    }

    public class ExpandedRevenuesSettingService : IExpandedRevenuesService
    {
        private IDBService dbService;

        public ExpandedRevenuesSettingService(IDBService dbService)
        {
            this.dbService = dbService;
        }

        public List<PresenceSettingsDTO> GetDaysRevenuesExpanded()
        {

            List<PresenceSettingsDTO> list = new List<PresenceSettingsDTO>();

            list = dbService.entities.PresenceSettings.Select(x => new PresenceSettingsDTO()
            {
                PresenceId = x.PresenceId,
                UserId = x.UserId,
                Day = x.Day,
                Hours = x.Hours,
            }).ToList();

            return list;
        }

        public bool AddPresenceRevenue(PresenceSettingsDTO newRevenue, int CurrentUserId)
        {
            PresenceSetting revenue = new PresenceSetting()
            {
                UserId = CurrentUserId,
                Day = newRevenue.Day,
                Hours = newRevenue.Hours,
            };

            dbService.entities.PresenceSettings.Add(revenue);
            dbService.Save();
            return true;
        }

        public bool DeletePresenceRevenue(int id, int currentId)
        {
            var revenue = dbService.entities.PresenceSettings.FirstOrDefault(x => x.PresenceId == id && x.UserId == currentId);
            if (revenue != null)
            {
                dbService.entities.PresenceSettings.Remove(revenue);
                dbService.Save();
                return true;
            }
            return false;
        }

        public bool updatePresenceRevenue(PresenceSettingsDTO newRevenue, int CurrentUserId)
        {
            if (dbService.entities.PresenceSettings.Any(x => x.UserId == CurrentUserId && x.PresenceId == newRevenue.PresenceId && x.Day == newRevenue.Day && x.Hours == newRevenue.Hours))
            {
                return false;
            }
            var revenue = dbService.entities.PresenceSettings.FirstOrDefault(x => x.PresenceId == newRevenue.PresenceId);
            if (revenue != null)
            {
                revenue.Day = newRevenue.Day;
                revenue.Hours = newRevenue.Hours;
                dbService.Save();
                return true;
            }
            return false;
        }

        public List<AmountSettingsDTO> GetProductsRevenuesExpanded()
        {

            List<AmountSettingsDTO> list = new List<AmountSettingsDTO>();

            list = dbService.entities.AmountSettings.Select(x => new AmountSettingsDTO()
            {
                ProductId = x.ProductId,
                UserId = x.UserId,
                Day = x.Day,
                Product = x.Product,
                ProductType = x.ProductType,
                ProductValue = x.ProductValue,
                ProductQuantity = x.ProductQuantity,
            }).ToList();

            return list;
        }

        public bool AddAmountRevenue(AmountSettingsDTO newRevenue, int CurrentUserId)
        {
            AmountSetting amount = new AmountSetting()
            {
                UserId = CurrentUserId,
                ProductId = newRevenue.ProductId,
                Day = newRevenue.Day,
                Product = newRevenue.Product,
                ProductQuantity = newRevenue.ProductQuantity,
                ProductType = newRevenue.ProductType,
                ProductValue = newRevenue.ProductValue,
            };
            dbService.entities.AmountSettings.Add(amount);
            dbService.Save();
            return true;
        }

        public bool DeleteAmountRevenue(int id, int currentId)
        {
            var revenue = dbService.entities.AmountSettings.FirstOrDefault(x => x.ProductId == id && x.UserId == currentId);
            if (revenue != null)
            {
                dbService.entities.AmountSettings.Remove(revenue);
                dbService.Save();
                return true;
            }
            return false;
        }

        public bool updateAmountRevenue(AmountSettingsDTO newRevenue, int CurrentUserId)
        {
            if (dbService.entities.AmountSettings.Any(x => x.UserId == CurrentUserId && x.ProductId == newRevenue.ProductId && x.Day == newRevenue.Day && x.Product == newRevenue.Product && x.ProductType == newRevenue.ProductType && x.ProductValue == newRevenue.ProductValue && x.ProductQuantity == newRevenue.ProductQuantity))
            {
                return false;
            }
            var revenue = dbService.entities.AmountSettings.FirstOrDefault(x => x.ProductId == newRevenue.ProductId);
            if (revenue != null)
            {
                revenue.ProductId = newRevenue.ProductId;
                revenue.UserId = CurrentUserId;
                revenue.Day = newRevenue.Day;
                revenue.Product = newRevenue.Product;
                revenue.ProductType = newRevenue.ProductType;
                revenue.ProductValue = newRevenue.ProductValue;
                revenue.ProductQuantity = newRevenue.ProductQuantity;

                dbService.Save();
                return true;
            }
            return false;
        }

    }

}
