import {Component, inject} from '@angular/core';
import {ApiModule, MitarbeiterService, SkillgruppenService, SkillsService} from '../../../backend';
import {toSignal} from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  standalone: true,
  providers: [
    SkillsService,
    SkillgruppenService,
    MitarbeiterService,
  ],
  imports: [
    ApiModule,
  ]
})
export class EmployeeComponent {
  public skillsService = inject(SkillsService);
  public skillgruppenService = inject(SkillgruppenService);
  public mitarbeiterService = inject(MitarbeiterService);

  public skills = toSignal(this.skillsService.apiSkillsGetSkillsGet());
  public skillGruppen = toSignal(this.skillgruppenService.apiSkillgruppenGetSkillgruppenWithUsersGet());
  public mitarbeiter = toSignal(this.mitarbeiterService.apiMitarbeiterGetAllMitarbeiterGet());
}
