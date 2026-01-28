import {Component, inject} from '@angular/core';
import {ApiModule, SkillgruppenService, SkillsService} from '../../../backend';
import {toSignal} from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  standalone: true,
  providers: [
    SkillsService,
    SkillgruppenService,
  ],
  imports: [
    ApiModule,
  ]
})
export class EmployeeComponent {
  public skillsService = inject(SkillsService);
  public skillgruppenService = inject(SkillgruppenService);
  public skills = toSignal(this.skillsService.apiSkillsGetSkillsGet());
  public skillGruppen = toSignal(this.skillgruppenService.apiSkillgruppenGetSkillgruppenGet());
}
