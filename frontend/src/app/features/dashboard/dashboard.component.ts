import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { DashboardService } from '../../core/services/dashboard.service';
import { InsightService } from '../../core/services/insight.service';
import { Dashboard } from '../../core/models/dashboard.model';
import { Insight } from '../../core/models/insight.model';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  private dashboardService = inject(DashboardService);
  private insightService = inject(InsightService);

  dashboard = signal<Dashboard | null>(null);
  insights = signal<Insight[]>([]);
  loading = signal(true);

  today = new Date();
  month = this.today.getMonth() + 1;
  year = this.today.getFullYear();

  ngOnInit() {
    this.load();
  }

  load() {
    this.loading.set(true);
    this.dashboardService.get(this.month, this.year).subscribe(d => {
      this.dashboard.set(d);
      this.loading.set(false);
    });
    this.insightService.get(this.month, this.year).subscribe(i => this.insights.set(i));
  }

  prevMonth() {
    if (this.month === 1) { this.month = 12; this.year--; }
    else this.month--;
    this.load();
  }

  nextMonth() {
    if (this.month === 12) { this.month = 1; this.year++; }
    else this.month++;
    this.load();
  }

  get monthLabel() {
    const label = new Date(this.year, this.month - 1).toLocaleDateString('pt-BR', { month: 'long', year: 'numeric' });
    return label.charAt(0).toUpperCase() + label.slice(1);
  }

  budgetPercent(spent: number, planned: number | null): number {
    if (!planned || planned === 0) return 0;
    return Math.min(100, Math.round((spent / planned) * 100));
  }

  severityClass(severity: string) {
    return { 'Info': 'info', 'Warning': 'warning', 'Critical': 'critical' }[severity] ?? 'info';
  }
}
