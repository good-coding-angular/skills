import {Component, inject} from '@angular/core';
import {ApiModule, MitarbeiterService, SkillgruppenService} from '../../../backend';
import {toSignal} from '@angular/core/rxjs-interop';
import {NgClass} from '@angular/common';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  standalone: true,
  providers: [
    SkillgruppenService,
    MitarbeiterService,
  ],
  imports: [
    ApiModule,
    NgClass,
    MatFormFieldModule, MatSelectModule, FormsModule, ReactiveFormsModule
  ]
})
export class EmployeeComponent {
  public skillgruppenService = inject(SkillgruppenService);
  public mitarbeiterService = inject(MitarbeiterService);

  public skillGruppen = toSignal(this.skillgruppenService.apiSkillgruppenGetSkillgruppenWithUsersGet());
  public mitarbeiter = toSignal(this.mitarbeiterService.apiMitarbeiterGetAllMitarbeiterGet());

  mitarbeiterForm = new FormGroup({
    mitarbeiter: new FormControl('')
  });

  public getSkillLevel(level: number | undefined): string {
    if (level === undefined) {
      return 'skill-level-undefined';
    }

    return 'skill-level-' + level;
  }

  public onSelectMitarbeiter(value: any): void {
    console.log('mitarbeiter', value);
  }
}
