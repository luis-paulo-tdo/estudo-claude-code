import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TransactionService } from '../../core/services/transaction.service';
import { CategoryService } from '../../core/services/category.service';
import { Transaction, CreateTransaction, TransactionType } from '../../core/models/transaction.model';
import { Category } from '../../core/models/category.model';

@Component({
  selector: 'app-transaction-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './transaction-list.component.html',
  styleUrl: './transaction-list.component.scss'
})
export class TransactionListComponent implements OnInit {
  private service = inject(TransactionService);
  private categoryService = inject(CategoryService);

  transactions = signal<Transaction[]>([]);
  categories = signal<Category[]>([]);
  showForm = signal(false);
  editing = signal<Transaction | null>(null);

  today = new Date();
  filterMonth = this.today.getMonth() + 1;
  filterYear = this.today.getFullYear();

  form: CreateTransaction = this.emptyForm();

  ngOnInit() {
    this.categoryService.getAll().subscribe(c => this.categories.set(c));
    this.load();
  }

  load() {
    this.service.getAll({ month: this.filterMonth, year: this.filterYear })
      .subscribe(t => this.transactions.set(t));
  }

  openCreate() {
    this.form = this.emptyForm();
    this.editing.set(null);
    this.showForm.set(true);
  }

  openEdit(t: Transaction) {
    this.form = {
      date: t.date, amount: t.amount, description: t.description,
      type: t.type, categoryId: t.categoryId, isRecurring: t.isRecurring, recurrenceDay: t.recurrenceDay
    };
    this.editing.set(t);
    this.showForm.set(true);
  }

  save() {
    const ed = this.editing();
    if (ed) {
      this.service.update(ed.id, this.form).subscribe(() => { this.closeForm(); this.load(); });
    } else {
      this.service.create(this.form).subscribe(() => { this.closeForm(); this.load(); });
    }
  }

  delete(id: string) {
    if (!confirm('Excluir este lançamento?')) return;
    this.service.delete(id).subscribe(() => this.load());
  }

  closeForm() { this.showForm.set(false); }

  get monthLabel() {
    return new Date(this.filterYear, this.filterMonth - 1).toLocaleDateString('pt-BR', { month: 'long', year: 'numeric' });
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

  private emptyForm(): CreateTransaction {
    const now = new Date();
    const local = new Date(now.getTime() - now.getTimezoneOffset() * 60000).toISOString().slice(0, 16);
    return { date: local, amount: 0, description: '', type: 'Expense', categoryId: '', isRecurring: false, recurrenceDay: null };
  }
}
