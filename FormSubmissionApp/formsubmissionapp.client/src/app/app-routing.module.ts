import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormComponent } from './form/form.component';
import { DisplayComponent } from './display/display.component';


const routes: Routes = [
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

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
