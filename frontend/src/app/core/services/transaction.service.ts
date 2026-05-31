import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Transaction, CreateTransaction, UpdateTransaction, TransactionType } from '../models/transaction.model';

@Injectable({ providedIn: 'root' })
export class TransactionService {
  private http = inject(HttpClient);
  private base = '/api/transactions';

  getAll(filters?: { month?: number; year?: number; categoryId?: string; type?: TransactionType }) {
    let params = new HttpParams();
    if (filters?.month) params = params.set('month', filters.month);
    if (filters?.year) params = params.set('year', filters.year);
    if (filters?.categoryId) params = params.set('categoryId', filters.categoryId);
    if (filters?.type) params = params.set('type', filters.type);
    return this.http.get<Transaction[]>(this.base, { params });
  }

  create(dto: CreateTransaction) {
    return this.http.post<Transaction>(this.base, dto);
  }

  update(id: string, dto: UpdateTransaction) {
    return this.http.put<void>(`${this.base}/${id}`, dto);
  }

  delete(id: string) {
    return this.http.delete<void>(`${this.base}/${id}`);
  }
}
