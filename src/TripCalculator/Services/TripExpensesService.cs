using System;
using System.Collections.Generic;
using System.Linq;
using TripCalculator.Models.TripExpenses;

namespace TripCalculator.Services
{
    public interface ITripExpensesService
    {
        TripSettlementCollection GetSettlements(TripMemberCollection members);
    }

    public class TripExpensesService : ITripExpensesService
    {
        public TripSettlementCollection GetSettlements(TripMemberCollection members)
        {
            SetExpenseTotals(members);
            SetAmountOwed(members);

            var settlements = GetCalculatedSettlements(members);
            return new TripSettlementCollection { Settlements = settlements };
        }

        private void SetExpenseTotals(TripMemberCollection members)
        {
            foreach (var member in members.TripMembers)
                member.TotalExpense = member.Expenses.Sum();

            members.TotalExpense = members.TripMembers.Sum(m => m.TotalExpense);
            members.TotalExpenseAverage = members.TotalExpense/members.TripMembers.Count();
        }

        private void SetAmountOwed(TripMemberCollection members)
        {
            foreach (var member in members.TripMembers)
            {
                member.AmountOwed = member.TotalExpense - members.TotalExpenseAverage;
                member.AmountBalance = member.AmountOwed;
            }
        }

        private IEnumerable<TripSettlement> GetCalculatedSettlements(TripMemberCollection members)
        {
            var orderedMembers = GetOrderedMembers(members);

            foreach (var sender in GetSenders(orderedMembers))
            {
                foreach (var receiver in GetReceivers(orderedMembers))
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

        private List<TripMember> GetOrderedMembers(TripMemberCollection members)
        {
            return members.TripMembers.OrderBy(m => m.AmountOwed).ToList();
        }

        private IEnumerable<TripMember> GetSenders(List<TripMember> orderedMembers)
        {
            return orderedMembers.Where(s => s.AmountBalance < 0);
        }

        private IEnumerable<TripMember> GetReceivers(List<TripMember> orderedMembers)
        {
            return orderedMembers.Where(r => r.AmountBalance > 0);
        }

        private decimal GetMaximumTransferrableAmount(TripMember sender, TripMember receiver)
        {
            return Math.Min(Math.Abs(sender.AmountBalance), receiver.AmountBalance);
        }
    }
}