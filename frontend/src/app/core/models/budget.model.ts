export interface Budget {
  id: string;
  categoryId: string;
  categoryName: string;
  categoryColor: string | null;
  year: number;
  month: number;
  plannedAmount: number;
}

export interface CreateBudget {
  categoryId: string;
  year: number;
  month: number;
  plannedAmount: number;
}

export interface UpdateBudget {
  plannedAmount: number;
}
