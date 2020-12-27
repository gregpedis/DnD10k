import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Effect } from 'src/app/models/effect';
import { GenerationService } from 'src/app/services/generation/generation.service';

@Component({
  selector: 'app-generation',
  templateUrl: './generation.component.html',
  styleUrls: ['./generation.component.scss']
})
export class GenerationComponent implements OnInit, OnDestroy, AfterViewInit {

  public maxHistoryLength: number = 10;

  public effect: Effect = new Effect();
  public effectsHistory: Effect[] = [];

  private effectSub: Subscription = new Subscription;
  private errorSub: Subscription = new Subscription;

  constructor(private generationService: GenerationService) { }

  ngOnInit(): void {

    this.effectSub = this.generationService
      .effectObservable
      .subscribe(
        data => { this.addLastEffectToHistory(); this.effect = data; this.saveToStorage(); },
        error => { alert(`Error ${error.error} Message ${error.message}`); }
      );

    this.errorSub = this.generationService
      .errorObservable
      .subscribe(
        data => { alert(`Error ${data.error} Message ${data.message}`); },
        error => { alert(`Error ${error.error} Message ${error.message}`); }
      );
  }

  ngAfterViewInit(): void {
    this.loadFromStorage();
  }

  ngOnDestroy(): void {
    if (this.effectSub) {
      this.effectSub.unsubscribe();
    }
    if (this.errorSub) {
      this.errorSub.unsubscribe();
    }
  }

  generateRandomEffect(): void {
    this.generationService.getRandomEffect();
  }

  addLastEffectToHistory(): void {
    const validEffect = Boolean(this.effect.description) && Boolean(this.effect.description.trim());
    if (validEffect) {
      this.effectsHistory.unshift(this.effect);
    }

    const historyTooLarge = Boolean(this.effectsHistory) && this.effectsHistory.length >= this.maxHistoryLength;
    if (historyTooLarge) {
      this.effectsHistory = this.effectsHistory.slice(0, this.maxHistoryLength);
    }
  }

  loadFromStorage(): void {
    const effectHistoryJson = localStorage.getItem("effectHistory");
    const effectJson = localStorage.getItem("effect");

    if (effectHistoryJson) {
      this.effectsHistory = JSON.parse(effectHistoryJson);
    }

    if (effectJson) {
      this.effect = JSON.parse(effectJson);
    }
    else {
      this.generateRandomEffect();
    }
  }

  saveToStorage(): void {
    localStorage.clear();

    const historyJson = JSON.stringify(this.effectsHistory);
    localStorage.setItem("effectHistory", historyJson);

    const effectJson = JSON.stringify(this.effect);
    localStorage.setItem("effect", effectJson);
  }
}
