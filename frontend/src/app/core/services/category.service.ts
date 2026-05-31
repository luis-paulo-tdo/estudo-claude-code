import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category, CreateCategory, UpdateCategory } from '../models/category.model';

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private http = inject(HttpClient);
  private base = '/api/categories';

  getAll() {
    return this.http.get<Category[]>(this.base);
  }

  create(dto: CreateCategory) {
    return this.http.post<Category>(this.base, dto);
  }

  update(id: string, dto: UpdateCategory) {
    return this.http.put<void>(`${this.base}/${id}`, dto);
  }

  delete(id: string) {
    return this.http.delete<void>(`${this.base}/${id}`);
  }
}
