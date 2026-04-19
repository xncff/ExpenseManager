## Expense Manager

Tracks wallets (accounts) and their transactions.

Implemented as a strict three-layer architecture:

**Projects**:
- ExpenseManager.PresentationLayer – MAUI interface and app logic. Interacts only with Services.
- ExpenseManager.BusinessLayer – services and DTOs. Interacts with Storage and transforms models.
- ExpenseManager.DataAccessLayer – storage, repositories, and entity models.