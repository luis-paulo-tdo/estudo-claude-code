export type TransactionType = 'Income' | 'Expense';

export interface Transaction {
  id: string;
  date: string;
  amount: number;
  description: string;
  type: TransactionType;
  categoryId: string;
  categoryName: string;
  categoryColor: string | null;
  isRecurring: boolean;
  recurrenceDay: number | null;
  unitPrice: number | null;
  quantity: number | null;
  unit: string | null;
}

export interface CreateTransaction {
  date: string;
  amount: number;
  description: string;
  type: TransactionType;
  categoryId: string;
  isRecurring: boolean;
  recurrenceDay: number | null;
  unitPrice: number | null;
  quantity: number | null;
  unit: string | null;
}

export interface UpdateTransaction extends CreateTransaction {}
