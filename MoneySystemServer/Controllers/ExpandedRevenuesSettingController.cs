using Logic;
using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Controllers;


namespace Api.Controllers
{
    public class ExpandedRevenuesSettingController : GlobalController
    {
        private IExpandedRevenuesService expandedRevenuesService;

        public ExpandedRevenuesSettingController(IExpandedRevenuesService expandedRevenuesService)
        {
            this.expandedRevenuesService = expandedRevenuesService;
        }

        [HttpGet]
        public GResult<List<PresenceSettingsDTO>> GetDaysRevenuesExpanded()
        {
            return Success(expandedRevenuesService.GetDaysRevenuesExpanded());
        }

        [HttpPost]
        public Result AddPresenceRevenue(PresenceSettingsDTO newRevenue)
        {
            var isDayExist = expandedRevenuesService.AddPresenceRevenue(newRevenue, UserId.Value);

            return Success();
        }

        [HttpPut]
        public Result UpdatePresenceRevenue(PresenceSettingsDTO newRevenue)
        {
            var isRevenueExist = expandedRevenuesService.updatePresenceRevenue(newRevenue, UserId.Value);
            if (!isRevenueExist)
            {
                return Fail(message: "ההכנסה קיימת כבר");
            }
            return Success();
        }

        [HttpDelete("{id}")]
        public Result DeletePresenceRevenue(int id)
        {
            var isRevenueExist = expandedRevenuesService.DeletePresenceRevenue(id, UserId.Value);
            if (!isRevenueExist)
            {
                return Fail(message: "ההכנסה לא נמצאה");
            }
            return Success();
        }

        [HttpGet]
        public GResult<List<AmountSettingsDTO>> GetProductsRevenuesExpanded()
        {
            return Success(expandedRevenuesService.GetProductsRevenuesExpanded());
        }

        [HttpPost]
        public Result AddAmountRevenue(AmountSettingsDTO newRevenue)
        {
            var isProductExist = expandedRevenuesService.AddAmountRevenue(newRevenue, UserId.Value);
            return Success();
        }

        [HttpPut]
        public Result UpdateAmountRevenue(AmountSettingsDTO newRevenue)
        {
            var isRevenueExist = expandedRevenuesService.updateAmountRevenue(newRevenue, UserId.Value);
            if (!isRevenueExist)
            {
                return Fail(message: "ההכנסה קיימת כבר");
            }
            return Success();
        }

        [HttpDelete("{id}")]
        public Result DeleteAmountRevenue(int id)
        {
            var isRevenueExist = expandedRevenuesService.DeleteAmountRevenue(id, UserId.Value);
            if (!isRevenueExist)
            {
                return Fail(message: "ההכנסה לא נמצאה");
            }
            return Success();
        }

    }
}
