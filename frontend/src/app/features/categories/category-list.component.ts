import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CategoryService } from '../../core/services/category.service';
import { Category, CreateCategory } from '../../core/models/category.model';

@Component({
  selector: 'app-category-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.scss'
})
export class CategoryListComponent implements OnInit {
  private service = inject(CategoryService);

  categories = signal<Category[]>([]);
  showForm = signal(false);
  editing = signal<Category | null>(null);

  form: CreateCategory = { name: '', color: '#4a90d9' };

  readonly colorOptions = [
    '#4CAF50','#8BC34A','#009688','#2196F3','#4a90d9',
    '#FF9800','#FF5722','#E91E63','#9C27B0','#3F51B5',
    '#00BCD4','#FFC107','#9E9E9E','#795548','#607D8B'
  ];

  ngOnInit() {
    this.load();
  }

  load() {
    this.service.getAll().subscribe(c => this.categories.set(c));
  }

  openCreate() {
    this.form = { name: '', color: '#4a90d9' };
    this.editing.set(null);
    this.showForm.set(true);
  }

  openEdit(cat: Category) {
    this.form = { name: cat.name, color: cat.color };
    this.editing.set(cat);
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

  delete(cat: Category) {
    if (cat.isDefault) return;
    if (!confirm(`Excluir a categoria "${cat.name}"?`)) return;
    this.service.delete(cat.id).subscribe(() => this.load());
  }

  closeForm() { this.showForm.set(false); }
}
