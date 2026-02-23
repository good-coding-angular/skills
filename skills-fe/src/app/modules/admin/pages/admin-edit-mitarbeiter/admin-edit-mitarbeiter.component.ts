import {Component, inject, OnInit, signal} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NgIf} from '@angular/common';
import {ApiModule, MitarbeiterDto, MitarbeiterService} from '../../../backend';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatButtonModule} from '@angular/material/button';

@Component({
  standalone: true,
  selector: 'admin-edit-mitarbeiter',
  templateUrl: './admin-edit-mitarbeiter.component.html',
  providers: [
    MitarbeiterService,
  ],
  imports: [
    ApiModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSelectModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatButtonModule
  ]
})
export class AdminEditMitarbeiterComponent implements OnInit {
  public mitarbeiterService = inject(MitarbeiterService);
  public allMitarbeiterList = signal<MitarbeiterDto[]>([]);
  public editingMitarbeiterId: number | null = null;

  public formChosenMitarbeiter = new FormGroup({
    mitarbeiter: new FormControl(['1'])
  });

  public formCurrentlyEditedMitarbeiter = new FormGroup({
    mitarbeiterId: new FormControl<number>(0, { nonNullable: true }),
    name: new FormControl<string>('', { nonNullable: true }),
    available: new FormControl<number>(0, { nonNullable: true }),
  });

  public ngOnInit(): void {
    this.mitarbeiterService
      .apiMitarbeiterGetAllMitarbeiterGet()
      .subscribe(
        mitarbeiter => this.allMitarbeiterList.set(mitarbeiter)
      );
  }

  public onChangeExistingMitarbeiter(id: number): void {
    if (!id) {
      this.editingMitarbeiterId = null;
      this.formCurrentlyEditedMitarbeiter.reset({
        mitarbeiterId: undefined,
        name: '',
        available: 0,
      });
      return;
    }

    const selected = this.allMitarbeiterList()?.find(m => m.mitarbeiterId === id);

    if (selected) {
      this.editingMitarbeiterId = selected.mitarbeiterId ?? null;
      this.formCurrentlyEditedMitarbeiter.patchValue({
        mitarbeiterId: selected.mitarbeiterId ?? undefined,
        name: selected.name ?? '',
        available: selected.available ?? 0,
      });
    }
  }

  public onSubmit(): void {
    if (this.formCurrentlyEditedMitarbeiter.invalid) {
      return;
    }

    const dto: MitarbeiterDto = this.formCurrentlyEditedMitarbeiter.getRawValue();

    const request$ = this.editingMitarbeiterId
      ? this.mitarbeiterService.apiMitarbeiterUpdateMitarbeiterPost(dto)
      : this.mitarbeiterService.apiMitarbeiterCreateMitarbeiterPost(dto);

    request$.subscribe(result => {
      this.mitarbeiterService.apiMitarbeiterGetAllMitarbeiterGet().subscribe(all => {
        this.allMitarbeiterList.set(all ?? []);

        if (!this.editingMitarbeiterId && result?.mitarbeiterId) {
          this.editingMitarbeiterId = result.mitarbeiterId;
        }
      });
    });
  }
}
