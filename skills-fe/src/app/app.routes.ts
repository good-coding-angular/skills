import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./modules/skills/pages/employee/employee.component')
      .then(m => m.EmployeeComponent) // Lazy loaded
  },
];
