import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EstablishmentService } from '../../core/services/establishment.service';
import { Establishment, CreateEstablishment } from '../../core/models/establishment.model';

@Component({
  selector: 'app-establishment-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './establishment-list.component.html',
  styleUrl: './establishment-list.component.scss'
})
export class EstablishmentListComponent implements OnInit {
  private service = inject(EstablishmentService);

  establishments = signal<Establishment[]>([]);
  showForm = signal(false);
  editing = signal<Establishment | null>(null);

  form: CreateEstablishment = { name: '' };

  ngOnInit() {
    this.load();
  }

  load() {
    this.service.getAll().subscribe(e => this.establishments.set(e));
  }

  openCreate() {
    this.form = { name: '' };
    this.editing.set(null);
    this.showForm.set(true);
  }

  openEdit(e: Establishment) {
    this.form = { name: e.name };
    this.editing.set(e);
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

  delete(e: Establishment) {
    if (!confirm(`Excluir "${e.name}"?`)) return;
    this.service.delete(e.id).subscribe(() => this.load());
  }

  closeForm() { this.showForm.set(false); }
}
