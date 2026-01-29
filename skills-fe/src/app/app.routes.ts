import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./modules/skills/pages/skills-employee/skills-employee.component')
      .then(m => m.SkillsEmployeeComponent) // Lazy loaded
  },
  {
    path: 'admin/edit-mitarbeiter',
    loadComponent: () => import('./modules/admin/pages/admin-edit-mitarbeiter/admin-edit-mitarbeiter.component')
      .then(m => m.AdminEditMitarbeiterComponent) // Lazy loaded
  },
];
