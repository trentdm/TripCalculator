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
            var tripMemberExpenses = member.TripMembers.OrderBy(m => m.AmountOwed).ToList();

            for(var i = 0; i < tripMemberExpenses.Count; i++)
            {
                var sender = tripMemberExpenses[i];

                if (sender.AmountBalance < 0.01M)
                {
                    for (var j = i + 1; j < tripMemberExpenses.Count; j++)
                    {
                        var receiver = tripMemberExpenses[j];

                        if (receiver.AmountBalance > 0.01M)
                        {
                            var transferrableAmount = GetMaximumTransferrableAmount(sender, receiver);
                            sender.AmountTransferred += transferrableAmount;
                            sender.AmountBalance += transferrableAmount;
                            receiver.AmountTransferred -= transferrableAmount;
                            receiver.AmountBalance -= transferrableAmount;

                            yield return new TripSettlement
                            {
                                SenderName = sender.Name,
                                ReceiverName = receiver.Name,
                                Amount = transferrableAmount
                            };
                        }
                    }
                }
            }
        }

        private decimal GetMaximumTransferrableAmount(TripMember sender, TripMember receiver)
        {
            return Math.Min(Math.Abs(sender.AmountBalance), receiver.AmountBalance);
        }
    }
}