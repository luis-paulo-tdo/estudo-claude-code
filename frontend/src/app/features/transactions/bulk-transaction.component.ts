import { Component, OnInit, inject, signal, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TransactionService } from '../../core/services/transaction.service';
import { CategoryService } from '../../core/services/category.service';
import { EstablishmentService } from '../../core/services/establishment.service';
import { BulkCreateTransaction, BulkTransactionItem, TransactionType } from '../../core/models/transaction.model';
import { Category } from '../../core/models/category.model';
import { Establishment } from '../../core/models/establishment.model';

@Component({
  selector: 'app-bulk-transaction',
  imports: [CommonModule, FormsModule],
  templateUrl: './bulk-transaction.component.html',
  styleUrl: './bulk-transaction.component.scss'
})
export class BulkTransactionComponent implements OnInit {
  private service = inject(TransactionService);
  private categoryService = inject(CategoryService);
  private establishmentService = inject(EstablishmentService);

  saved = output<void>();
  cancelled = output<void>();

  categories = signal<Category[]>([]);
  establishments = signal<Establishment[]>([]);
  saving = signal(false);

  readonly UNITS = ['Unidade', 'Dúzia', 'Kg', 'g', 'L', 'mL', 'Pacote', 'Caixa', 'Porção'];

  header: { date: string; type: TransactionType; establishmentId: string | null } = {
    date: this.nowLocal(),
    type: 'Expense',
    establishmentId: null
  };

  items: BulkTransactionItem[] = [this.emptyItem()];

  ngOnInit() {
    this.categoryService.getAll().subscribe(c => this.categories.set(c));
    this.establishmentService.getAll().subscribe(e => this.establishments.set(e));
  }

  addItem() {
    this.items = [...this.items, this.emptyItem()];
  }

  removeItem(index: number) {
    if (this.items.length === 1) return;
    this.items = this.items.filter((_, i) => i !== index);
  }

  save() {
    const dto: BulkCreateTransaction = {
      date: this.header.date,
      type: this.header.type,
      establishmentId: this.header.establishmentId,
      items: this.items
    };
    this.saving.set(true);
    this.service.bulkCreate(dto).subscribe({
      next: () => this.saved.emit(),
      error: () => this.saving.set(false)
    });
  }

  get canSave() {
    return this.items.every(i => i.categoryId && i.description && i.amount);
  }

  get total() {
    return this.items.reduce((s, i) => s + (i.amount ?? 0), 0);
  }

  private emptyItem(): BulkTransactionItem {
    return { categoryId: '', description: '', amount: null, quantity: null, unit: null, unitPrice: null };
  }

  private nowLocal(): string {
    const now = new Date();
    return new Date(now.getTime() - now.getTimezoneOffset() * 60000).toISOString().slice(0, 16);
  }

  trackByIndex(index: number) { return index; }
}
