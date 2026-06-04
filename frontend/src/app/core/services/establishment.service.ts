import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Establishment, CreateEstablishment, UpdateEstablishment } from '../models/establishment.model';

@Injectable({ providedIn: 'root' })
export class EstablishmentService {
  private http = inject(HttpClient);
  private base = '/api/establishments';

  getAll() {
    return this.http.get<Establishment[]>(this.base);
  }

  create(dto: CreateEstablishment) {
    return this.http.post<Establishment>(this.base, dto);
  }

  update(id: string, dto: UpdateEstablishment) {
    return this.http.put<void>(`${this.base}/${id}`, dto);
  }

  delete(id: string) {
    return this.http.delete<void>(`${this.base}/${id}`);
  }
}
