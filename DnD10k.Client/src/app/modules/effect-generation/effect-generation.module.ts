import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EffectGenerationRoutingModule } from './effect-generation.routing';
import { GenerationComponent } from './generation/generation.component';
import { GenerationService } from 'src/app/services/generation/generation.service';


@NgModule({
  declarations: [GenerationComponent],
  providers: [GenerationService],
  imports: [CommonModule, EffectGenerationRoutingModule]
})
export class EffectGenerationModule { }
