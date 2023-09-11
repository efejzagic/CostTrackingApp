using Finance.Application.DTOs.Expense;

namespace Apigateway.Combine.Interfaces
{
    public interface IConstructionSiteExpenseService
    {
        Task<List<ExpenseDTO>> GetConstructionSiteExpenses();

        Task<object> GetConstructionSiteExpense(int constructionSiteId);
    }
}
