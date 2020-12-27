import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Effect } from 'src/app/models/effect';

@Injectable()
export class GenerationService {

  private effectDataSource = new Subject<Effect>();
  public effectObservable = this.effectDataSource.asObservable();

  private errorDataSource = new Subject<any>();
  public errorObservable = this.errorDataSource.asObservable();

  constructor(private http: HttpClient) { }

  public getRandomEffect() {
    this.http
      .get<Effect>('/api/generation')
      .subscribe(
        data => { this.effectDataSource.next(data); },
        error => { this.errorDataSource.next(error); }
      );
  }
}
