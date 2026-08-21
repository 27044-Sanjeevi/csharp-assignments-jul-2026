using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment5ExpenseTrackerEnhanced.Models;

namespace Assignment5ExpenseTrackerEnhanced.View.Interfaces
{
    internal interface IDataVisualizationView
    {
        /// <summary>
        /// Displays a table of transactions after applying filters.
        /// </summary>
        /// <param name="transactions">The filtered list of transactions to display.</param>
        void DisplayFilteredTable(IReadOnlyList<Transaction> transactions);

        /// <summary>
        /// Renders visualized charts (breakdown/bar) of the transaction data.
        /// </summary>
        /// <param name="transactions">The collection of transaction data objects to visualize.</param>
        void DisplayVisualCharts(IReadOnlyList<Transaction> transactions);

        /// <summary>
        /// Renders a collection of transaction records as a formatted table grid.
        /// </summary>
        /// <param name="transactions">The collection of transaction data objects to display.</param>
        void DisplayAsTable(IReadOnlyList<Transaction> transactions);

        /// <summary>
        /// Displays the details of a given transaction.
        /// </summary>
        /// <param name="transaction">The transaction object containing the details to be displayed.</param>
        void DisplayTransactionDetails(Transaction transaction);

        /// <summary>
        /// Displays financial insights and summary statistics.
        /// </summary>
        /// <param name="totalIncome">Total income amount.</param>
        /// <param name="totalExpense">Total expense amount.</param>
        /// <param name="netBalance">Net balance (income - expense).</param>
        /// <param name="totalTransactions">Count of all transactions.</param>
        void DisplayInsights(decimal totalIncome, decimal totalExpense, decimal netBalance, int totalTransactions);
    }
}
