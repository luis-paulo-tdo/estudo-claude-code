import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Insight } from '../models/insight.model';

@Injectable({ providedIn: 'root' })
export class InsightService {
  private http = inject(HttpClient);

  get(month?: number, year?: number) {
    let params = new HttpParams();
    if (month) params = params.set('month', month);
    if (year) params = params.set('year', year);
    return this.http.get<Insight[]>('/api/insights', { params });
  }
}
