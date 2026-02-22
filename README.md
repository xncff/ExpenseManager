## Expense Manager
***
Tracks wallets (accounts) and their transactions.

Implemented as three-layer architecture with an anemic domain model.

**Projects**:
- ExpenseManager.BusinessLayer – business services.
- ExpenseManager.DataAccessLayer – repositories and mock storage.
- ExpenseManager.PresentationLayer – TODO.

Objects in storage are represented by domain models `Wallet` and `Transaction`, no need for DTOs for now.