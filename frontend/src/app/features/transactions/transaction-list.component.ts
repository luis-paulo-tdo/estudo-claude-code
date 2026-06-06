import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TransactionService } from '../../core/services/transaction.service';
import { CategoryService } from '../../core/services/category.service';
import { EstablishmentService } from '../../core/services/establishment.service';
import { Transaction, CreateTransaction, TransactionType } from '../../core/models/transaction.model';
import { Category } from '../../core/models/category.model';
import { Establishment } from '../../core/models/establishment.model';
import { BulkTransactionComponent } from './bulk-transaction.component';

@Component({
  selector: 'app-transaction-list',
  imports: [CommonModule, FormsModule, BulkTransactionComponent],
  templateUrl: './transaction-list.component.html',
  styleUrl: './transaction-list.component.scss'
})
export class TransactionListComponent implements OnInit {
  private service = inject(TransactionService);
  private categoryService = inject(CategoryService);
  private establishmentService = inject(EstablishmentService);

  transactions = signal<Transaction[]>([]);
  categories = signal<Category[]>([]);
  establishments = signal<Establishment[]>([]);
  showForm = signal(false);
  showBulkForm = signal(false);
  editing = signal<Transaction | null>(null);

  today = new Date();
  filterMonth = this.today.getMonth() + 1;
  filterYear = this.today.getFullYear();

  filters = {
    dateFrom: '',
    dateTo: '',
    description: '',
    establishment: '',
    categoryId: '',
    type: '' as TransactionType | ''
  };

  form: CreateTransaction = this.emptyForm();

  ngOnInit() {
    this.categoryService.getAll().subscribe(c => this.categories.set(c));
    this.establishmentService.getAll().subscribe(e => this.establishments.set(e));
    this.load();
  }

  load() {
    this.service.getAll({
      month: this.filterMonth,
      year: this.filterYear,
      dateFrom: this.filters.dateFrom || undefined,
      dateTo: this.filters.dateTo || undefined,
      description: this.filters.description || undefined,
      establishment: this.filters.establishment || undefined,
      categoryId: this.filters.categoryId || undefined,
      type: (this.filters.type || undefined) as any
    }).subscribe(t => this.transactions.set(t));
  }

  clearFilters() {
    this.filters = { dateFrom: '', dateTo: '', description: '', establishment: '', categoryId: '', type: '' };
    this.load();
  }

  get hasActiveFilters() {
    return Object.values(this.filters).some(v => v !== '');
  }

  openCreate() {
    this.form = this.emptyForm();
    this.editing.set(null);
    this.showForm.set(true);
  }

  openEdit(t: Transaction) {
    this.form = {
      date: t.date, amount: t.amount, description: t.description,
      type: t.type, categoryId: t.categoryId, isRecurring: t.isRecurring, recurrenceDay: t.recurrenceDay,
      establishmentId: t.establishmentId, unitPrice: t.unitPrice, quantity: t.quantity, unit: t.unit
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

  get totals() {
    const list = this.transactions();
    const income   = list.filter(t => t.type === 'Income').reduce((s, t) => s + (t.amount ?? 0), 0);
    const expenses = list.filter(t => t.type === 'Expense').reduce((s, t) => s + (t.amount ?? 0), 0);
    return { income, expenses, balance: income - expenses };
  }

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

  private emptyForm(): CreateTransaction {
    const now = new Date();
    const local = new Date(now.getTime() - now.getTimezoneOffset() * 60000).toISOString().slice(0, 16);
    return { date: local, amount: null, description: '', type: 'Expense', categoryId: '', isRecurring: false, recurrenceDay: null, establishmentId: null, unitPrice: null, quantity: null, unit: null };
  }
}
