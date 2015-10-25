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

        private decimal GetAverageMemberExpense(TripMemberCollection members)
        {
            return members.TotalExpense / members.TripMembers.Count();
        }

        private IEnumerable<TripSettlement> GetCalculatedSettlements(TripMemberCollection members)
        {
            const decimal shareTolerance = 0.1M;
            var orderedMembers = GetOrderedMembers(members);

            foreach (var sender in GetSenders(orderedMembers, shareTolerance))
            {
                foreach (var receiver in GetReceivers(orderedMembers, shareTolerance))
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

        private IEnumerable<TripMember> GetSenders(List<TripMember> orderedMembers, decimal shareTolerance)
        {
            return orderedMembers.Where(s => s.AmountBalance < shareTolerance);
        }

        private IEnumerable<TripMember> GetReceivers(List<TripMember> orderedMembers, decimal shareTolerance)
        {
            return orderedMembers.Where(r => r.AmountBalance > shareTolerance);
        }

        private decimal GetMaximumTransferrableAmount(TripMember sender, TripMember receiver)
        {
            return Math.Min(Math.Abs(sender.AmountBalance), receiver.AmountBalance);
        }
    }
}