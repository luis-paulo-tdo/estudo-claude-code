import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
  },
  {
    path: 'transactions',
    loadComponent: () => import('./features/transactions/transaction-list.component').then(m => m.TransactionListComponent)
  },
  {
    path: 'budgets',
    loadComponent: () => import('./features/budgets/budget-list.component').then(m => m.BudgetListComponent)
  },
  {
    path: 'categories',
    loadComponent: () => import('./features/categories/category-list.component').then(m => m.CategoryListComponent)
  },
  {
    path: 'establishments',
    loadComponent: () => import('./features/establishments/establishment-list.component').then(m => m.EstablishmentListComponent)
  },
  { path: '**', redirectTo: 'dashboard' }
];
