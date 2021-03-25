using System;
using System.Linq;
using Backoffice.Abstractions.Models;
using MyCrm.Accounts.Grpc.Contracts;
using MyCrm.Accounts.Grpc.Models;
using MyCRM.Logs.GrpcContracts.Models;

namespace Backoffice.Services.Accounts
{
    public static class ModelsUtils
    {
        public static IAccountModel ToDomain(this CrmAccountGrpcModel src)
        {
            return new AccountModel
            {
                Registered = src.Registered,
                TraderId = src.TraderId,
                AccountId = src.AccountId,
                Currency = src.Currency,
                Balance = src.Balance,
                Bonus = src.Bonus,
                Equity = src.Equity,
                ReservedForWithdrawals = src.ReservedForWithdrawals,
                FreeToWithdraw = src.FreeToWithdraw,
                IsLive = src.IsLive,
                Login = src.Login,
                Leverage = src.Leverage,
                Group = src.Group,
                AccountType = src.AccountType,
                Margin = src.Margin,
                MarginFree = src.MarginFree,
                FirstDeposit = src.FirstDeposit,
                NetDeposit = src.NetDeposit,
                Profit = src.Profit,
            };
        }

        public static IAuthLogModel ToDomain(this AuthenticationGrpcEvent src)
        {
            return new AuthLogModel
            {
                Authenticated = src.Authenticated,
                Ip = src.Ip,
                UserAgent = src.UserAgent,
                Language = src.Language,
                CountryByIp = src.CountryByIp
            };
        }

        public static IDealingInfoActivePositionModel ToDomain(this ActivePositionGrpcModel src)
        {
            return new DealingInfoActivePositionModel
            {
                Id = src.Id,
                Type = src.Type,
                Symbol = src.Symbol,
                InvestAmount = src.InvestAmount,
                Multiplier = src.Multiplier,
                Pnl = src.Pnl,
                ToStopOutPercent = src.ToStopOutPercent,
                Swaps = src.Swaps,
                Tp = src.Tp,
                Sl = src.Sl,
                OpenDate = src.OpenDate,
                SoLvl = src.SoLvl,
                ReservedFundForToppingUp = src.ReservedFundForToppingUp
            };
        }

        public static IClosedPositionModel ToDomain(this ClosedPositionGrpcModel src)
        {
            return new ClosedPositionModel
            {
                Id = src.Id,
                Type = src.Type,
                Symbol = src.Symbol,
                InvestAmount = src.InvestAmount,
                Multiplier = src.Multiplier,
                Pnl = src.Pnl,
                ToStopOutPercent = src.ToStopOutPercent,
                Swaps = src.Swaps,
                Tp = src.Tp,
                Sl = src.Sl,
                OpenDate = src.OpenDate,
                SoLvl = src.SoLvl,
                ReservedFundForToppingUp = src.ReservedFundForToppingUp,
                CloseReason = src.CloseReason,
                CloseDate = src.CloseDate
            };
        }

        public static IDealingInfoModel ToDomain(this DealingInfoResponse src)
        {
            return new DealingInfoModel
            {
                LastPositionPlacedDate = src.LastPositionPlacedDate,
                TotalInvestment = src.TotalInvestment,
                RemainingBalance = src.RemainingBalance,
                CurrentPnlWithSwaps = src.CurrentPnlWithSwaps,
                SwapsSum = src.SwapsSum,
                CommissionSum = src.CommissionSum,
                DepositsSum = src.DepositsSum,
                WithdrawalsSum = src.WithdrawalsSum,
                TotalPl = src.TotalPl,
                ActivePositionsCount = src.ActivePositionsCount,
                NetDeposit = src.NetDeposit,
                TradedVolume = src.TradedVolume,
                TotalSwaps = src.TotalSwaps,
                ActivePosition = src.ActivePosition?.Select(itm => itm.ToDomain()) ??
                                 Array.Empty<IDealingInfoActivePositionModel>(),
                ClosedPosition = src.ClosedPosition?.Select(itm => itm.ToDomain()) ??
                                 Array.Empty<IClosedPositionModel>(),
            };
        }
    }
}