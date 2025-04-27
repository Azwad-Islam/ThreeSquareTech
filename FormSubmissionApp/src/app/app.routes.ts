import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'form',
    loadComponent: () => import('./form/form.component').then(m => m.FormComponent)
  },
  {
    path: 'display',
    loadComponent: () => import('./display/display.component').then(m => m.DisplayComponent)
  },
  { path: '', redirectTo: '/form', pathMatch: 'full' },
  { path: '**', redirectTo: '/form' }
];
