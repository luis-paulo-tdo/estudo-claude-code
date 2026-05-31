import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Budget, CreateBudget, UpdateBudget } from '../models/budget.model';

@Injectable({ providedIn: 'root' })
export class BudgetService {
  private http = inject(HttpClient);
  private base = '/api/budgets';

  getAll(month?: number, year?: number) {
    let params = new HttpParams();
    if (month) params = params.set('month', month);
    if (year) params = params.set('year', year);
    return this.http.get<Budget[]>(this.base, { params });
  }

  create(dto: CreateBudget) {
    return this.http.post<Budget>(this.base, dto);
  }

  update(id: string, dto: UpdateBudget) {
    return this.http.put<void>(`${this.base}/${id}`, dto);
  }

  delete(id: string) {
    return this.http.delete<void>(`${this.base}/${id}`);
  }
}
