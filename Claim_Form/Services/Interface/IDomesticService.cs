using Claim_Form.Dtos;

namespace Claim_Form.Services.Interface
{
    /// <summary>
    /// Defines business operations related to domestic expenses.
    /// </summary>
    public interface IDomesticService
    {
        /// <summary>
        /// Creates multiple domestic expense entries for a given claim.
        /// </summary>
        /// <param name="claimId">
        /// The unique identifier of the claim to which the domestic expenses belong.
        /// </param>
        /// <param name="entries">
        /// A list of domestic expense details to be created.
        /// </param>
        /// <returns>
        /// A collection of created domestic expense DTOs.
        /// </returns>
        Task<IEnumerable<DomesticDto>> CreateDomesticExpense(
            Guid claimId,
            List<DomesticDto> entries);

        /// <summary>
        /// Retrieves a specific domestic expense by its identifier.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the domestic expense.
        /// </param>
        /// <returns>
        /// The domestic expense DTO if found; otherwise, null.
        /// </returns>
        Task<List<DomesticDto?>> GetDomesticAsync(Guid id);

        /// <summary>
        /// Updates an existing domestic expense.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the domestic expense to update.
        /// </param>
        /// <param name="dto">
        /// The updated domestic expense details.
        /// </param>
        /// <returns>
        /// The updated domestic expense DTO if the update was successful; otherwise, null.
        /// </returns>
        Task<bool> UpdateDomesticAsync(
            Guid id,
           List<ExpenseDto> dtoList);

        /// <summary>
        /// Deletes a domestic expense by its identifier.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the domestic expense to delete.
        /// </param>
        /// <returns>
        /// True if the deletion was successful; otherwise, false.
        /// </returns>
        Task<bool> DeleteDomesticAsync(Guid id);
    }
}