import { Transaction } from './transaction.model';

export type BudgetStatus = 'Ok' | 'Warning' | 'Critical' | 'NoBudget';

export interface CategorySummary {
  categoryId: string;
  categoryName: string;
  categoryColor: string | null;
  totalSpent: number;
  plannedAmount: number | null;
  status: BudgetStatus;
}

export interface Dashboard {
  year: number;
  month: number;
  totalIncome: number;
  totalExpenses: number;
  balance: number;
  categorySummaries: CategorySummary[];
  recentTransactions: Transaction[];
}
