using System;
using System.Collections.Generic;
using System.Linq;
using TripCalculator.Models.TripExpenses;

namespace TripCalculator.Services
{
    public interface ITripExpensesService
    {
        TripSettlementCollection GetSettlements(TripMemberCollection member);
    }

    public class TripExpensesService : ITripExpensesService
    {
        public TripSettlementCollection GetSettlements(TripMemberCollection member)
        {
            SetExpenseTotals(member);
            SetAmountOwed(member);

            var settlements = GetCalculatedSettlements(member);
            return new TripSettlementCollection { Settlements = settlements.ToList() };
        }

        private void SetExpenseTotals(TripMemberCollection member)
        {
            foreach (var tripMemberExpense in member.TripMembers)
                tripMemberExpense.TotalExpense = tripMemberExpense.Expenses.Sum();

            member.TotalExpense = member.TripMembers.Sum(m => m.TotalExpense);
        }

        private void SetAmountOwed(TripMemberCollection member)
        {
            var averageMemberExpense = GetAverageMemberExpense(member);

            foreach (var memberExpense in member.TripMembers)
            {
                memberExpense.AmountOwed = memberExpense.TotalExpense - averageMemberExpense;
                memberExpense.AmountBalance = memberExpense.AmountOwed;
            }
        }

        private decimal GetAverageMemberExpense(TripMemberCollection member)
        {
            return member.TotalExpense / member.TripMembers.Count();
        }

        private IEnumerable<TripSettlement> GetCalculatedSettlements(TripMemberCollection member)
        {
            var tripMemberExpenses = member.TripMembers.ToList();

            for(var i = 0; i < tripMemberExpenses.Count; i++)
            {
                var payer = tripMemberExpenses[i];

                if (payer.AmountBalance < 0.01M)
                {
                    for (var j = i + 1; j < tripMemberExpenses.Count; j++)
                    {
                        var payee = tripMemberExpenses[j];

                        if (payee.AmountBalance > 0.01M)
                        {
                            var transferrableAmount = GetMaximumTransferrableAmount(payer, payee);
                            payer.AmountTransferred += transferrableAmount;
                            payer.AmountBalance -= transferrableAmount;
                            payee.AmountTransferred -= transferrableAmount;
                            payee.AmountBalance += transferrableAmount;

                            yield return new TripSettlement
                            {
                                Payer = payer,
                                Payee = payee,
                                Amount = transferrableAmount
                            };
                        }
                    }
                }
            }
        }

        private decimal GetMaximumTransferrableAmount(TripMember payer, TripMember payee)
        {
            return Math.Min(Math.Abs(payer.AmountBalance), payee.AmountBalance);
        }
    }
}