export type TransactionType = 'Income' | 'Expense';

export interface Transaction {
  id: string;
  date: string;
  amount: number | null;
  description: string;
  type: TransactionType;
  categoryId: string;
  categoryName: string;
  categoryColor: string | null;
  isRecurring: boolean;
  recurrenceDay: number | null;
  establishmentId: string | null;
  establishmentName: string | null;
  unitPrice: number | null;
  quantity: number | null;
  unit: string | null;
}

export interface CreateTransaction {
  date: string;
  amount: number | null;
  description: string;
  type: TransactionType;
  categoryId: string;
  isRecurring: boolean;
  recurrenceDay: number | null;
  establishmentId: string | null;
  unitPrice: number | null;
  quantity: number | null;
  unit: string | null;
}

export interface UpdateTransaction extends CreateTransaction {}

export interface BulkTransactionItem {
  categoryId: string;
  description: string;
  amount: number | null;
  quantity: number | null;
  unit: string | null;
  unitPrice: number | null;
}

export interface BulkCreateTransaction {
  date: string;
  type: TransactionType;
  establishmentId: string | null;
  items: BulkTransactionItem[];
}
