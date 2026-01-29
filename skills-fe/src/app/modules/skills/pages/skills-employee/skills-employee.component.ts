import {Component, inject, signal} from '@angular/core';
import {ApiModule, MitarbeiterDto, MitarbeiterService, SkillgruppeDto, SkillgruppenService} from '../../../backend';
import {toSignal} from '@angular/core/rxjs-interop';
import {NgClass} from '@angular/common';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';

@Component({
  selector: 'app-skills-employee',
  templateUrl: './skills-employee.component.html',
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
export class SkillsEmployeeComponent {
  public displayedMitarbeiter = ['1'];
  public displayedSkills = [];

  public skillgruppenService = inject(SkillgruppenService);
  public mitarbeiterService = inject(MitarbeiterService);

  public allSkillGruppen = toSignal(this.skillgruppenService.apiSkillgruppenGetSkillgruppenGet());
  public skillGruppen = signal<SkillgruppeDto[]>([]);
  public mitarbeiter = toSignal(this.mitarbeiterService.apiMitarbeiterGetAllMitarbeiterGet());

  mitarbeiterForm = new FormGroup({
    mitarbeiter: new FormControl(['1']),
    skillgruppen: new FormControl([])
  });

  public ngOnInit(): void {
    this.skillgruppenService.apiSkillgruppenGetFilteredSkillgruppenPost(
      {
        users: this.displayedMitarbeiter,
        skills: this.displayedSkills
      }
    ).subscribe(response => {
      this.skillGruppen.set(response ?? []);
    });
  }

  public getSkillLevel(level: number | undefined): string {
    if (level === undefined) {
      return 'skill-level-undefined';
    }

    return 'skill-level-' + level;
  }

  public onSelectSkills(selectedSkills: any): void {
    this.displayedSkills = selectedSkills;

    this.skillgruppenService.apiSkillgruppenGetFilteredSkillgruppenPost(
      {
        users: this.displayedMitarbeiter,
        skills: this.displayedSkills
      }
    ).subscribe(response => {
      this.skillGruppen.set(response ?? []);
    });
  }

  public onSelectMitarbeiter(selectedMitarbeiter: any): void {
    this.displayedMitarbeiter = selectedMitarbeiter;

    this.skillgruppenService.apiSkillgruppenGetFilteredSkillgruppenPost(
      {
        users: this.displayedMitarbeiter,
        skills: this.displayedSkills
      }
    ).subscribe(response => {
      this.skillGruppen.set(response ?? []);
    });
  }

  public filterMitarbeiter(mitarbeiter: MitarbeiterDto[] | undefined): MitarbeiterDto[] | undefined {
    if (!mitarbeiter) {
      return mitarbeiter;
    }

    return mitarbeiter.filter(m => {
      if (!m.mitarbeiterId) return undefined;
      return this.displayedMitarbeiter.includes(m.mitarbeiterId);
    });
  }
}
