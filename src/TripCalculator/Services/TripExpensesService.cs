using System;
using System.Collections.Generic;
using System.Linq;
using TripCalculator.Models.TripExpenses;

namespace TripCalculator.Services
{
    public interface ITripExpensesService
    {
        TripSettlementCollection GetSettlements(TripMemberExpensesCollection memberExpenses);
    }

    public class TripExpensesService : ITripExpensesService
    {
        public TripSettlementCollection GetSettlements(TripMemberExpensesCollection memberExpenses)
        {
            SetExpenseTotals(memberExpenses);
            SetAmountOwed(memberExpenses);

            var settlements = GetCalculatedSettlements(memberExpenses);
            return new TripSettlementCollection { Settlements = settlements.ToList() };
        }

        private void SetExpenseTotals(TripMemberExpensesCollection memberExpenses)
        {
            foreach (var tripMemberExpense in memberExpenses.TripMemberExpenses)
                tripMemberExpense.TotalExpense = tripMemberExpense.Expenses.Sum();

            memberExpenses.TotalExpense = memberExpenses.TripMemberExpenses.Sum(m => m.TotalExpense);
        }

        private void SetAmountOwed(TripMemberExpensesCollection memberExpenses)
        {
            var averageMemberExpense = GetAverageMemberExpense(memberExpenses);

            foreach (var memberExpense in memberExpenses.TripMemberExpenses)
                memberExpense.AmountOwed = memberExpense.TotalExpense - averageMemberExpense;
        }

        private decimal GetAverageMemberExpense(TripMemberExpensesCollection memberExpenses)
        {
            return memberExpenses.TotalExpense / memberExpenses.TripMemberExpenses.Count();
        }

        private IEnumerable<TripSettlement> GetCalculatedSettlements(TripMemberExpensesCollection memberExpenses)
        {
            var tripMemberExpenses = memberExpenses.TripMemberExpenses.ToList();

            for(var i = 0; i < tripMemberExpenses.Count; i++)
            {
                var memberExpense = tripMemberExpenses[i];

                if (memberExpense.AmountOwed < 0.01M)
                {
                    for (var j = i + 1; j < tripMemberExpenses.Count; j++)
                    {
                        var owedMember = tripMemberExpenses[j];

                        if (owedMember.AmountOwed > 0.01M)
                        {
                            var transferrableAmount = GetMaximumTransferrableAmount(memberExpense, owedMember);
                            memberExpense.AmountOwed += transferrableAmount;
                            owedMember.AmountOwed -= transferrableAmount;

                            yield return new TripSettlement
                            {
                                Payer = memberExpense.Member,
                                Payee = owedMember.Member,
                                Amount = transferrableAmount
                            };
                        }
                    }
                }
            }
        }

        private decimal GetMaximumTransferrableAmount(TripMemberExpenses memberExpense, TripMemberExpenses owedMember)
        {
            return Math.Min(Math.Abs(memberExpense.AmountOwed), owedMember.AmountOwed);
        }
    }
}