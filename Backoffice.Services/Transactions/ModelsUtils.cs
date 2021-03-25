using System;
using Backoffice.Abstractions.Models;
using MyCrm.Abstractions.Transactions;
using MyCRM.AccountTransactions.Grpc.Models;
using AccountTransactionType = MyCrm.Abstractions.Transactions.AccountTransactionType;

namespace Backoffice.Services.Transactions
{
    public static class ModelsUtils
    {
        public static IBoTransactionModel ToDomain(this AccountTransactionGrpcModel src)
        {
            return new TransactionModel
            {
                TransactionDomain = src.Operation.GetTransactionDomain(),
                DateTime = src.DateTime,
                Id = src.Id,
                Instrument = src.Instrument,
                Operation = src.Operation.ToDomain(),
                Volume = src.Volume,
                Pl = src.Pl,
                Swaps = src.Swaps,
                Comment = src.Comment,
                OpenBalance = src.OpenBalance,
                OpenBonus = src.OpenBonus,
                CompanyPl = src.CompanyPl,
                BonusDelta = src.BonusDelta,
                OpenDate = src.OpenDate,
                Commission = src.Commission,
                AccountId = src.AccountId,
                CloseReason = src.CloseReason.ToDomain(),
                OpenPrice = src.OpenPrice,
                ClosePrice = src.ClosePrice,
            };
        }

        public static BoAccountTransactionType ToDomain(this AccountTransactionType operation)
        {
            return operation switch
            {
                AccountTransactionType.OpenBuy => BoAccountTransactionType.OpenBuy,
                AccountTransactionType.CloseBuy => BoAccountTransactionType.CloseBuy,
                AccountTransactionType.OpenSell => BoAccountTransactionType.OpenSell,
                AccountTransactionType.CloseSell => BoAccountTransactionType.CloseSell,
                AccountTransactionType.Deposit => BoAccountTransactionType.Deposit,
                AccountTransactionType.WithdrawRegistered => BoAccountTransactionType.WithdrawRegistered,
                AccountTransactionType.WithdrawMoneyReservation => BoAccountTransactionType.WithdrawMoneyReservation,
                AccountTransactionType.WithdrawCanceledWithoutReservation => BoAccountTransactionType
                    .WithdrawCanceledWithoutReservation,
                AccountTransactionType.WithdrawCanceledWithReservation => BoAccountTransactionType
                    .WithdrawCanceledWithReservation,
                AccountTransactionType.WithdrawProcessed => BoAccountTransactionType.WithdrawProcessed,
                AccountTransactionType.Transfer => BoAccountTransactionType.Transfer,
                AccountTransactionType.BonusDeposit => BoAccountTransactionType.BonusDeposit,
                AccountTransactionType.BonusWithdrawal => BoAccountTransactionType.BonusWithdrawal,
                AccountTransactionType.BonusCorrection => BoAccountTransactionType.BonusCorrection,
                AccountTransactionType.DepositVoid => BoAccountTransactionType.DepositVoid,
                AccountTransactionType.DepositChargeBack => BoAccountTransactionType.DepositChargeBack,
                _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
            };
        }

        public static BoClosedPositionReason ToDomain(this ClosedPositionReason operation)
        {
            return operation switch
            {
                ClosedPositionReason.ClientManual => BoClosedPositionReason.ClientManual,
                ClosedPositionReason.ManagerClose => BoClosedPositionReason.ManagerClose,
                ClosedPositionReason.Sl => BoClosedPositionReason.Sl,
                ClosedPositionReason.Tp => BoClosedPositionReason.Tp,
                ClosedPositionReason.So => BoClosedPositionReason.So,
                ClosedPositionReason.Split => BoClosedPositionReason.Split,
                ClosedPositionReason.Others => BoClosedPositionReason.Others,
                _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
            };
        }

        public static TransactionDomain GetTransactionDomain(this AccountTransactionType operation)
        {
            return operation switch
            {
                AccountTransactionType.OpenBuy => TransactionDomain.Trading,
                AccountTransactionType.CloseBuy => TransactionDomain.Trading,
                AccountTransactionType.OpenSell => TransactionDomain.Trading,
                AccountTransactionType.CloseSell => TransactionDomain.Trading,
                _ => TransactionDomain.Others
            };
        }
    }
}