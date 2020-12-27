import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GenerationComponent } from './generation/generation.component';

const routes: Routes = [
  {
    path: '',
    component: GenerationComponent,
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EffectGenerationRoutingModule { }
