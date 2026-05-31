import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BudgetService } from '../../core/services/budget.service';
import { CategoryService } from '../../core/services/category.service';
import { Budget, CreateBudget } from '../../core/models/budget.model';
import { Category } from '../../core/models/category.model';

@Component({
  selector: 'app-budget-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './budget-list.component.html',
  styleUrl: './budget-list.component.scss'
})
export class BudgetListComponent implements OnInit {
  private service = inject(BudgetService);
  private categoryService = inject(CategoryService);

  budgets = signal<Budget[]>([]);
  categories = signal<Category[]>([]);
  showForm = signal(false);
  editing = signal<Budget | null>(null);

  today = new Date();
  filterMonth = this.today.getMonth() + 1;
  filterYear = this.today.getFullYear();

  form: CreateBudget = this.emptyForm();

  ngOnInit() {
    this.categoryService.getAll().subscribe(c => this.categories.set(c));
    this.load();
  }

  load() {
    this.service.getAll(this.filterMonth, this.filterYear).subscribe(b => this.budgets.set(b));
  }

  openCreate() {
    this.form = this.emptyForm();
    this.editing.set(null);
    this.showForm.set(true);
  }

  openEdit(b: Budget) {
    this.form = { categoryId: b.categoryId, year: b.year, month: b.month, plannedAmount: b.plannedAmount };
    this.editing.set(b);
    this.showForm.set(true);
  }

  save() {
    const ed = this.editing();
    if (ed) {
      this.service.update(ed.id, { plannedAmount: this.form.plannedAmount }).subscribe(() => { this.closeForm(); this.load(); });
    } else {
      this.service.create(this.form).subscribe(() => { this.closeForm(); this.load(); });
    }
  }

  delete(id: string) {
    if (!confirm('Excluir este orçamento?')) return;
    this.service.delete(id).subscribe(() => this.load());
  }

  closeForm() { this.showForm.set(false); }

  get monthLabel() {
    const label = new Date(this.filterYear, this.filterMonth - 1).toLocaleDateString('pt-BR', { month: 'long', year: 'numeric' });
    return label.charAt(0).toUpperCase() + label.slice(1);
  }

  prevMonth() {
    if (this.filterMonth === 1) { this.filterMonth = 12; this.filterYear--; }
    else this.filterMonth--;
    this.load();
  }

  nextMonth() {
    if (this.filterMonth === 12) { this.filterMonth = 1; this.filterYear++; }
    else this.filterMonth++;
    this.load();
  }

  private emptyForm(): CreateBudget {
    return { categoryId: '', year: this.filterYear, month: this.filterMonth, plannedAmount: 0 };
  }
}
